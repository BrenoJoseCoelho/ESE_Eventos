
using EventosApi.Dtos;
using EventosApi.Services.Enterprises;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseService _EnterpriseService;

        public EnterpriseController(IEnterpriseService EnterpriseService)
        {
            _EnterpriseService = EnterpriseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnterpriseDto>>> Get()
        {
            var EnterprisesDto = await _EnterpriseService.GetEnterprises();

            if (EnterprisesDto is null)
                return NotFound("Enterprises not found");

            return Ok(EnterprisesDto);
        }

        [HttpGet("{id:Guid}", Name = "GetEnterprise")]
        public async Task<ActionResult<EnterpriseDto>> Get(Guid id)
        {
            var EnterpriseDto = await _EnterpriseService.GetEnterpriseById(id);
            if (EnterpriseDto == null)
            {
                return NotFound("Enterprise not found");
            }
            return Ok(EnterpriseDto);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EnterpriseDto EnterpriseDto)
        {
            if (EnterpriseDto == null)
                return BadRequest("Invalid Data");

            await _EnterpriseService.AddEnterprise(EnterpriseDto);

            return new CreatedAtRouteResult("GetEnterprise", new { id = EnterpriseDto.Id },
                EnterpriseDto);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] EnterpriseDto EnterpriseDto)
        {
            if (id != EnterpriseDto.Id)
                return BadRequest();

            if (EnterpriseDto is null)
                return BadRequest();

            await _EnterpriseService.UpdateEnterprise(EnterpriseDto);

            return Ok(EnterpriseDto);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<EnterpriseDto>> Delete(Guid id)
        {
            var EnterpriseDto = await _EnterpriseService.GetEnterpriseById(id);
            if (EnterpriseDto == null)
            {
                return NotFound("Enterprise not found");
            }

            await _EnterpriseService.RemoveEnterprise(id);

            return Ok(EnterpriseDto);
        }
    }
}
