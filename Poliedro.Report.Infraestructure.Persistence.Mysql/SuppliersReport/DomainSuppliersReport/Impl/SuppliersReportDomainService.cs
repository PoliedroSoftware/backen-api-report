using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.SuppliersReport.DomainSuppliersReport;
using Poliedro.Billing.Domain.SuppliersReport.Entities;
using Poliedro.Report.Application.Ports.Redis;
using StackExchange.Redis;

namespace Poliedro.Billing.Infraestructure.Persistence.Mysql.SuppliersReport.DomainService.Impl
{
    public class SuppliersReportDomainService(IConfiguration config, IRedisService redisService) : ISuppliersReportDomainSuppliersReport
    {
        private readonly string _connectionString = config["ConnectionStrings:MysqlConnection"];

        public async Task<Result<IEnumerable<SuppliersReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
        {
            List<SuppliersReportEntity> suppliersReports = new();

            string cacheKey = $"suppliersReport_{paginationParams.PageNumber}_{paginationParams.PageSize}";
            var cachedData = await redisService.GetCacheAsync<IEnumerable<SuppliersReportEntity>>(cacheKey);
            if (cachedData is not null) return Result<IEnumerable<SuppliersReportEntity>, Error>.Success(cachedData);

            using MySqlConnection connection = new(_connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);

                int offset = (paginationParams.PageNumber -1) * paginationParams.PageSize;
                string query = $"SELECT * FROM v_saldo_por_proveedor"; // LIMIT @PageSize OFFSET @Offset";

                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
                command.Parameters.AddWithValue("@Offset", offset);
                
                using MySqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    SuppliersReportEntity report = new()
                    {
                        Proveedor = reader.GetString(0),
                        Saldo = reader.GetDecimal(1)
                    };
                    suppliersReports.Add(report);
                }

                return Result<IEnumerable<SuppliersReportEntity>, Error>.Success(suppliersReports);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return Result<IEnumerable<SuppliersReportEntity>, Error>.Failure(null);
        }
    }
}