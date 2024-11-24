using EventosApi.Dtos;

namespace EventosApi.Services.Enterprises
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<EnterpriseDto>> GetEnterprises();
        Task<EnterpriseDto> GetEnterpriseById(Guid id);
        Task AddEnterprise(EnterpriseDto enterpriseDto);
        Task UpdateEnterprise(EnterpriseDto enterpriseDto);
        Task RemoveEnterprise(Guid id);
    }
}
