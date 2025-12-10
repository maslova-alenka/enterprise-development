namespace Polyclinic.Contracts;

/// <summary>
/// Generic interface for application services with CRUD operations
/// </summary>
/// <typeparam name="TDto">Data transfer object type</typeparam>
/// <typeparam name="TCreateUpdateDto">Create and update data transfer object type</typeparam>
/// <typeparam name="TKey">Entity identifier type</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="dto">Data for creating the entity</param>
    /// <returns>Created entity</returns>
    Task<TDto> CreateAsync(TCreateUpdateDto dto);

    /// <summary>
    /// Retrieves an entity by identifier
    /// </summary>
    /// <param name="dtoId">Entity identifier</param>
    /// <returns>Entity if found</returns>
    Task<TDto?> GetAsync(TKey dtoId);

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<List<TDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="dto">Data for updating the entity</param>
    /// <param name="dtoId">Entity identifier</param>
    /// <returns>Updated entity</returns>
    Task<TDto> UpdateAsync(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Deletes an entity by identifier
    /// </summary>
    /// <param name="dtoId">Entity identifier</param>
    /// <returns>True if deletion was successful</returns>
    Task<bool> DeleteAsync(TKey dtoId);
}
