using FloodWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodWatch.Infrastructure.Mappings;

public class SensorReadingMapping : IEntityTypeConfiguration<SensorReading>
{
    public void Configure(EntityTypeBuilder<SensorReading> builder)
    {
        builder.ToTable("SensorReadings");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.SensorValue)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.HasOne(r => r.Sensor)
            .WithMany(s => s.Readings)
            .HasForeignKey(r => r.SensorId);
    }
}
