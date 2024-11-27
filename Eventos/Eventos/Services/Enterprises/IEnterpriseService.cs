using EventosApi.Dtos;

namespace EventosApi.Services.Enterprises
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<EnterpriseDto>> GetEnterprises(); // Lista de empresas
        Task<EnterpriseDto> GetEnterpriseById(Guid id); // Buscar empresa por ID
        Task AddEnterprise(EnterpriseDto enterpriseDto); // Adicionar nova empresa
        Task UpdateEnterprise(EnterpriseDto enterpriseDto); // Atualizar empresa existente
        Task<EnterpriseDto> RemoveEnterprise(Guid id); // Remover empresa por ID
    }
}
