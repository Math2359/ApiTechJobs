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
    public class ExperienciaCandidatoRepository(IConfiguration configuration): GenericRepository<ExperienciaCandidato>(configuration)
    {
        static ExperienciaCandidatoRepository()
        {
            OutputColumn = "IdCandidato";
        }

        public IEnumerable<ExperienciaCandidatoDTO> ObterExperienciasCandidato(int idUsuario)
        {
            using var conexao = CriarConexao();

            const string sqlCommand = @"SELECT EC.* FROM ExperienciaCandidato AS EC
                                    LEFT JOIN Candidato AS C
                                    ON C.Id = EC.IdCandidato
                                    WHERE C.IdUsuario = @idUsuario
                                    ORDER BY TipoExperiencia, DataInicio DESC";

            return conexao.Query<ExperienciaCandidatoDTO>(sqlCommand, new { idUsuario });
        }

        public override void Excluir(int idCandidato)
        {
            using var conexao = CriarConexao();

            const string sqlCommand = "DELETE FROM ExperienciaCandidato WHERE IdCandidato = @idCandidato";

            conexao.Execute(sqlCommand, new { idCandidato });
        }
    }
}
