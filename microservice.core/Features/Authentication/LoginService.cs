using microservice.core.DTOs.Authentication.Login.Request;
using microservice.core.DTOs.Authentication.Token.Response;
using microservice.core.Interfaces.Services;
using microservice.domain.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace microservice.core.Features.Authentication;

public class LoginService : ILoginService
{
    private readonly IConfiguration _configuration;
    public LoginService(
         IConfiguration configuration
        )
    {
        _configuration = configuration;
    }

    public async Task<Response<JWTTokenDtoResponse>> AddLoginAsync(LoginDtoRequest login)
    {
        if (login.UserName == "microservice" && login.Password == "123456")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(6),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return await Task.FromResult(new Response<JWTTokenDtoResponse>(new JWTTokenDtoResponse { Token = tokenString }) { Message = $"Usuario autenticado." });

        }

        return new Response<JWTTokenDtoResponse>(new JWTTokenDtoResponse { Token = "" }) { Message = $"Usuario no autenticado." };

    }
}