
using Poliedro.Billing.Domain.Common.Results.Errors;
using System.Net;

namespace Poliedro.Report.Application.SalesReport.Erros
{
    public class SalesReportErros
    {
        public const string SALES_REPORT_CREATION_ERROR = "SalesReportCreationErrorException";
        public const string SALES_REPORT_NOT_FOUND_ERROR = "SalesReportNotFoundErrorException";
        public const string SALES_REPORT_UPDATE_ERROR = "SalesReportUpdateErrorException";
        public const string NO_SALES_REPORT_RECORDS_FOUND = "NoSalesReportRecordsFoundErrorException";
        public const string SALES_REPORT_DELETION_ERROR = "SalesReportDeletionErrorException";

        public static Error SalesReportCreationException() => Error.CreateInstance(
            SALES_REPORT_CREATION_ERROR,
            "Failed to create Sales Report due to an internal error.",
            HttpStatusCode.InternalServerError);

        public static Error SalesReportNotFoundException(int id) => Error.CreateInstance(
            SALES_REPORT_NOT_FOUND_ERROR,
            $"Sales Report with ID {id} was not found.",
            HttpStatusCode.NotFound);

        public static Error SalesReportUpdateException(int id) => Error.CreateInstance(
            SALES_REPORT_UPDATE_ERROR,
            $"Failed to update Sales Report with ID {id} due to an internal error.",
            HttpStatusCode.InternalServerError);

        public static Error NoSalesReportRecordsFoundException() => Error.CreateInstance(
            NO_SALES_REPORT_RECORDS_FOUND,
            "No active Sales Report records were found.",
            HttpStatusCode.NotFound);

        public static Error SalesReportDeletionException(int id) => Error.CreateInstance(
            SALES_REPORT_DELETION_ERROR,
            $"Failed to delete Sales Report with ID {id} due to an internal error.",
            HttpStatusCode.InternalServerError);
    }
}
