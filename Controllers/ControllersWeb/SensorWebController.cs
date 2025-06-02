using FloodWatch.Domain.DTOS;
using FloodWatch.Domain.Entities;
using FloodWatch.Domain.Enums;
using FloodWatch.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FloodWatch.Controllers.ControllersWeb
{
    public class SensorWebController : Controller
    {
        private readonly IRepository<Sensor> _sensorRepository;

        public SensorWebController(IRepository<Sensor> sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.SensorTypes = Enum.GetValues(typeof(SensorType))
                .Cast<SensorType>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.GetType()
                        .GetMember(e.ToString())
                        .First()
                        .GetCustomAttributes(false)
                        .OfType<DisplayAttribute>()
                        .FirstOrDefault()?.Name ?? e.ToString()
                }).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(SensorCreateDto dto)
        {
            var sensor = new Sensor
            {
                Id = Guid.NewGuid(),
                Type = Enum.Parse<Domain.Enums.SensorType>(dto.Type),
                Localizacao = dto.Localizacao,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IsActive = dto.IsActive,
                Alerts = [],
                Readings = []
            };

            await _sensorRepository.AddAsync(sensor);
            return RedirectToAction("Success");
        }

        public IActionResult Success() => Content("Sensor created successfully.");
    }
}
