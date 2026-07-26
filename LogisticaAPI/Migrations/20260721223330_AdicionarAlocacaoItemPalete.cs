using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarAlocacaoItemPalete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedido_Palete_PaleteId",
                table: "ItensPedido");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedido_PaleteId",
                table: "ItensPedido");

            migrationBuilder.DropColumn(
                name: "PaleteId",
                table: "ItensPedido");

            migrationBuilder.AlterColumn<int>(
                name: "PaleteId",
                table: "Palete",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "ItensPalete",
                columns: table => new
                {
                    ItemPaleteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemPedidoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaleteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPalete", x => x.ItemPaleteId);
                    table.ForeignKey(
                        name: "FK_ItensPalete_ItensPedido_ItemPedidoId",
                        column: x => x.ItemPedidoId,
                        principalTable: "ItensPedido",
                        principalColumn: "ItemPedidoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensPalete_Palete_PaleteId",
                        column: x => x.PaleteId,
                        principalTable: "Palete",
                        principalColumn: "PaleteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPalete_ItemPedidoId",
                table: "ItensPalete",
                column: "ItemPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPalete_PaleteId",
                table: "ItensPalete",
                column: "PaleteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPalete");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaleteId",
                table: "Palete",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaleteId",
                table: "ItensPedido",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PaleteId",
                table: "ItensPedido",
                column: "PaleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPedido_Palete_PaleteId",
                table: "ItensPedido",
                column: "PaleteId",
                principalTable: "Palete",
                principalColumn: "PaleteId");
        }
    }
}
