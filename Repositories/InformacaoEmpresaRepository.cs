using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using Repositories.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class InformacaoEmpresaRepository(IConfiguration configuration) : GenericRepository<InformacaoEmpresa>(configuration)
    {
        public InformacaoEmpresa? ObterInformacoesPorUsuario(int idUsuario)
        {
            using var conexao = CriarConexao();

            const string sqlCommand = @"SELECT IP.*
                                        FROM InformacaoEmpresa IP
                                        LEFT JOIN Empresa E
                                            ON E.Id = IP.IdEmpresa
                                        WHERE E.IdUsuario = @idUsuario;";

            return conexao.QuerySingleOrDefault<InformacaoEmpresa>(sqlCommand, new { idUsuario });
        }

        public InformacaoEmpresa? ObterInformacoesPorIdEmpresa(int idEmpresa)
        {
            using var conexao = CriarConexao();

            const string sqlCommand = "SELECT * FROM InformacaoEmpresa WHERE IdEmpresa = @idEmpresa";

            return conexao.QuerySingleOrDefault<InformacaoEmpresa>(sqlCommand, new { idEmpresa });
        }

        public override void Editar(InformacaoEmpresa obj)
        {
            using var conexao = CriarConexao();

            const string sqlCommand = @"UPDATE InformacaoEmpresa
                    SET Descricao = @Descricao,
                        Setor = @Setor,
                        Tecnologias = @Tecnologias,
                        LinkSite = @LinkSite
                    WHERE IdEmpresa = @IdEmpresa;";

            conexao.Execute(sqlCommand, obj);
        }
    }
}
