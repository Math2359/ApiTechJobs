using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Request;
using Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class VagaRepository(IConfiguration configuration) : GenericRepository<Vaga>(configuration)
{
    public IList<Vaga> ObterVagasPorIdUsuarioEmpresa(int idUsuarioEmpresa)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT V.* FROM Vaga AS V
                                    LEFT JOIN Empresa AS E
                                    ON V.IdEmpresa = E.Id
                                    WHERE IdUsuario = @idUsuarioEmpresa";

        return [..conexao.Query<Vaga>(sqlCommand, new { idUsuarioEmpresa })];
    }

    public IList<Vaga> ObterTodos(ObterTodasVagasRequest request)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT *
                                    FROM Vaga
                                    WHERE
                                        (NULLIF(@Cargo, '') IS NULL OR Cargo LIKE '%' + @Cargo + '%')
                                    AND (NULLIF(@NivelExperiencia, '') IS NULL OR NivelExperiencia LIKE '%' + @NivelExperiencia + '%')
                                    AND (NULLIF(@Modelo, '') IS NULL OR Modelo LIKE '%' + @Modelo + '%')
                                    AND (NULLIF(@CEP, '') IS NULL OR Cep LIKE '%' + @CEP + '%')";

        return [.. conexao.Query<Vaga>(sqlCommand, request)];
    }

    public Vaga? ObterVagaPorIdUsuarioEmpresa(int idVaga, int idUsuarioEmpresa)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT *
                                    FROM Vaga
                                    AS V
                                    JOIN Empresa AS E
                                    ON V.IdEmpresa = E.Id
                                    WHERE V.Id = @idVaga AND IdUsuario = @idUsuarioEmpresa
                                    AND IdEmpresa = @idUsuarioEmpresa";

        return conexao.QuerySingleOrDefault<Vaga>(sqlCommand, new { idVaga, idUsuarioEmpresa });
    }
}
