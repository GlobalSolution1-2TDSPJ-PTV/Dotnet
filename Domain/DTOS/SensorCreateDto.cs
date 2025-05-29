namespace FloodWatch.Domain.DTOS
{
    public class SensorCreateDto
    {
        public string Type { get; set; }
        public string Localizacao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }
    }

}
