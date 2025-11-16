namespace Polyclinic.Repositories;

public interface IRepository<T, TKey>
{
    void Create(T entity);
    void Delete(TKey entityId);
    T Read(TKey entityId);
    List<T> ReadAll();
    void Update(T entity);
}