namespace FloodWatch.Domain.DTOS
{
    public class SensorReadingResponseDto
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public double SensorValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
