using Poliedro.Billing.Domain.Common.Results.Errors;
using System.Net;

namespace Poliedro.Report.Application.PaymentMethodReport.Errors;

public class ReportMethodPaymentErrors : IError
{
    public const string REPORT_METHOD_PAYMENT_CREATION_ERROR = "ReportMethodPaymentCreationErrorException";
    public const string REPORT_METHOD_PAYMENT_NOT_FOUND_ERROR = "ReportMethodPaymentNotFoundErrorException";
    public const string REPORT_METHOD_PAYMENT_UPDATE_ERROR = "ReportMethodPaymentUpdateErrorException";
    public const string NO_REPORT_METHOD_PAYMENT_RECORDS_FOUND = "NoReportMethodPaymentRecordsFoundErrorException";
    public const string REPORT_METHOD_PAYMENT_DELETION_ERROR = "ReportMethodPaymentDeletionErrorException";

    public static Error ReportMethodPaymentCreationException() => Error.CreateInstance(
        REPORT_METHOD_PAYMENT_CREATION_ERROR,
        "Failed to create Report Method Payment due to an internal error.",
        HttpStatusCode.InternalServerError);

    public static Error ReportMethodPaymentNotFoundException(int id) => Error.CreateInstance(
        REPORT_METHOD_PAYMENT_NOT_FOUND_ERROR,
        $"Report Method Payment with ID {id} was not found.",
        HttpStatusCode.NotFound);

    public static Error ReportMethodPaymentUpdateException(int id) => Error.CreateInstance(
        REPORT_METHOD_PAYMENT_UPDATE_ERROR,
        $"Failed to update Report Method Payment with ID {id} due to an internal error.",
        HttpStatusCode.InternalServerError);

    public static Error NoReportMethodPaymentRecordsFoundException() => Error.CreateInstance(
        NO_REPORT_METHOD_PAYMENT_RECORDS_FOUND,
        "No active Report Method Payment records were found.",
        HttpStatusCode.NotFound);

    public static Error ReportMethodPaymentDeletionException(int id) => Error.CreateInstance(
        REPORT_METHOD_PAYMENT_DELETION_ERROR,
        $"Failed to delete Report Method Payment with ID {id} due to an internal error.",
        HttpStatusCode.InternalServerError);
}
