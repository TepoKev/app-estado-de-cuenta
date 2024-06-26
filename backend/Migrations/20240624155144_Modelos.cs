using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Modelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuraciones",
                columns: table => new
                {
                    ConfiguracionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PorcentajeInteres = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentajeSaldoMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuraciones", x => x.ConfiguracionID);
                });

            migrationBuilder.CreateTable(
                name: "TarjetasDeCredito",
                columns: table => new
                {
                    TarjetaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTitular = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroTarjeta = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    SaldoActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LimiteCredito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoDisponible = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarjetasDeCredito", x => x.TarjetaID);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    CompraID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarjetaID = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.CompraID);
                    table.ForeignKey(
                        name: "FK_Compras_TarjetasDeCredito_TarjetaID",
                        column: x => x.TarjetaID,
                        principalTable: "TarjetasDeCredito",
                        principalColumn: "TarjetaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarjetaID = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagoID);
                    table.ForeignKey(
                        name: "FK_Pagos_TarjetasDeCredito_TarjetaID",
                        column: x => x.TarjetaID,
                        principalTable: "TarjetasDeCredito",
                        principalColumn: "TarjetaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_TarjetaID",
                table: "Compras",
                column: "TarjetaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_TarjetaID",
                table: "Pagos",
                column: "TarjetaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Configuraciones");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "TarjetasDeCredito");
        }
    }
}
