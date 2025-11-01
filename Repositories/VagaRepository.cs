using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
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
}
