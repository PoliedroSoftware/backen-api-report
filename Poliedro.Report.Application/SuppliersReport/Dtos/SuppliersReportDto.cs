namespace Poliedro.Billing.Application.SuppliersReport.Dtos
{
    public class SuppliersReportDto
    {
        public string Proveedor { get; set; } = string.Empty;  
        public decimal Saldo { get; set; } = default!;  
    }
}
