using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;

namespace Repositories;

public class UsuarioRepository(IConfiguration configuration) : GenericRepository<Usuario>(configuration)
{
    public override int Adicionar(Usuario usuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"INSERT INTO Usuario (Perfil, Login, Senha, DataCadastro)
                                    OUTPUT INSERTED.Id
                                    VALUES (@Perfil, @Login, @Senha, @DataCadastro)";

        return conexao.ExecuteScalar<int>(sqlCommand, usuario);
    }

    public Usuario? ObterUsuarioPorLogin(string login)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = "SELECT Id, Perfil, Login, Senha, DataCadastro FROM Usuario WHERE Login = @login";

        return conexao.QuerySingleOrDefault<Usuario>(sqlCommand, new { login });
    }
}
