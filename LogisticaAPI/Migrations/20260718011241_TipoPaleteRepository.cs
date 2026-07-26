using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class TipoPaleteRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Palete",
                columns: table => new
                {
                    PaleteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    PesoMaximo = table.Column<double>(type: "REAL", nullable: false),
                    AlturaMaxima = table.Column<double>(type: "REAL", nullable: false),
                    VolumeMaximo = table.Column<double>(type: "REAL", nullable: false),
                    PesoAtual = table.Column<double>(type: "REAL", nullable: false),
                    VolumeAtual = table.Column<double>(type: "REAL", nullable: false),
                    CarregamentoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palete", x => x.PaleteId);
                    table.ForeignKey(
                        name: "FK_Palete_Carregamentos_CarregamentoId",
                        column: x => x.CarregamentoId,
                        principalTable: "Carregamentos",
                        principalColumn: "CarregamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoPaletes",
                columns: table => new
                {
                    TipoPaleteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    PesoMaximo = table.Column<double>(type: "REAL", nullable: false),
                    AlturaMaximo = table.Column<double>(type: "REAL", nullable: false),
                    VolumeMaximo = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPaletes", x => x.TipoPaleteId);
                });

            migrationBuilder.CreateTable(
                name: "ItemPalete",
                columns: table => new
                {
                    ItemPaleteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    VolumeUnitario = table.Column<double>(type: "REAL", nullable: false),
                    PesoUnitario = table.Column<double>(type: "REAL", nullable: false),
                    AlturaUnitario = table.Column<double>(type: "REAL", nullable: false),
                    PaleteId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPalete", x => x.ItemPaleteId);
                    table.ForeignKey(
                        name: "FK_ItemPalete_Palete_PaleteId",
                        column: x => x.PaleteId,
                        principalTable: "Palete",
                        principalColumn: "PaleteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPalete_PaleteId",
                table: "ItemPalete",
                column: "PaleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Palete_CarregamentoId",
                table: "Palete",
                column: "CarregamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPalete");

            migrationBuilder.DropTable(
                name: "TipoPaletes");

            migrationBuilder.DropTable(
                name: "Palete");
        }
    }
}
