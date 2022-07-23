using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace n5.webApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoPermisos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "Unique ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Permission description")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPermisos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false, comment: "Unique ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Employee Forename"),
                    ApellidoEmpleado = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Employee Surname"),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    FechaPermiso = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Permission granted on Date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permisos_TipoPermisos_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "TipoPermisos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoPermisos",
                columns: new[] { "ID", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Seguridad" },
                    { 2, "Medios tecnologicos" },
                    { 3, "Contabilidad" },
                    { 4, "Administraciond" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_PermissionTypeId",
                table: "Permisos",
                column: "PermissionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "TipoPermisos");
        }
    }
}
