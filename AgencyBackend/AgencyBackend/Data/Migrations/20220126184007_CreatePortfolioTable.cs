using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgencyBackend.Data.Migrations
{
    public partial class CreatePortfolioTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Info = table.Column<string>(maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Portfolios",
                columns: new[] { "Id", "Image", "Info", "Title" },
                values: new object[,]
                {
                    { 1, "watch.jpg", "Lorem Ipsum Dolor Sit Amet", "Lorem Ipsum1" },
                    { 2, "watch.jpg", "Lorem Ipsum Dolor Sit Amet", "Lorem Ipsum2" },
                    { 3, "watch.jpg", "Lorem Ipsum Dolor Sit Amet", "Lorem Ipsum3" },
                    { 4, "watch.jpg", "Lorem Ipsum Dolor Sit Amet", "Lorem Ipsum4" },
                    { 5, "watch.jpg", "Lorem Ipsum Dolor Sit Amet", "Lorem Ipsum5" },
                    { 6, "watch.jpg", "Lorem Ipsum Dolor Sit Amet", "Lorem Ipsum6" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
