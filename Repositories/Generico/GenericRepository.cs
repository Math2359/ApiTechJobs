using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using Repositories.Generico.Interface;
using Utils;

namespace Repositories.Generico;

public class GenericRepository<T>(IConfiguration configuration) : IGenericRepository<T>
{
    private readonly string _connectionString = configuration.GetConnectionString("TechJobs") ?? "";

    private readonly string obterPorIdQuery = $"SELECT * FROM [{typeof(T).Name}] WHERE Id = @id";

    private readonly string obterTodosQuery = $"SELECT * FROM [{typeof(T).Name}]";

    private readonly string deleteQuery = $"DELETE FROM [{typeof(T).Name}] WHERE Id = @id";

    private readonly string insertQuery = SqlHelper.GerarInsertQuery<T>();

    protected SqlConnection CriarConexao() => new(_connectionString);

    public virtual int Adicionar(T obj)
    {
        using var conexao = CriarConexao();

        return conexao.ExecuteScalar<int>(insertQuery, obj);
    }

    public virtual void Editar(T obj)
    {
        throw new NotImplementedException("Deve ser implementado no repositório específico.");
    }

    public virtual void Excluir(int id)
    {
        using var conexao = CriarConexao();

        conexao.Execute(deleteQuery, new { id });
    }

    public virtual T? ObterPorId(int id)
    {
        using var conexao = CriarConexao();

        return conexao.QuerySingleOrDefault<T>(obterPorIdQuery, new { id });
    }

    public virtual IList<T> ObterTodos()
    {
        using var conexao = CriarConexao();

        return [..conexao.Query<T>(obterTodosQuery)];
    }
}
