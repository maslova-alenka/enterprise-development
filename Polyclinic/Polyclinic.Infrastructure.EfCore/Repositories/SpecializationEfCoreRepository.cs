using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Repository for Specialization entities with CRUD operations.
/// Uses explicit DbSet for better separation of concerns.
/// </summary>
public class SpecializationEfCoreRepository : IRepository<Specialization, int>
{
    private readonly PolyclinicDbContext _context;
    private readonly DbSet<Specialization> _specializations;

    public SpecializationEfCoreRepository(PolyclinicDbContext context)
    {
        _context = context;
        _specializations = context.Specializations;
    }

    /// <summary>
    /// Creates new specialization in database.
    /// </summary>
    public void Create(Specialization entity)
    {
        _specializations.Add(entity);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deletes specialization by ID if exists.
    /// </summary>
    public void Delete(int entityId)
    {
        var entity = Read(entityId);
        if (entity != null)
        {
            _specializations.Remove(entity);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Gets specialization by ID.
    /// </summary>
    public Specialization? Read(int entityId)
    {
        return _specializations.FirstOrDefault(s => s.Id == entityId);
    }

    /// <summary>
    /// Gets all specializations ordered by ID.
    /// </summary>
    public List<Specialization> ReadAll()
    {
        return _specializations.OrderBy(s => s.Id).ToList();
    }

    /// <summary>
    /// Updates existing specialization.
    /// </summary>
    public void Update(Specialization entity)
    {
        _specializations.Update(entity);
        _context.SaveChanges();
    }
}