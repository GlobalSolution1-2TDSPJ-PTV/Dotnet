using FloodWatch.Domain.DTOS;
using FloodWatch.Domain.Entities;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FloodWatch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly IRepository<Alert> _alertRepository;

        public AlertController(IRepository<Alert> alertRepository)
        {
            _alertRepository = alertRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertResponseDto>>> GetAll()
        {
            var alerts = await _alertRepository.GetAllAsync();

            var response = alerts.Select(a => new AlertResponseDto
            {
                Id = a.Id,
                SensorId = a.SensorId,
                Type = a.Type,
                Message = a.Message,
                Level = a.Level,
                CreatedAt = a.CreatedAt,
                IsResolved = a.IsResolved
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlertResponseDto>> GetById(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null) return NotFound();

            var dto = new AlertResponseDto
            {
                Id = alert.Id,
                SensorId = alert.SensorId,
                Type = alert.Type,
                Message = alert.Message,
                Level = alert.Level,
                CreatedAt = alert.CreatedAt,
                IsResolved = alert.IsResolved
            };

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, AlertCreateDto dto)
        {
            var existing = await _alertRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Type = dto.Type;
            existing.Message = dto.Message;
            existing.Level = dto.Level;
            existing.IsResolved = false;

            await _alertRepository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null) return NotFound();

            await _alertRepository.DeleteAsync(alert);
            return NoContent();
        }
    }
}
