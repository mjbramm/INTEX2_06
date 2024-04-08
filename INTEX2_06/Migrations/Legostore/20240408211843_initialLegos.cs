using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INTEX2_06.Migrations.Legostore
{
    /// <inheritdoc />
    public partial class initialLegos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Legos",
                columns: table => new
                {
                    product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    num_parts = table.Column<int>(type: "int", nullable: false),
                    img_link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primary_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondary_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_ordered = table.Column<int>(type: "int", nullable: false),
                    avg_rating = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legos", x => x.product_ID);
                });

            migrationBuilder.CreateIndex(
                name: "INTEX2_06_Legos_product_ID",
                table: "Legos",
                column: "product_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Legos");
        }
    }
}
