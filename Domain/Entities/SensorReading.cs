namespace FloodWatch.Domain.Entities
{
    public class SensorReading
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public double SensorValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public Sensor Sensor { get; set; }
    }
}
