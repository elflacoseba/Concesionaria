using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregoColumnaNoLeidaConsultaContacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoLeida",
                table: "ConsultasContacto",
                type: "bit",
                nullable: false,
                defaultValue: true)
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoLeida",
                table: "ConsultasContacto");
        }
    }
}
