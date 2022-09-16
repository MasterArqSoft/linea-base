using microservice.core.DTOs.Health;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace product.api.Controllers;

[Produces("application/json")]
[Route("/api/health")]
[ApiController]
[AllowAnonymous]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        this.healthCheckService = healthCheckService;
    }

    /// <summary>
    /// Get Health
    /// </summary>
    /// <remarks>Provides an indication about the health of the API</remarks>
    /// <response code="200">API is healthy</response>
    /// <response code="503">API is unhealthy or in degraded state</response>
    [HttpGet]
    [ProducesResponseType(typeof(HealthCheckDtoReponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAsync()
    {
        HealthReport report = await this.healthCheckService.CheckHealthAsync();
        var response = new HealthCheckDtoReponse
        {
            Status = report.Status.ToString(),
            HealthChecks = report.Entries.Select(x => new StatusHealthsDtoResponse
            {
                Components = x.Key,
                Status = x.Value.Status.ToString(),
                Description = x.Value.Description ?? ""
            }),
            HealthCheckDuration = report.TotalDuration
        };

        return Ok(response);
    }
}
