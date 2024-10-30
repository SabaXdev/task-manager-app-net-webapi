using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerBackend.Migrations
{
    /// <inheritdoc />
    public partial class Final10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyTasks",
                table: "MyTasks");

            migrationBuilder.RenameTable(
                name: "MyTasks",
                newName: "MyTasksNew");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyTasksNew",
                table: "MyTasksNew",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyTasksNew",
                table: "MyTasksNew");

            migrationBuilder.RenameTable(
                name: "MyTasksNew",
                newName: "MyTasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyTasks",
                table: "MyTasks",
                column: "Id");
        }
    }
}
