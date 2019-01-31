using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SneakerDrop.Code.SneakerDropMigrations
{
    public partial class first_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.EnsureSchema(
                name: "Store");

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Store",
                columns: table => new
                {
                    ProductInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductTitle = table.Column<string>(maxLength: 50, nullable: false),
                    Brand = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    DisplayPrice = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<string>(maxLength: 50, nullable: false),
                    Color = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductInfoId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(maxLength: 50, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                schema: "Store",
                columns: table => new
                {
                    ListingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<int>(nullable: false),
                    ProductInfo = table.Column<int>(nullable: false),
                    UserSetPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Size = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_Listing_Product_ProductInfo",
                        column: x => x.ProductInfo,
                        principalSchema: "Store",
                        principalTable: "Product",
                        principalColumn: "ProductInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listing_User_User",
                        column: x => x.User,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "User",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<int>(nullable: false),
                    Street = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_User_User",
                        column: x => x.User,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "User",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<int>(nullable: false),
                    CCNumber = table.Column<long>(nullable: false),
                    CCUserName = table.Column<string>(maxLength: 50, nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    CVV = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_User_User",
                        column: x => x.User,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "User",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Listing = table.Column<int>(nullable: false),
                    User = table.Column<int>(nullable: false),
                    Payment = table.Column<int>(nullable: false),
                    OrderGroupNumber = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ShippingStatus = table.Column<string>(maxLength: 50, nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Listing_Listing",
                        column: x => x.Listing,
                        principalSchema: "Store",
                        principalTable: "Listing",
                        principalColumn: "ListingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payment_Payment",
                        column: x => x.Payment,
                        principalSchema: "User",
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_User_User",
                        column: x => x.User,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listing_ProductInfo",
                schema: "Store",
                table: "Listing",
                column: "ProductInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_User",
                schema: "Store",
                table: "Listing",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Address_User",
                schema: "User",
                table: "Address",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Listing",
                schema: "User",
                table: "Orders",
                column: "Listing");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Payment",
                schema: "User",
                table: "Orders",
                column: "Payment");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_User",
                schema: "User",
                table: "Orders",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_User",
                schema: "User",
                table: "Payment",
                column: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Listing",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Store");

            migrationBuilder.DropTable(
                name: "User",
                schema: "User");
        }
    }
}
