using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Request;
using Model.Response;
using Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class VagaRepository(IConfiguration configuration) : GenericRepository<Vaga>(configuration)
{
    public IList<VagaResponse> ObterVagasPorIdUsuarioEmpresa(int idUsuarioEmpresa)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT 
                                    V.*,
                                    (
                                        SELECT COUNT(*)
                                        FROM CandidatoVaga AS CV
                                        WHERE CV.IdVaga = V.Id
                                    ) AS QuantidadeAplicacoes
                                FROM Vaga AS V
                                LEFT JOIN Empresa AS E
                                    ON V.IdEmpresa = E.Id
                                WHERE E.IdUsuario = @idUsuarioEmpresa;
                                ";

        return [..conexao.Query<VagaResponse>(sqlCommand, new { idUsuarioEmpresa })];
    }

    public IList<VagaCandidatoResponse> ObterTodos(ObterTodasVagasRequest request)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT V.*, E.Nome AS NomeEmpresa
                                    FROM Vaga AS V
                                    LEFT JOIN Empresa AS E
                                    ON E.Id = V.IdEmpresa
                                    WHERE
                                        (
                                            NULLIF(@TermoBusca, '') IS NULL
                                            OR (
                                                Cargo LIKE '%' + @TermoBusca + '%'
                                                OR NivelExperiencia LIKE '%' + @TermoBusca + '%'
                                                OR Modelo LIKE '%' + @TermoBusca + '%'
                                                OR V.Cep LIKE '%' + @TermoBusca + '%'
                                            )
                                        )
                                        AND (
                                            @SalarioInicio IS NULL OR SalarioPrevisto >= @SalarioInicio
                                        ) AND (
                                            @SalarioFim IS NULL OR SalarioPrevisto <= @SalarioFim
                                        );
                                    ";

        return [.. conexao.Query<VagaCandidatoResponse>(sqlCommand, request)];
    }

    public Vaga? ObterVagaPorIdUsuarioEmpresa(int idVaga, int idUsuarioEmpresa)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT *
                                    FROM Vaga
                                    AS V
                                    JOIN Empresa AS E
                                    ON V.IdEmpresa = E.Id
                                    WHERE V.Id = @idVaga AND IdUsuario = @idUsuarioEmpresa";

        return conexao.QuerySingleOrDefault<Vaga>(sqlCommand, new { idVaga, idUsuarioEmpresa });
    }
}
