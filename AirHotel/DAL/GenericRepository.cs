using Microsoft.EntityFrameworkCore;
using AirHotel.Models;

namespace AirHotel.DAL;
public class GenericRepository<T> : IGenericRepository<T> where T : class

{
    private readonly HotelDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(HotelDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetEntitylById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Create(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item == null)
        {
            return false;
        }

        _dbSet.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}

