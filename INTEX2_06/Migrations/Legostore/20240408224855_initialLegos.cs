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
                name: "Customers",
                columns: table => new
                {
                    customer_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Customers", x => x.customer_ID);
                });

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

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_ID = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.transaction_ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    transaction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_ID = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    day_of_week = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entry_mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    type_of_transaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country_of_transaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shipping_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type_of_card = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fraud = table.Column<bool>(type: "bit", nullable: false),
                    predict_fraud = table.Column<bool>(type: "bit", nullable: false),
                    complete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.transaction_ID);
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
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Legos");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
