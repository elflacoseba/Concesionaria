using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregoColumnaFechaLectura_ConsultaContacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLectura",
                table: "ConsultasContacto",
                type: "datetime2",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaLectura",
                table: "ConsultasContacto");
        }
    }
}
