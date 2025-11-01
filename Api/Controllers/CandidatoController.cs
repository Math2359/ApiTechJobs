using Api.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Enum;
using Model.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

[Route("candidato")]
[ApiController]
public class CandidatoController(IOptions<JwtSettings> jwt) : ControllerBase
{
    private readonly JwtSettings _jwt = jwt.Value;

    [HttpGet]
    [AutorizarPerfis(EnumPerfil.Candidato, EnumPerfil.Empresa)]
    public string Get()
    {
        return User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
    }

    [HttpPost]
    public string Post([FromBody] EnumPerfil perfil)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwt.SigningKey ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("Id", "1"),
                new Claim(ClaimTypes.Role, perfil.ToString()),
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            Audience = _jwt.Audience,
            Issuer = _jwt.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [Authorize]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
