using microservice.core.DTOs.Authentication.Login.Request;
using microservice.core.Interfaces.Services;
using microservice.domain.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace product.api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    /// <summary>
    /// Agrega un nuevo login.
    /// </summary>
    /// <remarks>
    /// Inserta los datos del login.
    /// </remarks>
    /// <returns>Retorna los datos del login agregado.</returns>
    /// <param name="login">El objeto login.</param>
    /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
    /// <response code="200">OK. Devuelve la información solicitada.</response>
    /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] LoginDtoRequest login)
    {
        return Ok(await _loginService.AddLoginAsync(login));
    }
}
