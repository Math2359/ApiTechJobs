using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;

namespace Repositories;

public class UsuarioRepository(IConfiguration configuration) : GenericRepository<Usuario>(configuration)
{
    public CredenciaisUsuarioDTO? ObterCredenciaisUsuario(string login)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT U.Id, U.Perfil, U.Senha, COALESCE(E.Email, C.Email) AS Email, COALESCE(E.Nome, C.Nome) AS Nome FROM Usuario AS U
                                    LEFT JOIN Empresa AS E
                                    ON E.IdUsuario = U.Id
                                    LEFT JOIN Candidato AS C
                                    ON C.IdUsuario = U.Id
                                    WHERE U.Login = @login";

        return conexao.QuerySingleOrDefault<CredenciaisUsuarioDTO>(sqlCommand, new { login });
    }
}
