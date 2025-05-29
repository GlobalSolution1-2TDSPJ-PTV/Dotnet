using FloodWatch.Domain.DTOS;
using FloodWatch.Domain.Entities;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FloodWatch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorReadingController : ControllerBase
    {
        private readonly IRepository<SensorReading> _readingRepository;

        private readonly IRepository<Alert> _alertRepository;

        public SensorReadingController(IRepository<SensorReading> readingRepository, IRepository<Alert> alertRepository)
        {
            _readingRepository = readingRepository;
            _alertRepository = alertRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorReadingResponseDto>>> GetAll()
        {
            var readings = await _readingRepository.GetAllAsync();

            var response = readings.Select(r => new SensorReadingResponseDto
            {
                Id = r.Id,
                SensorId = r.SensorId,
                SensorValue = r.SensorValue,
                CreatedAt = r.CreatedAt
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorReadingResponseDto>> GetById(Guid id)
        {
            var reading = await _readingRepository.GetByIdAsync(id);
            if (reading == null) return NotFound();

            var dto = new SensorReadingResponseDto
            {
                Id = reading.Id,
                SensorId = reading.SensorId,
                SensorValue = reading.SensorValue,
                CreatedAt = reading.CreatedAt
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SensorReadingCreateDto dto)
        {
            var reading = new SensorReading
            {
                Id = Guid.NewGuid(),
                SensorId = dto.SensorId,
                SensorValue = dto.SensorValue,
                CreatedAt = DateTime.UtcNow
            };

            await _readingRepository.AddAsync(reading);

            if (reading.SensorValue > 1.5)
            {
                var alerta = new Alert
                {
                    Id = Guid.NewGuid(),
                    SensorId = reading.SensorId,
                    Type = "enchente",
                    Message = $"Nível da água crítico: {reading.SensorValue}m",
                    Level = "critico",
                    CreatedAt = DateTime.UtcNow,
                    IsResolved = false
                };

                await _alertRepository.AddAsync(alerta);
            }

            return CreatedAtAction(nameof(GetById), new { id = reading.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, SensorReadingCreateDto dto)
        {
            var existing = await _readingRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.SensorValue = dto.SensorValue;

            await _readingRepository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var reading = await _readingRepository.GetByIdAsync(id);
            if (reading == null) return NotFound();

            await _readingRepository.DeleteAsync(reading);
            return NoContent();
        }
    }
}
