using FloodWatch.Domain.Entities;
using FloodWatch.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FloodWatch.Infrastructure.Context
{
    public class FloodWatchContext(DbContextOptions<FloodWatchContext> options) : DbContext(options)
    {

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SensorMapping());
            modelBuilder.ApplyConfiguration(new SensorReadingMapping());
            modelBuilder.ApplyConfiguration(new AlertMapping());
        }
    }
}
