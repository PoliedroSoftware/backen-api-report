using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Report.Domain.UtilityReport.Entity;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Domain.UtilityReport.Ports;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Report.Application.Ports.Redis;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.UtilityReport.ImpI
{
    public class UtilityReportDomainService(IConfiguration config, IRedisService redisService) : IUtilityReportDomain
    {
        private readonly string _connectionString = config["ConnectionStrings:MysqlConnection"];

        public async Task<Result<UtilityReportEntity, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
        {
            UtilityReportEntity utilityReports = new();

            string cacheKey = $"utilityReport_{paginationParams.PageNumber}_{paginationParams.PageSize}";
            var cachedData = await redisService.GetCacheAsync<UtilityReportEntity>(cacheKey);
            if (cachedData is not null) return Result<UtilityReportEntity, Error>.Success(cachedData);

            using MySqlConnection connection = new(_connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);
                int offset = (paginationParams.PageNumber -1) * paginationParams.PageSize;

                string queryYear = "SELECT * FROM v_utility_for_year LIMIT @PageSize OFFSET @Offset";
                using (MySqlCommand cmd = new(queryYear, connection))
                {
                    cmd.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            utilityReports.ForYear.Add(new UtilityReportYearEntity
                            {
                                Year = reader.GetInt32(0),
                                Utility = reader.GetDecimal(1)
                            });
                        }
                    }
                }

                string queryMonth = "SELECT * FROM v_utility_for_month LIMIT @PageSize OFFSET @Offset";
                using (MySqlCommand cmd = new(queryMonth, connection))
                {
                    cmd.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                
                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            utilityReports.ForMonth.Add(new UtilityReportMonthEntity
                            {
                                Year = reader.GetInt32(0),
                                Month = reader.GetString(1),
                                Utility = reader.GetDecimal(2)
                            });
                        }
                    }
                }
                string queryDay = "SELECT * FROM v_utility_for_day LIMIT @PageSize OFFSET @Offset";
                using (MySqlCommand cmd = new(queryDay, connection))
                {
                    cmd.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
                    cmd.Parameters.AddWithValue("@Offset", offset);

                    using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            utilityReports.ForDay.Add(new UtilityReportDayEntity
                            {
                                Date = reader.GetDateTime(0),
                                Year = reader.GetInt32(1),
                                Month = reader.GetString(2),
                                Utility = reader.GetDecimal(3)
                            });
                        }
                    }
                }
                return Result<UtilityReportEntity, Error>.Success(utilityReports);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return Result<UtilityReportEntity, Error>.Failure(null);
            }
        }
    }
}
