using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Enum;
using Repositories.Generico;

namespace Repositories;

public class AgendamentoEntrevistaRepository(IConfiguration configuration) : GenericRepository<AgendamentoEntrevista>(configuration)
{
    public bool Agendar(AgendamentoEntrevista agendamento, DateTime dataAtualizacao)
    {
        using var conexao = CriarConexao();
        conexao.Open();

        using var transacao = conexao.BeginTransaction();

        const string atualizarAplicacao = @"UPDATE CandidatoVaga
                                            SET Situacao = @situacao,
                                                DataAtualizacao = @dataAtualizacao
                                            WHERE Id = @idAplicacao";

        var aplicacaoAtualizada = conexao.Execute(atualizarAplicacao, new
        {
            situacao = EnumSituacao.Entrevista,
            dataAtualizacao,
            idAplicacao = agendamento.IdAplicacao
        }, transacao) > 0;

        if (!aplicacaoAtualizada)
        {
            transacao.Rollback();
            return false;
        }

        const string atualizarAgendamento = @"UPDATE AgendamentoEntrevista
                                              SET Data = @Data,
                                                  Hora = @Hora,
                                                  Local = @Local,
                                                  Observacao = @Observacao
                                              WHERE IdAplicacao = @IdAplicacao";

        var agendamentoAtualizado = conexao.Execute(atualizarAgendamento, agendamento, transacao) > 0;

        if (!agendamentoAtualizado)
        {
            const string adicionarAgendamento = @"INSERT INTO AgendamentoEntrevista
                                                  (IdAplicacao, Data, Hora, Local, Observacao)
                                                  VALUES
                                                  (@IdAplicacao, @Data, @Hora, @Local, @Observacao)";

            conexao.Execute(adicionarAgendamento, agendamento, transacao);
        }

        transacao.Commit();
        return true;
    }

    public AgendamentoEntrevista? ObterPorIdAplicacao(int idAplicacao)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT TOP 1 *
                                    FROM AgendamentoEntrevista
                                    WHERE IdAplicacao = @idAplicacao";

        return conexao.QuerySingleOrDefault<AgendamentoEntrevista>(sqlCommand, new { idAplicacao });
    }
}
