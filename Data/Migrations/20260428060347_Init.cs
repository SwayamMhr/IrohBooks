using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IrohBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductGenres",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGenres", x => new { x.ProductId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ProductGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductGenres_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Non-Fiction" },
                    { 3, "Technology" },
                    { 4, "Education" },
                    { 5, "Biography" },
                    { 6, "Science" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Romance" },
                    { 3, "Mystery" },
                    { 4, "Self-Help" },
                    { 5, "Motivation" },
                    { 6, "Programming" },
                    { 7, "Web Development" },
                    { 8, "Data Science" },
                    { 9, "Textbook" },
                    { 10, "Exam Preparation" },
                    { 11, "Autobiography" },
                    { 12, "Memoir" },
                    { 13, "Physics" },
                    { 14, "Biology" },
                    { 15, "Epic Fantasy" },
                    { 16, "Urban Fantasy" },
                    { 17, "Detective" },
                    { 18, "Thriller" },
                    { 19, "Habit Building" },
                    { 20, "Productivity" },
                    { 21, "Coding Practices" },
                    { 22, "System Design" },
                    { 23, "Machine Learning" },
                    { 24, "Algorithm Design" },
                    { 25, "Life Story" },
                    { 26, "Entrepreneur Journey" },
                    { 27, "Cosmology" },
                    { 28, "Astrophysics" },
                    { 29, "Adventure" },
                    { 30, "Philosophical" },
                    { 31, "Career Growth" },
                    { 32, "Best Practices" },
                    { 33, "Problem Solving" },
                    { 34, "Space Science" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "A philosophical novel about following your dreams by Paulo Coelho", "The Alchemist", 500m, 50 },
                    { 2, 2, "A self-improvement book about building good habits by James Clear", "Atomic Habits", 650m, 40 },
                    { 3, 3, "A guide to writing clean and maintainable code by Robert C. Martin", "Clean Code", 800m, 30 },
                    { 4, 4, "A comprehensive textbook on algorithms by Thomas H. Cormen", "Introduction to Algorithms", 1200m, 20 },
                    { 5, 5, "A biography of Steve Jobs by Walter Isaacson", "Steve Jobs", 700m, 25 },
                    { 6, 6, "A popular science book on cosmology by Stephen Hawking", "A Brief History of Time", 900m, 15 },
                    { 7, 1, "A fantasy adventure novel by J.R.R. Tolkien", "The Hobbit", 550m, 35 }
                });

            migrationBuilder.InsertData(
                table: "ProductGenres",
                columns: new[] { "GenreId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 16, 1 },
                    { 29, 1 },
                    { 30, 1 },
                    { 4, 2 },
                    { 5, 2 },
                    { 19, 2 },
                    { 20, 2 },
                    { 31, 2 },
                    { 6, 3 },
                    { 7, 3 },
                    { 18, 3 },
                    { 21, 3 },
                    { 33, 3 },
                    { 9, 4 },
                    { 10, 4 },
                    { 22, 4 },
                    { 24, 4 },
                    { 33, 4 },
                    { 11, 5 },
                    { 12, 5 },
                    { 25, 5 },
                    { 26, 5 },
                    { 31, 5 },
                    { 13, 6 },
                    { 27, 6 },
                    { 28, 6 },
                    { 30, 6 },
                    { 34, 6 },
                    { 1, 7 },
                    { 3, 7 },
                    { 15, 7 },
                    { 17, 7 },
                    { 29, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGenres_GenreId",
                table: "ProductGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductGenres");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
