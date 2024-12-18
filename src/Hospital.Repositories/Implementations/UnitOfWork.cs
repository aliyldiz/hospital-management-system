using Hospital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private bool disposed = false;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IGenericRepository<T> GenericRepository<T>() where T : class
    {
        IGenericRepository<T> repository = new GenericRepository<T>(_context);
        return repository;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
}