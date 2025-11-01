namespace Repositories.Generico.Interface;

public interface IGenericRepository<T>
{
    T? ObterPorId(int id);
    IList<T> ObterTodos();
    void Excluir(int id);
    void Editar(T obj);
    int Adicionar(T obj);
}
