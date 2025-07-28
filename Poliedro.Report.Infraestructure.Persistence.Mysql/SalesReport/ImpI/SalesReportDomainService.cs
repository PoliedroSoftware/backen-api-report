using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Domain.SalesReport.Entities;
using Poliedro.Report.Domain.SalesReport.Ports;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.SalesReport.ImpI;

public class SalesReportDomainService(IConfiguration config) : ISalesReportDomain
{
    private readonly string _connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? config["ConnectionStrings:MysqlConnection"];

    public async Task<Result<SalesReportEntity, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
    {
        SalesReportEntity salesReports = new();

        using MySqlConnection connection = new(_connectionString);
        try
        {
            await connection.OpenAsync(cancellationToken);

            int offset = (paginationParams.PageNumber - 1) * paginationParams.PageSize;

            string queryYear = "SELECT * FROM v_sales_for_year";
            using (MySqlCommand cmd = new(queryYear, connection))
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    salesReports.ForYear.Add(new SalesReportYearEntity
                    {
                        Year = reader.GetInt32(0),
                        Sale = reader.GetDecimal(1)
                    });
                }
            }

            string queryMonth = "SELECT * FROM v_sale_for_month";
            using (MySqlCommand cmd = new(queryMonth, connection))
            using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    salesReports.ForMonth.Add(new SalesReportMonthEntity
                    {
                        NumberMonth = reader.GetInt32(0),
                        Month = reader.GetString(1),
                        Year = reader.GetInt32(2),
                        Sale = reader.GetDecimal(3)
                    });
                }
            }

            string queryDay = "SELECT * FROM v_sales_for_day LIMIT @Pagesize OFFSET @Offset";
            using (MySqlCommand cmd = new(queryDay, connection))
            {
                cmd.Parameters.AddWithValue("@Pagesize", paginationParams.PageSize);
                cmd.Parameters.AddWithValue("@Offset", offset);

                using (var reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        salesReports.ForDay.Add(new SalesReportDayEntity
                        {
                            Date = reader.GetDateTime(0),
                            NumberMonth = reader.GetInt32(1),
                            Month = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Sale = reader.GetDecimal(4)
                        });
                    }
                }
            }

            return Result<SalesReportEntity, Error>.Success(salesReports);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
            return Result<SalesReportEntity, Error>.Failure(null);
        }
    }
}
