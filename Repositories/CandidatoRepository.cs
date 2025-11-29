using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;
using Repositories.Generico.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public class CandidatoRepository(IConfiguration configuration) : GenericRepository<Candidato>(configuration)
{
    public Candidato ObterCandidatoPorIdUsuario(int idUsuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = "SELECT * FROM Candidato WHERE IdUsuario = @idUsuario";

        return conexao.QuerySingle<Candidato>(sqlCommand, new { idUsuario });
    }
}
