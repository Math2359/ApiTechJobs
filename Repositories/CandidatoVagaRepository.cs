using Azure.Core;
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

public class CandidatoVagaRepository(IConfiguration configuration) : GenericRepository<CandidatoVaga>(configuration)
{
    public IList<CandidatoVagaDTO> ObterAplicacoes(int idVaga)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"SELECT C.Nome, C.Email FROM CandidatoVaga AS CV
                                    LEFT JOIN Candidato AS C
                                    ON C.Id = CV.IdCandidato
                                    WHERE IdVaga = @idVaga";


        return [.. conexao.Query<CandidatoVagaDTO>(sqlCommand, new { idVaga })];
    }
}