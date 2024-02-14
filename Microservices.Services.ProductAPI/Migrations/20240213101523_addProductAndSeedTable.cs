using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Microservices.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductAndSeedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Computer", "Keyboard description", "https://m.media-amazon.com/images/I/61jPovnNr-L._AC_SL1200_.jpg", "Keyboard", 10.0 },
                    { 2, "Computer", "Mouse description", "https://productimages.hepsiburada.net/s/468/550/110000506050793.jpg/format:webp", "Mouse", 10.0 },
                    { 3, "Computer", "Motherboard description", "https://dlcdnwebimgs.asus.com/gain/fdae2676-06f0-41e4-a184-8547aedb3b9c/w692", "Motherboard", 10.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
