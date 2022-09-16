using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.core.DTOs.Health;

public class HealthCheckDtoReponse
{
    public string? Status { get; set; }
    public IEnumerable<StatusHealthsDtoResponse>? HealthChecks { get; set; }
    public TimeSpan HealthCheckDuration { get; set; }
}
