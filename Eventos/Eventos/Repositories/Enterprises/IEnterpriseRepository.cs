using EventosApi.Models;

namespace EventosApi.Repositories.Enterprises
{
    public interface IEnterpriseRepository
    {
        Task<IEnumerable<Enterprise>> GetAll();
        Task<Enterprise> GetById(Guid id);
        Task<Enterprise> Create(Enterprise category);
        Task<Enterprise> Update(Enterprise category);
        Task<Enterprise> Delete(Guid id);
    }
}
