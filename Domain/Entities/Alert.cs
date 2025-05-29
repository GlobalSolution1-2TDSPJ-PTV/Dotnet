using System.Globalization;

namespace FloodWatch.Domain.Entities
{
    public class Alert
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsResolved { get; set; }
        public Sensor Sensor { get; set; }
    }
}
