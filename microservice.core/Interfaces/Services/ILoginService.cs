using microservice.core.DTOs.Authentication.Login.Request;
using microservice.core.DTOs.Authentication.Token.Response;
using microservice.domain.Wrappers;

namespace microservice.core.Interfaces.Services;

public interface ILoginService
{
    Task<Response<JWTTokenDtoResponse>> AddLoginAsync(LoginDtoRequest login);
}
