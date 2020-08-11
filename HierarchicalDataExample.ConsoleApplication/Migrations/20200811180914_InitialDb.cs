using Microsoft.EntityFrameworkCore.Migrations;

namespace HierarchicalDataExample.ConsoleApplication.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Descriptions = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName" },
                values: new object[] { 1, "Farhad Zamani" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName" },
                values: new object[] { 2, "Himan falahi" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Descriptions", "Title", "UserId" },
                values: new object[] { 1, "HierarchicalData", "Test HierarchicalData", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ParentId", "PostId", "Text", "UserId" },
                values: new object[] { 1, null, 1, "First comments", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ParentId", "PostId", "Text", "UserId" },
                values: new object[] { 3, null, 1, "Second comment", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ParentId", "PostId", "Text", "UserId" },
                values: new object[] { 2, 1, 1, "Reply to first comments", 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ParentId", "PostId", "Text", "UserId" },
                values: new object[] { 5, 3, 1, "Reply to second comment", 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ParentId", "PostId", "Text", "UserId" },
                values: new object[] { 4, 2, 1, "Reply to previous comment", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
