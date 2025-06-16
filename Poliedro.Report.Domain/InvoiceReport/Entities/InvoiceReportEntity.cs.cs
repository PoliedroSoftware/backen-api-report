

using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.InvoiceReport.Entities
{
    public class InvoiceReportEntity
    {
        [Key]
        public int Id { get; set; }
        public int Transaccion { get; set; }
        public int Code { get; set; }
        public int TypeItemIdentificationId { get; set; }
        public required string Description { get; set; }
        public int UnitMeasureId { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal InvoicedQuantity { get; set; }
        public decimal PriceAmount { get; set; }
        public decimal LineExtensionAmount { get; set; }
        public double Percent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public required string ContactName { get; set; }
    }
}
