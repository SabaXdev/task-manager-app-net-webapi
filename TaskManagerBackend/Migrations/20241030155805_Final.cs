using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MyTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "MyTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
