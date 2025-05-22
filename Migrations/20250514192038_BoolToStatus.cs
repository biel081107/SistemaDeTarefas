using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2MVC.Migrations
{
    /// <inheritdoc />
    public partial class BoolToStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluida",
                table: "Tarefas");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tarefas");

            migrationBuilder.AddColumn<bool>(
                name: "Concluida",
                table: "Tarefas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
