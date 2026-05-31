using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Model.DTO;
using Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class InformacaoCandidatoRepository(IConfiguration configuration): GenericRepository<InformacaoCandidato>(configuration)
    {
        public InformacaoCandidato? ObterInformacoesPorUsuario(int idUsuario)
        {
            using var conexao = CriarConexao();

            const string sqlCommand = @"SELECT IC.*
                                        FROM InformacaoCandidato IC
                                        LEFT JOIN Candidato C
                                            ON C.Id = IC.IdCandidato
                                        WHERE C.IdUsuario = @idUsuario;";

            return conexao.QuerySingleOrDefault<InformacaoCandidato>(sqlCommand, new { idUsuario });
        }
    }
}
