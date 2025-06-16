
namespace Poliedro.Billing.Application.InventoryReport.Dtos;

public class InventoryReportDto
{
    public string SKU { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Presentation { get; set; } = string.Empty;
    public decimal Cost { get; set; } = default!;
    public decimal Sale { get; set; } = default!;
    public decimal Inventory { get; set; } = default!;
    public decimal Percentage { get; set; } = default!;
    public decimal Subtotal_Cost { get; set; } = default!;
    public decimal Subtotal_sale { get; set; } = default!;
}
