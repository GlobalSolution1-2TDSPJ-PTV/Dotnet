using FloodWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodWatch.Infrastructure.Mappings
{
    public class AlertMapping : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.ToTable("Alerts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Message)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Level)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.IsResolved)
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.HasOne(a => a.Sensor)
                .WithMany(s => s.Alerts)
                .HasForeignKey(a => a.SensorId);
        }
    }
}
