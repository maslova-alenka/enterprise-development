namespace Polyclinic.Repositories;

/// <summary>
/// Generic repository interface for data access operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
/// <typeparam name="TKey">Entity identifier type</typeparam>
public interface IRepository<T, TKey>
{
    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="entity">Entity to create</param>
    void Create(T entity);

    /// <summary>
    /// Deletes an entity by identifier
    /// </summary>
    /// <param name="entityId">Entity identifier</param>
    void Delete(TKey entityId);

    /// <summary>
    /// Retrieves an entity by identifier
    /// </summary>
    /// <param name="entityId">Entity identifier</param>
    /// <returns>Entity if found</returns>
    T Read(TKey entityId);

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    List<T> ReadAll();

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity">Entity with updated data</param>
    void Update(T entity);
}