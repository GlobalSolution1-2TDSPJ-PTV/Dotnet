using FloodWatch.Domain.Enums;

namespace FloodWatch.Domain.Entities
{
    public class Sensor
    {
        public Guid Id { get; set; }
        public SensorType Type { get; set; }
        public string Localizacao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }
        public ICollection<SensorReading> Readings { get; set; }
        public ICollection<Alert> Alerts { get; set; }
    }
}
