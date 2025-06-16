using Poliedro.Billing.Domain.Common.Results.Errors;
using System.Net;

namespace Poliedro.Report.Application.InvoiceReport.Errors;

public class InvoiceDetailReportErrors : IError
{
    public const string INVOICE_DETAIL_NOT_FOUND = "InvoiceDetailNotFound";
    public const string INVOICE_DETAIL_FETCH_ERROR = "InvoiceDetailFetchError";
    public const string INVOICE_DETAIL_INTERNAL_ERROR = "InvoiceDetailInternalError";

    public static Error InvoiceDetailNotFoundException(int transaccionId) => Error.CreateInstance(
        INVOICE_DETAIL_NOT_FOUND,
        $"No se encontraron detalles de factura para la transacción con ID {transaccionId}.",
        HttpStatusCode.NotFound);

    public static Error InvoiceDetailFetchException() => Error.CreateInstance(
        INVOICE_DETAIL_FETCH_ERROR,
        "Hubo un error al obtener los detalles de la factura.",
        HttpStatusCode.BadRequest);

    public static Error InvoiceDetailInternalException() => Error.CreateInstance(
        INVOICE_DETAIL_INTERNAL_ERROR,
        "Error interno al procesar los detalles de la factura.",
        HttpStatusCode.InternalServerError);
}
