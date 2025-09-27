using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregoCamposVehiculosAConsultaContacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehiculoInteres",
                table: "ConsultasContacto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<string>(
                name: "VehiculoPermuta",
                table: "ConsultasContacto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiculoInteres",
                table: "ConsultasContacto");

            migrationBuilder.DropColumn(
                name: "VehiculoPermuta",
                table: "ConsultasContacto");
        }
    }
}
