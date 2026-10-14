using Microsoft.EntityFrameworkCore;
using UberRideBooking.Data;
using UberRideBooking.REPOs.Interfaces;

namespace UberRideBooking.REPOs.Implementaions
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
             _context.Set<T>().Remove(_context.Set<T>().Find(id));
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
