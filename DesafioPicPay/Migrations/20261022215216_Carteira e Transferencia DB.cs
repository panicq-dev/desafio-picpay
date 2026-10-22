using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioPicPay.Migrations
{
    /// <inheritdoc />
    public partial class CarteiraeTransferenciaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carteiras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCompleto = table.Column<string>(type: "TEXT", nullable: false),
                    CPFCNPJ = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    SaldoConta = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TipoEntidade = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteiras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    IdTransferencia = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdRemetente = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDestino = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.IdTransferencia);
                    table.ForeignKey(
                        name: "FK_Transferencias_CarteiraDestinatario",
                        column: x => x.IdDestino,
                        principalTable: "Carteiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferencias_CarteiraRemetente",
                        column: x => x.IdRemetente,
                        principalTable: "Carteiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_CPFCNPJ",
                table: "Carteiras",
                column: "CPFCNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_Email",
                table: "Carteiras",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_IdDestino",
                table: "Transferencias",
                column: "IdDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_IdRemetente",
                table: "Transferencias",
                column: "IdRemetente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropTable(
                name: "Carteiras");
        }
    }
}
