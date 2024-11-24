using AutoMapper;
using EventosApi.Dtos;
using EventosApi.Models;
using EventosApi.Repositories.Enterprises;

namespace EventosApi.Services.Enterprises
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IMapper _mapper;
        public EnterpriseService(IEnterpriseRepository EnterpriseRepository, IMapper mapper)
        {
            _enterpriseRepository = EnterpriseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnterpriseDto>> GetEnterprises()
        {
            var enterprisesEntity = await _enterpriseRepository.GetAll();
            return _mapper.Map<IEnumerable<EnterpriseDto>>(enterprisesEntity);
        }
        public async Task<EnterpriseDto> GetEnterpriseById(Guid id)
        {
            var enterpriseEntity = await _enterpriseRepository.GetById(id);
            return _mapper.Map<EnterpriseDto>(enterpriseEntity);
        }
        public async Task AddEnterprise(EnterpriseDto enterpriseDto)
        {
            var enterpriseEntity = _mapper.Map<Enterprise>(enterpriseDto);
            await _enterpriseRepository.Create(enterpriseEntity);
            enterpriseDto.Id = enterpriseEntity.Id;
        }
        public async Task UpdateEnterprise(EnterpriseDto enterpriseDto)
        {
            var enterpriseEntity = _mapper.Map<Enterprise>(enterpriseDto);
            await _enterpriseRepository.Update(enterpriseEntity);
        }
        public async Task RemoveEnterprise(Guid id)
        {
            var enterpriseEntity = _enterpriseRepository.GetById(id).Result;
            await _enterpriseRepository.Delete(enterpriseEntity.Id);
        }
    }
}

