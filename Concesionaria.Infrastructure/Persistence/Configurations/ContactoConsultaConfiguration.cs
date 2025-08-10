using Concesionaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concesionaria.Infrastructure.Persistence.Configurations
{
    public class ContactoConsultaConfiguration : IEntityTypeConfiguration<ConsultaContacto>
    {
        public void Configure(EntityTypeBuilder<ConsultaContacto> builder)
        {
            builder.ToTable("ConsultasContacto");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Email)
                .IsRequired()               
                .HasMaxLength(100);

            builder.Property(v => v.Mensaje)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(v => v.FechaEnvio)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
