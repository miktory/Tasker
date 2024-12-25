using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkerNameToParametrizedTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "ParametrizedTasks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "ParametrizedTasks");
        }
    }
}
