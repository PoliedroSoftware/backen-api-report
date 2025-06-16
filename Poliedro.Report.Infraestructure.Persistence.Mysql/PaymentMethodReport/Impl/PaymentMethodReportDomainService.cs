using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Report.Domain.PaymentMethodReport.Ports;
using Poliedro.Report.Domain.PaymentMethodReport.Entity;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.Ports.Redis;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.PaymentMethodReport.Impl
{
    public class PaymentMethodReportDomainService(IConfiguration config, IRedisService redisService) : IPaymentMethodReportDomain
    {
        private readonly string _connectionString = config["ConnectionStrings:MysqlConnection"];

        public async Task<Result<PaymentMethodReportEntity, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
        {
            PaymentMethodReportEntity paymentReports = new();

                string cacheKey = $"paymentReport_{paginationParams.PageNumber}_{paginationParams.PageSize}";
                var cachedData = await redisService.GetCacheAsync<PaymentMethodReportEntity>(cacheKey);
                if (cachedData is not null) return Result<PaymentMethodReportEntity, Error>.Success(cachedData); 


            using MySqlConnection connection = new(_connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);

                int offset = (paginationParams.PageNumber -1) * paginationParams.PageSize;


                string queryYear = "SELECT * FROM v_sales_for_pay_method_for_year LIMIT @Pagesize OFFSET @Offset";
                using (MySqlCommand cmd = new(queryYear, connection)){

                    cmd.Parameters.AddWithValue("@Pagesize", paginationParams.PageSize);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                
                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        paymentReports.ForYear.Add(new PaymentMethodReportYearEntity
                        {
                                Year = reader.GetInt32(0),
                                PaymentMethod = reader.GetString(1),
                                TotalPaid = reader.GetDecimal(2),
                                TotalSales = reader.GetDecimal(3),
                                InvoiceQuantity = reader.GetInt64(4)
                            });
                        }
                    }
                }

                
                string queryMonth = "SELECT * FROM v_sales_for_pay_method_for_month";
                using (MySqlCommand cmd = new(queryMonth, connection))
                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        paymentReports.ForMonth.Add(new PaymentMethodReportMonthEntity
                        {
                            Year = reader.GetInt32(0),
                            Nmonth = reader.GetInt32(1),
                            Month = reader.GetString(2),
                            PaymentMethod = reader.GetString(3),
                            TotalPaid = reader.GetDecimal(4),
                            TotalSales = reader.GetDecimal(5),
                            InvoiceQuantity = reader.GetInt64(6)
                        });
                    }
                }

                string queryDay = "SELECT * FROM v_sales_for_pay_method_for_day LIMIT @Pagesize OFFSET @Offset";
                using (MySqlCommand cmd = new(queryDay, connection)){

                    cmd.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
                    cmd.Parameters.AddWithValue("@Offset", offset);
                
                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        paymentReports.ForDay.Add(new PaymentMethodReportDayEntity
                        {
                            Date = reader.GetDateTime(0),
                            PaymentMethod = reader.GetString(1),
                            TotalPaid = reader.GetDecimal(2),
                            TotalSales = reader.GetDecimal(3),
                            InvoiceQuantity = reader.GetInt64(4),
                            TotalSalesDay = reader.GetDecimal(5)
                        });
                    }
                }
            }

                return Result<PaymentMethodReportEntity, Error>.Success(paymentReports);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return Result<PaymentMethodReportEntity, Error>.Failure(null);
            }
        }
    }
}
