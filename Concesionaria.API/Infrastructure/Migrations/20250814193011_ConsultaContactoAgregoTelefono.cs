using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConsultaContactoAgregoTelefono : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "ConsultasContacto",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "ConsultasContacto");
        }
    }
}
