using EventosApi.Models;

namespace EventosApi.Repositories.Events
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetById(Guid id);
        Task<Event> Create(Event category);
        Task<Event> Update(Event category);
        Task<Event> Delete(Guid id);
    }
}
