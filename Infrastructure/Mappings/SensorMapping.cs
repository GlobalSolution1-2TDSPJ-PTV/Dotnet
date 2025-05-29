using FloodWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodWatch.Infrastructure.Mappings
{
    public class SensorMapping : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Sensors");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(s => s.Localizacao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Latitude)
                .IsRequired();

            builder.Property(s => s.Longitude)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.HasMany(s => s.Readings)
                .WithOne(r => r.Sensor)
                .HasForeignKey(r => r.SensorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Alerts)
                .WithOne(a => a.Sensor)
                .HasForeignKey(a => a.SensorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
