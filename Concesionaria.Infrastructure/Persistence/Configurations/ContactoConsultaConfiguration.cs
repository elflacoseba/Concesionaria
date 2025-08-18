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

            builder.Property(v => v.Id)
                .HasColumnOrder(0);

            builder.Property(v => v.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnOrder(1);

            builder.Property(v => v.Apellido)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnOrder(2);

            builder.Property(v => v.Email)
                .IsRequired()               
                .HasMaxLength(100)
                .HasColumnOrder(3);

            builder.Property(v => v.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnOrder(4);

            builder.Property(v => v.Mensaje)
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnOrder(5);

            builder.Property(v => v.FechaEnvio)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnOrder(6);

            builder.Property(v => v.NoLeida)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .HasColumnOrder(7);
        }
    }
}
