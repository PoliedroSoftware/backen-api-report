using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.InventoryReport.DomainInventoryReport;
using Poliedro.Billing.Domain.InventoryReport.Entities;
using Poliedro.Report.Application.Ports.Redis;


namespace Poliedro.Report.Infraestructure.Persistence.Mysql.InventoryReport.Impl;

public class InventoryReportDomainService(IConfiguration config, IRedisService redisService) : IInventoryReportDomainInventoryReport
{
    private readonly string _connectionString = config["ConnectionStrings:MysqlConnection"];

public async Task<Result<IEnumerable<InventoryReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
{
    List<InventoryReportEntity> inventoryReports = new();
    
    string cacheKey = $"inventoryReport_{paginationParams.PageNumber}_{paginationParams.PageSize}";
    var cachedData = await redisService.GetCacheAsync<IEnumerable<InventoryReportEntity>>(cacheKey);
    if (cachedData is not null) return Result<IEnumerable<InventoryReportEntity>, Error>.Success(cachedData); 

    using MySqlConnection connection = new(_connectionString);
    try
    {
        await connection.OpenAsync(cancellationToken);

        int offset = (paginationParams.PageNumber - 1) * paginationParams.PageSize;
        string query = "SELECT * FROM v_inventario LIMIT @PageSize OFFSET @Offset";
        
        using MySqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
        command.Parameters.AddWithValue("@Offset", offset);

        using MySqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            if (reader.GetDecimal(3) > 0)
            {
                InventoryReportEntity report = new()
                {
                    SKU = reader.GetString(0),
                    Name = reader.GetString(1),
                    Presentation = reader.GetString(2),
                    Cost = reader.GetDecimal(3),
                    Sale = reader.GetDecimal(4),
                    Inventory = reader.GetDecimal(5),
                    Percentage = reader.GetDecimal(6),
                    Subtotal_Cost = reader.GetDecimal(7),
                    Subtotal_sale = reader.GetDecimal(8),
                };
                inventoryReports.Add(report);
            }
        }

        await redisService.SetCacheAsync(cacheKey, inventoryReports, TimeSpan.FromMinutes(1440));
        return Result<IEnumerable<InventoryReportEntity>, Error>.Success(inventoryReports);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database error: {ex.Message}");
    }

    return Result<IEnumerable<InventoryReportEntity>, Error>.Failure(null);
}
}