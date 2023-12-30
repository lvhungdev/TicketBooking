using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using API.Configurations;
using API.Controllers.Authentication.Dtos;
using Domain.Users.Models;
using Domain.Users.UseCases;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.Authentication;

[Route("api/authentication")]
public class AuthenticationController : ApiController
{
    private readonly IMediator mediator;
    private readonly JwtSettings jwtSettings;

    public AuthenticationController(IMediator mediator, JwtSettings jwtSettings)
    {
        this.mediator = mediator;
        this.jwtSettings = jwtSettings;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInReqDto dto)
    {
        SignInRequest req = new(dto.Email, dto.Password);
        User? user = await mediator.Send(req);

        if (user == null) return Problem(statusCode: (int)HttpStatusCode.Unauthorized);

        return Ok(new AuthResDto(GenerateJwt(user)));
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpReqDto dto)
    {
        SignUpRequest req = new(dto.Email, dto.Password, dto.FullName);
        Result<User> userResult = await mediator.Send(req);

        if (userResult.IsFailed) return Problem(userResult.Errors);

        return Ok(new AuthResDto(GenerateJwt(userResult.Value)));
    }

    private string GenerateJwt(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

        SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
        SigningCredentials credentials = new(signingKey, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken token = new(
            claims: claims,
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            signingCredentials: credentials,
            expires: DateTime.Now.AddDays(1));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
