using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Models;
using Polyclinic.Infrastructure.EfCore;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

public class SpecializationEfCoreRepository : IRepository<Specialization, int>
{
    private readonly PolyclinicDbContext _context;
    private readonly DbSet<Specialization> _specializations;

    public SpecializationEfCoreRepository(PolyclinicDbContext context)
    {
        _context = context;
        _specializations = context.Specializations;
    }

    public void Create(Specialization entity)
    {
        _specializations.Add(entity);
        _context.SaveChanges();
    }

    public void Delete(int entityId)
    {
        var entity = Read(entityId);
        if (entity != null)
        {
            _specializations.Remove(entity);
            _context.SaveChanges();
        }
    }

    public Specialization? Read(int entityId)
    {
        return _specializations
            .FirstOrDefault(s => s.Id == entityId);
    }

    public List<Specialization> ReadAll()
    {
        return _specializations
            .OrderBy(s => s.Id)
            .ToList();
    }

    public void Update(Specialization entity)
    {
        _specializations.Update(entity);
        _context.SaveChanges();
    }
}