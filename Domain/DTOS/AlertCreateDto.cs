namespace FloodWatch.Domain.DTOS
{
    public class AlertCreateDto
    {
        public Guid SensorId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
    }
}
