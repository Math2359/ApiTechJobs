using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;

namespace Repositories;

public class ValidacaoEmailRepository(IConfiguration configuration) : GenericRepository<ValidacaoEmail>(configuration)
{
    public override int Adicionar(ValidacaoEmail validacaoEmail)
    {
        using var conexao = CriarConexao();
        conexao.Open();

        using var transacao = conexao.BeginTransaction();

        const string invalidarTokensAnteriores = @"UPDATE ValidacaoEmail
                                                   SET DataValidacao = @dataValidacao
                                                   WHERE IdUsuario = @idUsuario
                                                       AND DataValidacao IS NULL";

        conexao.Execute(invalidarTokensAnteriores, new
        {
            validacaoEmail.IdUsuario,
            dataValidacao = validacaoEmail.DataCriacao
        }, transacao);

        const string adicionarToken = @"INSERT INTO ValidacaoEmail
                                        (IdUsuario, TokenHash, DataExpiracao, DataCriacao, DataValidacao)
                                        OUTPUT INSERTED.Id
                                        VALUES
                                        (@IdUsuario, @TokenHash, @DataExpiracao, @DataCriacao, @DataValidacao)";

        var id = conexao.ExecuteScalar<int>(adicionarToken, validacaoEmail, transacao);

        transacao.Commit();
        return id;
    }

    public ValidacaoEmail? ObterPorTokenHash(string tokenHash)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT TOP 1 *
                                    FROM ValidacaoEmail
                                    WHERE TokenHash = @tokenHash
                                    ORDER BY DataCriacao DESC";

        return conexao.QuerySingleOrDefault<ValidacaoEmail>(sqlCommand, new { tokenHash });
    }

    public bool Validar(string tokenHash, int idUsuario, DateTime dataValidacao)
    {
        using var conexao = CriarConexao();
        conexao.Open();

        using var transacao = conexao.BeginTransaction();

        const string validarToken = @"UPDATE ValidacaoEmail
                                      SET DataValidacao = @dataValidacao
                                      WHERE TokenHash = @tokenHash
                                          AND IdUsuario = @idUsuario
                                          AND DataValidacao IS NULL
                                          AND DataExpiracao >= @dataValidacao";

        var tokenValidado = conexao.Execute(validarToken, new
        {
            tokenHash,
            idUsuario,
            dataValidacao
        }, transacao) > 0;

        if (!tokenValidado)
        {
            transacao.Rollback();
            return false;
        }

        const string validarEmailUsuario = @"UPDATE Usuario
                                             SET EmailValidado = 1
                                             WHERE Id = @idUsuario";

        conexao.Execute(validarEmailUsuario, new { idUsuario }, transacao);

        transacao.Commit();
        return true;
    }
}
