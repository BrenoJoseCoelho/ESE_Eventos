using EventosApi.Context;
using EventosApi.Models;
using EventosApi.Repositories.Enterprises;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseosApi.Repositories.Enterprises
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly AppDbContext _context;
        public EnterpriseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enterprise>> GetAll()
        {
            return await _context.Enterprises.ToListAsync();
        }
        public async Task<Enterprise> GetById(Guid id)
        {
            return await _context.Enterprises.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Enterprise> Create(Enterprise Enterprise)
        {
            _context.Enterprises.Add(Enterprise);
            await _context.SaveChangesAsync();
            return Enterprise;
        }
        public async Task<Enterprise> Update(Enterprise Enterprise)
        {
            _context.Entry(Enterprise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Enterprise;
        }
        public async Task<Enterprise> Delete(Guid id)
        {
            var Enterprise = await GetById(id);
            _context.Enterprises.Remove(Enterprise);
            await _context.SaveChangesAsync();
            return Enterprise;
        }
    }
}
