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

public class EmpresaRepository(IConfiguration configuration) : GenericRepository<Empresa>(configuration)
{
    public override int Adicionar(Empresa empresa)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = @"INSERT INTO Empresa (IdUsuario, Nome, Email, Cnpj, Cep, Numero)
                                    OUTPUT INSERTED.Id
                                    VALUES (@IdUsuario, @Nome, @Email, @Cnpj, @Cep, @Numero)";

        return conexao.ExecuteScalar<int>(sqlCommand, empresa);
    }
}
