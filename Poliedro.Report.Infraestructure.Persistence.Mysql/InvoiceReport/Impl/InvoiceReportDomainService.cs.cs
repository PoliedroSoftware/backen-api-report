using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Domain.InvoiceReport.Ports;
using Poliedro.Report.Domain.InvoiceReport.Entities;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Report.Application.Ports.Redis;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.InvoiceReport.Impl
{
    public class InvoiceReportDomainService(IConfiguration config, IRedisService redisService) : IInvoiceReportDomainService
    {
        private readonly string _connectionString = config["ConnectionStrings:MysqlConnection"];

        public async Task<Result<IEnumerable<InvoiceReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams)
        {
            List<InvoiceReportEntity> invoiceReports = new();

                string cacheKey = $"invoiceReport_{paginationParams.PageNumber}_{paginationParams.PageSize}";
                var cachedData = await redisService.GetCacheAsync<IEnumerable<InvoiceReportEntity>>(cacheKey);
                if (cachedData is not null) return Result<IEnumerable<InvoiceReportEntity>, Error>.Success(cachedData); 



            using MySqlConnection connection = new(_connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken);

                string query = @"
                    SELECT 
                        v_invoice_detail.id,
                        v_invoice_detail.transaccion,
                        v_invoice_detail.code,
                        v_invoice_detail.type_item_identification_id,
                        v_invoice_detail.description,
                        v_invoice_detail.unit_measure_id,
                        v_invoice_detail.base_quantity,
                        v_invoice_detail.invoiced_quantity,
                        v_invoice_detail.price_amount,
                        v_invoice_detail.line_extension_amount,
                        v_invoice_detail.percent,
                        v_invoice_detail.tax_amount,
                        v_invoice_detail.unit_price,
                        v_invoice.contact_name
                    FROM 
                        v_invoice_detail
                    JOIN 
                        v_invoice ON v_invoice_detail.transaccion = v_invoice.id
                    LIMIT @Pagesize OFFSET @Offset";
                int offset = (paginationParams.PageNumber - 1) * paginationParams.PageSize;
                using MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@PageSize", paginationParams.PageSize);
                command.Parameters.AddWithValue("@Offset", offset);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        invoiceReports.Add(new InvoiceReportEntity
                        {
                            Id = reader.GetInt32(0),
                            Transaccion = reader.GetInt32(1),
                            Code = reader.GetInt32(2),
                            TypeItemIdentificationId = reader.GetInt32(3),
                            Description = reader.GetString(4),
                            UnitMeasureId = reader.GetInt32(5),
                            BaseQuantity = reader.GetDecimal(6),
                            InvoicedQuantity = reader.GetDecimal(7),
                            PriceAmount = reader.GetDecimal(8),
                            LineExtensionAmount = reader.GetDecimal(9),
                            Percent = reader.GetDouble(10),
                            TaxAmount = reader.GetDecimal(11),
                            UnitPrice = reader.GetDecimal(12),
                            ContactName = reader.GetString(13)
                        });
                    }
                }

                return Result<IEnumerable<InvoiceReportEntity>, Error>.Success(invoiceReports);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return Result<IEnumerable<InvoiceReportEntity>, Error>.Failure(null);
            }
        }
    }
}
