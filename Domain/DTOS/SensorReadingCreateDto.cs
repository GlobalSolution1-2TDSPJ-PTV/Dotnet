namespace FloodWatch.Domain.DTOS
{
    public class SensorReadingCreateDto
    {
        public Guid SensorId { get; set; }
        public double SensorValue { get; set; }
    }
}
