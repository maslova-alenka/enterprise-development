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
    TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Retrieves an entity by identifier
    /// </summary>
    /// <param name="dtoId">Entity identifier</param>
    /// <returns>Entity if found</returns>
    TDto? Get(TKey dtoId);

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    List<TDto> GetAll();

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="dto">Data for updating the entity</param>
    /// <param name="dtoId">Entity identifier</param>
    /// <returns>Updated entity</returns>
    TDto Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Deletes an entity by identifier
    /// </summary>
    /// <param name="dtoId">Entity identifier</param>
    /// <returns>True if deletion was successful</returns>
    bool Delete(TKey dtoId);
}