using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenomearTabelaPalete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPalete_Palete_PaleteId",
                table: "ItensPalete");

            migrationBuilder.DropForeignKey(
                name: "FK_Palete_Carregamentos_CarregamentoId",
                table: "Palete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Palete",
                table: "Palete");

            migrationBuilder.RenameTable(
                name: "Palete",
                newName: "Paletes");

            migrationBuilder.RenameIndex(
                name: "IX_Palete_CarregamentoId",
                table: "Paletes",
                newName: "IX_Paletes_CarregamentoId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarregamentoId",
                table: "Paletes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paletes",
                table: "Paletes",
                column: "PaleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPalete_Paletes_PaleteId",
                table: "ItensPalete",
                column: "PaleteId",
                principalTable: "Paletes",
                principalColumn: "PaleteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paletes_Carregamentos_CarregamentoId",
                table: "Paletes",
                column: "CarregamentoId",
                principalTable: "Carregamentos",
                principalColumn: "CarregamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPalete_Paletes_PaleteId",
                table: "ItensPalete");

            migrationBuilder.DropForeignKey(
                name: "FK_Paletes_Carregamentos_CarregamentoId",
                table: "Paletes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paletes",
                table: "Paletes");

            migrationBuilder.RenameTable(
                name: "Paletes",
                newName: "Palete");

            migrationBuilder.RenameIndex(
                name: "IX_Paletes_CarregamentoId",
                table: "Palete",
                newName: "IX_Palete_CarregamentoId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarregamentoId",
                table: "Palete",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Palete",
                table: "Palete",
                column: "PaleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensPalete_Palete_PaleteId",
                table: "ItensPalete",
                column: "PaleteId",
                principalTable: "Palete",
                principalColumn: "PaleteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Palete_Carregamentos_CarregamentoId",
                table: "Palete",
                column: "CarregamentoId",
                principalTable: "Carregamentos",
                principalColumn: "CarregamentoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
