using FloodWatch.Domain.DTOS;
using FloodWatch.Domain.Entities;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FloodWatch.Controllers
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
        [ProducesResponseType(typeof(IEnumerable<AlertResponseDto>), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(AlertResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(Guid id, AlertCreateDto dto)
        {
            var existing = await _alertRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Type = dto.Type;
            existing.Message = dto.Message;
            existing.Level = dto.Level;

            await _alertRepository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpPut("{id}/resolve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> MarkAsResolved(Guid id)
        {
            var existing = await _alertRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.IsResolved = true;
            await _alertRepository.UpdateAsync(existing);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null) return NotFound();

            await _alertRepository.DeleteAsync(alert);
            return NoContent();
        }
    }
}