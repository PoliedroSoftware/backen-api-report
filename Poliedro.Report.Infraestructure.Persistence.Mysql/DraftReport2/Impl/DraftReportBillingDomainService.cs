using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.DraftReport.DomainDraftReport;
using Poliedro.Billing.Domain.DraftReport.Entities;
using Poliedro.Report.Application.Ports.Redis;


namespace Poliedro.Report.Infraestructure.Persistence.Mysql.DraftReport.Impl;

public class DraftReportDomainService(IConfiguration config, IRedisService redisService) : IDraftReportDomainDraftReport
{
    private readonly string _connectionString = config["ConnectionStrings:MysqlConnection"];

    public async Task<Result<IEnumerable<DraftReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
    {
        List<DraftReportEntity> draftReports = new();

        string cacheKey = $"draftReport_{paginationParams.PageNumber}_{paginationParams.PageSize}";
        var cachedData = await redisService.GetCacheAsync<IEnumerable<DraftReportEntity>>(cacheKey);
        if (cachedData is not null) return Result<IEnumerable<DraftReportEntity>, Error>.Success(cachedData);

        using MySqlConnection connection = new(_connectionString);
        try
        {
            await connection.OpenAsync(cancellationToken);

            int offset = (paginationParams.PageNumber - 1) * paginationParams.PageSize;
            string query = "SELECT * FROM v_draft LIMIT @PageSize OFFSET @Offset";

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
            command.Parameters.AddWithValue("@Offset", offset);

            using MySqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                if (reader.GetDecimal(3) > 0)
                {
                    DraftReportEntity report = new()
                    {
                        Date = reader.GetString(0),
                        Code = reader.GetString(1),
                        Product = reader.GetString(2),
                        Quantity = reader.GetDecimal(3),
                        Price = reader.GetDecimal(4),
                        Subtotal = reader.GetDecimal(5),
                        Client = reader.GetString(6),
                        Reference = reader.GetString(7)

                    };
                    draftReports.Add(report);
                }
            }

            await redisService.SetCacheAsync(cacheKey, draftReports, TimeSpan.FromMinutes(1440));
            return Result<IEnumerable<DraftReportEntity>, Error>.Success(draftReports);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }

        return Result<IEnumerable<DraftReportEntity>, Error>.Failure(null);
    }
}