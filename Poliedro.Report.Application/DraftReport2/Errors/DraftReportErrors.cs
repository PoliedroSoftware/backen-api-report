
using Poliedro.Billing.Domain.Common.Results.Errors;
using System.Net;

namespace Poliedro.Report.Application.DraftReport.Erros
{
    public class DraftReportErros
    {
        public const string DRAFT_REPORT_CREATION_ERROR = "DraftReportCreationErrorException";
        public const string DRAFT_REPORT_NOT_FOUND_ERROR = "DraftReportNotFoundErrorException";
        public const string DRAFT_REPORT_UPDATE_ERROR = "DraftReportUpdateErrorException";
        public const string NO_DRAFT_REPORT_RECORDS_FOUND = "NoDraftReportRecordsFoundErrorException";
        public const string DRAFT_REPORT_DELETION_ERROR = "DraftReportDeletionErrorException";

        public static Error DraftReportCreationException() => Error.CreateInstance(
            DRAFT_REPORT_CREATION_ERROR,
            "Failed to create Draft Report due to an internal error.",
            HttpStatusCode.InternalServerError);

        public static Error DraftReportNotFoundException(int id) => Error.CreateInstance(
            DRAFT_REPORT_NOT_FOUND_ERROR,
            $"Draft Report with ID {id} was not found.",
            HttpStatusCode.NotFound);

        public static Error DraftReportUpdateException(int id) => Error.CreateInstance(
            DRAFT_REPORT_UPDATE_ERROR,
            $"Failed to update Draft Report with ID {id} due to an internal error.",
            HttpStatusCode.InternalServerError);

        public static Error NoDraftReportRecordsFoundException() => Error.CreateInstance(
            NO_DRAFT_REPORT_RECORDS_FOUND,
            "No active Draft Report records were found.",
            HttpStatusCode.NotFound);

        public static Error DraftReportDeletionException(int id) => Error.CreateInstance(
            DRAFT_REPORT_DELETION_ERROR,
            $"Failed to delete Draft Report with ID {id} due to an internal error.",
            HttpStatusCode.InternalServerError);
    }
}
