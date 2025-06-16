using Poliedro.Billing.Domain.Common.Results.Errors;
using System.Net;

namespace Poliedro.Report.Application.UtilityReport.Errors
{
    public class UtilityReportError : IError
    {
        public const string UTILITY_CREATION_ERROR = "UtilityReportCreationErrorException";
        public const string UTILITY_NOT_FOUND_ERROR = "UtilityReportNotFoundErrorException";

        public static Error UtilityReportCreationException() =>
        Error.CreateInstance(
            UTILITY_CREATION_ERROR,
            "Failed to create Utility Report due to an internal error.",
            HttpStatusCode.InternalServerError
        );
        public static Error UtilityReportNotFoundException(int id) =>
        Error.CreateInstance(
            UTILITY_NOT_FOUND_ERROR,
            $"Utility Report with ID {id} was not found.",
            HttpStatusCode.NotFound
        );
    }
}

