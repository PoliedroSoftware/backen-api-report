using Poliedro.Billing.Domain.SuppliersReport.Enums;

namespace Poliedro.Billing.Domain.SuppliersReport.Entities
{
    public class SuppliersReportEntity
    {
        public string Proveedor { get; set; } = string.Empty;  
        public decimal Saldo { get; set; } = default!;  
    }
}