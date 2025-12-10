namespace Polyclinic.Domain.Interfaces;

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
    Task CreateAsync(T entity);

    /// <summary>
    /// Deletes an entity by identifier
    /// </summary>
    /// <param name="entityId">Entity identifier</param>
    Task DeleteAsync(TKey entityId);

    /// <summary>
    /// Retrieves an entity by identifier
    /// </summary>
    /// <param name="entityId">Entity identifier</param>
    /// <returns>Entity if found</returns>
    Task<T?> ReadAsync(TKey entityId);

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<List<T>> ReadAllAsync();

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity">Entity with updated data</param>
    Task UpdateAsync(T entity);
}