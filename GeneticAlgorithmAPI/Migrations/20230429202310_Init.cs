using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneticAlgorithmAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneticAlgorithmData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    minSizeBeforeStartStrategy = table.Column<int>(type: "int", nullable: false),
                    maxSizeBeforeStartStrategy = table.Column<int>(type: "int", nullable: false),
                    minSizeAfterStartStrategy = table.Column<int>(type: "int", nullable: false),
                    maxSizeAfterStartStrategy = table.Column<int>(type: "int", nullable: false),
                    totalNumbersOfMachines = table.Column<int>(type: "int", nullable: false),
                    totalNumbersOfJobs = table.Column<int>(type: "int", nullable: false),
                    strategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    percentageDifferenceBetweenMin = table.Column<int>(type: "int", nullable: false),
                    percentageDifferenceBetweenMax = table.Column<int>(type: "int", nullable: false),
                    numberOfIteration = table.Column<int>(type: "int", nullable: false),
                    minTimeOfExecutionOfJob = table.Column<int>(type: "int", nullable: false),
                    maxTimeOfExecutionOfJob = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticAlgorithmData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneticAlgorithmData");
        }
    }
}
