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
    public Empresa ObterEmpresaPorIdUsuario(int idUsuario)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = "SELECT * FROM Empresa WHERE IdUsuario = @idUsuario";

        return conexao.QuerySingle<Empresa>(sqlCommand, new { idUsuario });
    }

    public Empresa? ObterEmpresaPorDocumento(string documento)
    {
        using var conexao = CriarConexao();

        const string sqlCommand = "SELECT * FROM Empresa WHERE Cnpj = @documento";

        return conexao.QuerySingleOrDefault<Empresa>(sqlCommand, new { documento });
    }
}
