using FloodWatch.Domain.DTOS;
using FloodWatch.Domain.Entities;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FloodWatch.Controllers.ControllersWeb
{ 
    public class SensorReadingController : Controller
    {
        private readonly IRepository<SensorReading> _readingRepository;

        public SensorReadingController(IRepository<SensorReading> readingRepository)
        {
            _readingRepository = readingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var readings = await _readingRepository.GetAllAsync();
            var response = readings.Select(r => new SensorReadingResponseDto
            {
                Id = r.Id,
                SensorId = r.SensorId,
                SensorValue = r.SensorValue,
                CreatedAt = r.CreatedAt
            });

            return View(response);
        }
    }
}