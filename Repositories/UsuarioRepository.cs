using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;

namespace Repositories;

public class UsuarioRepository(IConfiguration configuration) : GenericRepository<Usuario>(configuration)
{

    public Usuario? ObterUsuarioPorLogin(string login)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = "SELECT Id, Perfil, Login, Senha, DataCadastro FROM Usuario WHERE Login = @login";

        return conexao.QuerySingleOrDefault<Usuario>(sqlCommand, new { login });
    }
}
