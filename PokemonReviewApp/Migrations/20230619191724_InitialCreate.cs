using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountrySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountrySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewerSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerSet_CountrySet_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountrySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonCategorySet",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PokemonCategoryCategoryId = table.Column<int>(type: "int", nullable: true),
                    PokemonCategoryPokemonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonCategorySet", x => new { x.PokemonId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PokemonCategorySet_CategorySet_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategorySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonCategorySet_PokemonCategorySet_PokemonCategoryPokemonId_PokemonCategoryCategoryId",
                        columns: x => new { x.PokemonCategoryPokemonId, x.PokemonCategoryCategoryId },
                        principalTable: "PokemonCategorySet",
                        principalColumns: new[] { "PokemonId", "CategoryId" });
                    table.ForeignKey(
                        name: "FK_PokemonCategorySet_PokemonSet_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "PokemonSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewSet_PokemonSet_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "PokemonSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewSet_ReviewerSet_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "ReviewerSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonOwnerSet",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonOwnerSet", x => new { x.PokemonId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_PokemonOwnerSet_OwnerSet_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "OwnerSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonOwnerSet_PokemonSet_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "PokemonSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerSet_CountryId",
                table: "OwnerSet",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCategorySet_CategoryId",
                table: "PokemonCategorySet",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCategorySet_PokemonCategoryPokemonId_PokemonCategoryCategoryId",
                table: "PokemonCategorySet",
                columns: new[] { "PokemonCategoryPokemonId", "PokemonCategoryCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonOwnerSet_OwnerId",
                table: "PokemonOwnerSet",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_PokemonId",
                table: "ReviewSet",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSet_ReviewerId",
                table: "ReviewSet",
                column: "ReviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonCategorySet");

            migrationBuilder.DropTable(
                name: "PokemonOwnerSet");

            migrationBuilder.DropTable(
                name: "ReviewSet");

            migrationBuilder.DropTable(
                name: "CategorySet");

            migrationBuilder.DropTable(
                name: "OwnerSet");

            migrationBuilder.DropTable(
                name: "PokemonSet");

            migrationBuilder.DropTable(
                name: "ReviewerSet");

            migrationBuilder.DropTable(
                name: "CountrySet");
        }
    }
}
