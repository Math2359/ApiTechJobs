using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;

namespace Repositories;

public class NotificacaoUsuarioRepository(IConfiguration configuration) : GenericRepository<NotificacaoUsuario>(configuration)
{
    public IList<NotificacaoUsuario> ObterPorUsuario(int idUsuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT *
                                    FROM NotificacaoUsuario
                                    WHERE IdUsuario = @idUsuario
                                    ORDER BY DataCadastro DESC";

        return [.. conexao.Query<NotificacaoUsuario>(sqlCommand, new { idUsuario })];
    }

    public int ObterQuantidadeNaoLidas(int idUsuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT COUNT(*)
                                    FROM NotificacaoUsuario
                                    WHERE IdUsuario = @idUsuario
                                        AND Lida = 0";

        return conexao.ExecuteScalar<int>(sqlCommand, new { idUsuario });
    }

    public void MarcarComoLida(int id, int idUsuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"UPDATE NotificacaoUsuario
                                    SET Lida = 1
                                    WHERE Id = @id
                                        AND IdUsuario = @idUsuario";

        conexao.Execute(sqlCommand, new { id, idUsuario });
    }

    public void MarcarTodasComoLidas(int idUsuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"UPDATE NotificacaoUsuario
                                    SET Lida = 1
                                    WHERE IdUsuario = @idUsuario
                                        AND Lida = 0";

        conexao.Execute(sqlCommand, new { idUsuario });
    }
}
