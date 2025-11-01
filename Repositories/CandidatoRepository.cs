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
    public override int Adicionar(Candidato candidato)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"INSERT INTO Candidato (IdUsuario, IdEmpresa, Nome, Email, Cpf)
                                    OUTPUT INSERTED.Id
                                    VALUES (@IdUsuario, @IdEmpresa, @Nome, @Email, @Cpf)";

        return conexao.ExecuteScalar<int>(sqlCommand, candidato);
    }
}
