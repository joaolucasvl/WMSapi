using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarNovasControllers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPalete");

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Fornecedor = table.Column<string>(type: "TEXT", nullable: false),
                    Perecivel = table.Column<bool>(type: "INTEGER", nullable: false),
                    VolumeUnitario = table.Column<double>(type: "REAL", nullable: false),
                    PesoUnitario = table.Column<double>(type: "REAL", nullable: false),
                    AlturaUnitario = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoPedido = table.Column<int>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cliente = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    ItemPedidoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    VolumeUnitario = table.Column<double>(type: "REAL", nullable: false),
                    PesoUnitario = table.Column<double>(type: "REAL", nullable: false),
                    AlturaUnitario = table.Column<double>(type: "REAL", nullable: false),
                    PaleteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => x.ItemPedidoId);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Palete_PaleteId",
                        column: x => x.PaleteId,
                        principalTable: "Palete",
                        principalColumn: "PaleteId");
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_ItemId",
                table: "ItensPedido",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PaleteId",
                table: "ItensPedido",
                column: "PaleteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.CreateTable(
                name: "ItemPalete",
                columns: table => new
                {
                    ItemPaleteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaleteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlturaUnitario = table.Column<double>(type: "REAL", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    PesoUnitario = table.Column<double>(type: "REAL", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    VolumeUnitario = table.Column<double>(type: "REAL", nullable: false)
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
        }
    }
}
