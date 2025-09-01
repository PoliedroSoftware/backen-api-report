using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.InventoryReport.DomainInventoryReport;
using Poliedro.Billing.Domain.InventoryReport.Entities;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.InventoryReport.Impl;

public class InventoryReportDomainService(IConfiguration config) : IInventoryReportDomainInventoryReport
{
    private readonly string _connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? config["ConnectionStrings:MysqlConnection"];

    public async Task<Result<PaginationResponse<InventoryReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
    {
        List<InventoryReportEntity> inventoryReports = [];
        int totalRows = 0;

        using MySqlConnection connection = new(_connectionString);
        try
        {
            await connection.OpenAsync(cancellationToken);

            // First, get the total count
            string countQuery = "SELECT COUNT(*) FROM v_inventario WHERE cost > 0";
            using (MySqlCommand countCommand = new(countQuery, connection))
            {
                var result = await countCommand.ExecuteScalarAsync(cancellationToken);
                totalRows = Convert.ToInt32(result);
            }

            // Then get the paginated data
            int offset = (paginationParams.PageNumber - 1) * paginationParams.PageSize;
            string query = "SELECT * FROM v_inventario WHERE cost > 0 LIMIT @PageSize OFFSET @Offset";

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
            command.Parameters.AddWithValue("@Offset", offset);

            using MySqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
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

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalRows / paginationParams.PageSize);

            var paginationResponse = new PaginationResponse<InventoryReportEntity>
            {
                Data = inventoryReports,
                TotalRows = totalRows,
                TotalPages = totalPages
            };

            return Result<PaginationResponse<InventoryReportEntity>, Error>.Success(paginationResponse);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }

        return Result<PaginationResponse<InventoryReportEntity>, Error>.Failure(null);
    }
}
