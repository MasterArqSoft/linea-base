using microservice.core.Interfaces.Repositories;
using microservice.core.Interfaces.Services;
using microservice.domain.Entities;
using microservice.domain.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace microservice.core.Helpers;

public class LogHelperService : ILogHelperService
{
    private readonly IUnitOfWork _unitOfWork;

    public LogHelperService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task LogError(
        string _requestBody,
        string _response,
        Stopwatch _sw,
        Exception ex,
        int StatusCode,
        DateTime _beginTime,
        HttpContext context
        )
    {
        var detail = GetLogDetail(_requestBody, _response, context, null,
                                  ex, StatusCode, _sw, _beginTime).Result;

        await WritingLogs(detail);
    }

    private static async Task<LogDetail> GetLogDetail(
        string _requestBody,
        string _response,
        HttpContext context,
        string? AditionalInfo,
        Exception ex,
        int StatusCode,
        Stopwatch _sw,
        DateTime _beginTime
        )
    {
        var detail = new LogDetail()
        {
            CorrelationalId = Activity.Current?.Id ?? context.TraceIdentifier,
            MicroService = $"{context.Request.Path}".Split("/")[2],
            StatusCode = StatusCode,
            ActivityName = $"{context.Request.Path} - {context.Request.Method}",
            Location = ex.Source,
            Ip = UtilitiesHelper.GetClientIPAddress(context),
            HostName = Environment.MachineName,
            Exception = ex.ToString(),
            StackTrace = ex.StackTrace,
            InnerException = ex.InnerException?.Message.Replace("'", "") ?? string.Empty,
            RequestBody = _requestBody.Trim().Replace(" ", ""),
            Response = _response.Trim().Replace(" ", ""),
            AditionalInfo = AditionalInfo ?? "",
            Message = ex.Message,
            UserId = "",
            UserName = ""
        };
        _sw.Stop();
        detail.ElapsedMilliSeconds = _sw.ElapsedMilliseconds;
        detail.AditionalInfo = JsonConvert.SerializeObject((new Dictionary<string, object>()
        {
          {"Started", _beginTime.ToString(CultureInfo.InvariantCulture) }
        }), Formatting.Indented);

        return await Task.FromResult(detail);
    }

    private async Task WritingLogs(LogDetail detail)
    {
        await _unitOfWork.LogRepositoryAsync.AddAsync(detail);
        await _unitOfWork.CommitAsync();
    }
}
