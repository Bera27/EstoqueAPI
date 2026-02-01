using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueAPI.Migrations
{
    /// <inheritdoc />
    public partial class MaxLengthTelefone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Funcionarios",
                type: "NVARCHAR(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Funcionarios",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(13)",
                oldMaxLength: 13);
        }
    }
}
