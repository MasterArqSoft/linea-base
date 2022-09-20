using microservice.domain.BaseEntities;

namespace microservice.domain.Entities;

public class LogDetail : EntityBase
{
    public LogDetail()
    {
        DateTimeLogs = DateTime.Now;
    }

    public DateTime DateTimeLogs { get; set; }
    public string? Message { get; set; }

    ////Where
    public string? MicroService { get; set; }
    public string? ActivityName { get; set; }
    public string? Location { get; set; }
    public int StatusCode { get; set; }
    public string? HostName { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    public string? Ip { get; set; }

    ////Who
    public string? UserId { get; set; }
    public string? UserName { get; set; }

    ////Everything else
    public long? ElapsedMilliSeconds { get; set; }
    public string? Exception { get; set; }
    public string? CorrelationalId { get; set; }
    public string? RequestBody { get; set; }
    public string? Response { get; set; }
    public string? AditionalInfo { get; set; }
}