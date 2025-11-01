using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Repositories.Generico.Interface;

namespace Repositories.Generico;

public class GenericRepository<T>(IConfiguration configuration) : IGenericRepository<T>
{
    private readonly string _connectionString = configuration.GetConnectionString("TechJobs") ?? "";

    protected readonly string TableName = typeof(T).Name;

    protected SqlConnection CriarConexao() => new(_connectionString);

    public virtual int Adicionar(T obj)
    {
        throw new NotImplementedException("Deve ser implementado no repositório específico.");
    }

    public virtual void Editar(T obj)
    {
        throw new NotImplementedException("Deve ser implementado no repositório específico.");
    }

    public virtual void Excluir(int id)
    {
        using var conexao = CriarConexao();

        string sqlCommand = $"DELETE FROM [{TableName}] WHERE Id = @id";

        conexao.Execute(sqlCommand, new { id });
    }

    public virtual T? ObterPorId(int id)
    {
        using var conexao = CriarConexao();

        string sqlCommand = $"SELECT * FROM [{TableName}] WHERE Id = @id";

        return conexao.QuerySingleOrDefault<T>(sqlCommand, new { id });
    }

    public virtual IList<T> ObterTodos()
    {
        using var conexao = CriarConexao();

        string sqlCommand = $"SELECT * FROM [{TableName}]";

        return [..conexao.Query<T>(sqlCommand)];
    }
}
