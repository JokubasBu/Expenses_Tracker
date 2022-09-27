using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Server.Migrations
{
    public partial class firstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyExps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Money = table.Column<double>(type: "REAL", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyExps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyExps_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Groceries" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "Transportation" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Health" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 4, "Entertainment" });

            migrationBuilder.InsertData(
                table: "MonthlyExps",
                columns: new[] { "Id", "CategoryId", "Comment", "Money" },
                values: new object[] { 1, 1, "Chicken has become quite expensive", 435.0 });

            migrationBuilder.InsertData(
                table: "MonthlyExps",
                columns: new[] { "Id", "CategoryId", "Comment", "Money" },
                values: new object[] { 2, 2, "Buses are slow", 25.0 });

            migrationBuilder.InsertData(
                table: "MonthlyExps",
                columns: new[] { "Id", "CategoryId", "Comment", "Money" },
                values: new object[] { 3, 3, "partyyy", 1000.0 });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyExps_CategoryId",
                table: "MonthlyExps",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyExps");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
