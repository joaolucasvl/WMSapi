using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarStatusTransportadora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Carregamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModeloCaminhao",
                table: "Carregamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PesoTotal",
                table: "Carregamentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Carregamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Transportadora",
                table: "Carregamentos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Carregamentos");

            migrationBuilder.DropColumn(
                name: "ModeloCaminhao",
                table: "Carregamentos");

            migrationBuilder.DropColumn(
                name: "PesoTotal",
                table: "Carregamentos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Carregamentos");

            migrationBuilder.DropColumn(
                name: "Transportadora",
                table: "Carregamentos");
        }
    }
}
