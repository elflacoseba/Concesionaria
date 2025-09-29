using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concesionaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdenarConsultasContactoDescendente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ConsultasContacto_FechaEnvio_Desc",
                table: "ConsultasContacto",
                column: "FechaEnvio",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsultasContacto_FechaEnvio_Desc",
                table: "ConsultasContacto");
        }
    }
}
