
namespace Poliedro.Billing.Domain.DraftReport.Entities
{
    public class DraftReportEntity
    {
        public string Date { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Subtotal { get; set; } = default!;
        public string Client { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
    }
}