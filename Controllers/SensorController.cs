﻿using FloodWatch.Domain.DTOS;
using FloodWatch.Domain.Entities;
using FloodWatch.Domain.Enums;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FloodWatch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly IRepository<Sensor> _sensorRepository;

        public SensorController(IRepository<Sensor> sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SensorResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SensorResponseDto>>> GetAll()
        {
            var sensors = await _sensorRepository.GetAllAsync();

            var response = sensors.Select(s => new SensorResponseDto
            {
                Id = s.Id,
                Type = s.Type.ToString(),
                Localizacao = s.Localizacao,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                IsActive = s.IsActive
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SensorResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorResponseDto>> GetById(Guid id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null) return NotFound();

            var dto = new SensorResponseDto
            {
                Id = sensor.Id,
                Type = sensor.Type.ToString(),
                Localizacao = sensor.Localizacao,
                Latitude = sensor.Latitude,
                Longitude = sensor.Longitude,
                IsActive = sensor.IsActive
            };

            return Ok(dto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(SensorCreateDto dto)
        {
            var sensor = new Sensor
            {
                Id = Guid.NewGuid(),
                Type = Enum.Parse<SensorType>(dto.Type),
                Localizacao = dto.Localizacao,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IsActive = dto.IsActive,
                Alerts = new List<Alert>(),
                Readings = new List<SensorReading>()
            };

            await _sensorRepository.AddAsync(sensor);
            return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(Guid id, SensorCreateDto dto)
        {
            var existing = await _sensorRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Type = Enum.Parse<SensorType>(dto.Type);
            existing.Localizacao = dto.Localizacao;
            existing.Latitude = dto.Latitude;
            existing.Longitude = dto.Longitude;
            existing.IsActive = dto.IsActive;

            await _sensorRepository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null) return NotFound();

            await _sensorRepository.DeleteAsync(sensor);
            return NoContent();
        }
    }
}