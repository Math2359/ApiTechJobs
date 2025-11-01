using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Enum;
using Model.Options;
using Model.Request;
using Repositories;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utils;

namespace Services;

public class UsuarioService(IOptions<JwtSettings> jwt, UsuarioRepository usuarioRespository, ICandidatoService candidatoService, IEmpresaService empresaService) : IUsuarioService
{
    private readonly JwtSettings _jwt = jwt.Value;
    private readonly UsuarioRepository _usuarioRepository = usuarioRespository;
    private readonly ICandidatoService _candidatoService = candidatoService;
    private readonly IEmpresaService _empresaService = empresaService;

    public void NovoUsuario(NovoUsuarioRequest novoUsuario)
    {
        var usuario = new Usuario
        {
            DataCadastro = DateTime.Now,
            Login = novoUsuario.Login,
            Perfil = novoUsuario.Perfil,
            Senha = novoUsuario.Senha.CriptografarSenha()
        };

        int idUsuario = _usuarioRepository.Adicionar(usuario);

        switch (novoUsuario.Perfil)
        {
            case EnumPerfil.Candidato:
                var candidato = new Candidato
                {
                    Cpf = novoUsuario.Documento,
                    Email = novoUsuario.Login,
                    IdUsuario = idUsuario,
                    Nome = novoUsuario.Nome,
                };

                _candidatoService.Adicionar(candidato);
                break;

            case EnumPerfil.Empresa:
                var empresa = new Empresa
                {
                    Cep = novoUsuario.Cep,
                    Cnpj = novoUsuario.Documento,
                    Email = novoUsuario.Login,
                    IdUsuario = idUsuario,
                    Nome = novoUsuario.Nome,
                    Numero = novoUsuario.Numero
                };

                _empresaService.Adicionar(empresa);
                break;
        }

    }

    public string LogarUsuario(LogarUsuarioRequest logarUsuario)
    {
        var usuario = _usuarioRepository.ObterUsuarioPorLogin(logarUsuario.Login) ?? throw new Exception("Usuario ou senha incorretos!");

        if (!usuario.Senha.VerificarSenha(logarUsuario.Senha)) throw new Exception("Usuario ou senha incorretos!");

        return GerarToken(usuario);
    }

    private string GerarToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwt.SigningKey ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("Id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
            ]),
            Expires = DateTime.UtcNow.AddDays(1),
            Audience = _jwt.Audience,
            Issuer = _jwt.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}