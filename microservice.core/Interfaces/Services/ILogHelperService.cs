using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace microservice.core.Interfaces.Services;

public interface ILogHelperService
{
    Task LogError(
        string _requestBody,
        string _response,
        Stopwatch _sw,
        Exception ex,
        int StatusCode,
        DateTime _beginTime,
        HttpContext context
        );
}
