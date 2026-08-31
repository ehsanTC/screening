using System.Net;
using Screening.Domain.RiskCalculator.Fracture.Model;

namespace Screening.Domain.RiskCalculator.Fracture.Utility;

public class FractureRiskApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public FractureRiskErrorResponse? ErrorResponse { get; }

    public FractureRiskApiException(
        HttpStatusCode statusCode,
        string message,
        FractureRiskErrorResponse? errorResponse = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorResponse = errorResponse;
    }
}
