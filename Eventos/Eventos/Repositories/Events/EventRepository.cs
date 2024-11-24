using EventosApi.Context;
using EventosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventosApi.Repositories.Events;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;
    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        return await _context.Events.Include(e => e.Enterprise).ToListAsync();

    }
    public async Task<Event> GetById(Guid id)
    {
        return await _context.Events.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Event> Create(Event Event)
    {
        _context.Events.Add(Event);
        await _context.SaveChangesAsync();
        return Event;
    }
    public async Task<Event> Update(Event Event)
    {
        _context.Entry(Event).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Event;
    }
    public async Task<Event> Delete(Guid id)
    {
        var Event = await GetById(id);
        _context.Events.Remove(Event);
        await _context.SaveChangesAsync();
        return Event;
    }
}
