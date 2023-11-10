using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vb_Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaxNumber = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false, defaultValue: "TR000000000000000000000000"),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dealer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InvoiceAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dividend = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    OpenAccountLimit = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false, defaultValue: 0m),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "dealer")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chat_Dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentSuccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompanyApprove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceExist = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Piece = table.Column<int>(type: "int", nullable: false),
                    TotalAmountByProduct = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderReject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderReject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderReject_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
               table: "Company",
               columns: new[] { "Id", "Address", "Email", "InsertDate", "InsertUserId", "Name", "Password", "TaxNumber", "UpdateDate" },
               values: new object[] { 1, "Suite 46", "testCompany@mail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Leda Wernham", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", 817553976, null });
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "Email", "InsertDate", "InsertUserId", "Name", "Password", "TaxNumber", "UpdateDate" },
                values: new object[] { 2, "Room 907", "scampana1@163.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Shannah Campana", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", 984386925, null });
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "Email", "InsertDate", "InsertUserId", "Name", "Password", "TaxNumber", "UpdateDate" },
                values: new object[] { 3, "Apt 1661", "okike2@elpais.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Olia Kike", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", 487790504, null });
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "Email", "InsertDate", "InsertUserId", "Name", "Password", "TaxNumber", "UpdateDate" },
                values: new object[] { 4, "PO Box 77127", "bburstowe3@soundcloud.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Berte Burstowe", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", 532110099, null });
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Address", "Email", "InsertDate", "InsertUserId", "Name", "Password", "TaxNumber", "UpdateDate" },
                values: new object[] { 5, "Room 907", "eabethell4@marketwatch.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Edgardo Abethell", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", 817553976, null });


            migrationBuilder.InsertData(
                table: "Dealer",
                columns: new[] { "Id", "Address", "InvoiceAddress", "Dividend", "OpenAccountLimit", "Email", "InsertDate", "InsertUserId", "Name", "Password", "UpdateDate" },
                values: new object[] { 1, "Apt 1858 Apt 1858 Apt 1858 Apt 1858 Apt 1858", "Apt 1858 Apt 1858 Apt 1858 Apt 1858 Apt 1858", 1.5, 10000, "testdealer@mail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Van der Viel", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", null });
            migrationBuilder.InsertData(
                table: "Dealer",
                columns: new[] { "Id", "Address", "InvoiceAddress", "Dividend", "OpenAccountLimit", "Email", "InsertDate", "InsertUserId", "Name", "Password", "UpdateDate" },
                values: new object[] { 2, "Street 534 Street 534 Street 534 Street 534", "Street 534 Street 534 Street 534 Street 534", 1.8, 45000, "dolfert7@tinypic.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Gwenneth Ayer", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", null });
            migrationBuilder.InsertData(
                table: "Dealer",
                columns: new[] { "Id", "Address", "InvoiceAddress", "Dividend", "OpenAccountLimit", "Email", "InsertDate", "InsertUserId", "Name", "Password", "UpdateDate" },
                values: new object[] { 3, "Istanbul Istanbul Istanbul Istanbul Istanbul", "Istanbul Istanbul Istanbul Istanbul Istanbul", 1.2, 25000, "mlilford@orange.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Henry Drinkwater", "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824", null });


            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 1, "Repair of teeth healthy", "Yiyecek", 150, 0.25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Sakız", 0.18, 2, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 2, "Repair of teeth healthy", "Yiyecek", 150, 1.35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Bisküvi", 0.18, 2, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 3, "Repair of teeth healthy", "İçecek", 65, 2.5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ayran", 0.18, 1, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 4, "Repair of teeth healthy", "İçecek", 95, 25.50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Çay", 0.18, 1, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 5, "Repair of teeth healthy", "İçecek", 48, 6.50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Soda", 0.18, 1, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 6, "Repair of teeth healthy", "Manav", 150, 0.85, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Domates", 0.18, 3, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 7, "Repair of teeth healthy", "Manav", 65, 8.25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Soğan", 0.18, 3, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 8, "Repair of teeth healthy", "Manav", 50, 3.30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Kavun", 0.18, 3, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 9, "Repair of teeth healthy", "Manav", 132, 1.20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Elma", 0.18, 3, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 10, "Repair of teeth healthy", "Balık", 110, 2.45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Portakal", 0.18, 4, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 11, "Repair of teeth healthy", "Balık", 50, 8.65, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Patates", 0.18, 4, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 12, "Repair of teeth healthy", "Temizlik", 90, 20.50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Tuvalet Kağıdı", 0.20, 5, null });
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Type", "StockQuantity", "Price", "InsertDate", "InsertUserId", "Name", "TaxRate", "CompanyId", "UpdateDate" },
                values: new object[] { 13, "Repair of teeth healthy", "Temizlik", 250, 25.00, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Çamaşır Suyu", 0.20, 5, null });

             migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 1, 535353, "Card", "Room 907", 240, true, true, 1, 1, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
	        migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 2, 343434, "OpenAccount", "Room 907", 320, false, false, 1, 5, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 3, 504228, "EFT", "Room 907", 18, false, false, 1, 3, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 4, 301737, "Transfer", "Room 907", 3, false, false, 1, 3, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 5, 854764, "OpenAccount", "Room 907", 2, false, false, 1, 3, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 6, 775386, "Card", "Room 907", 10, false, false, 1, 1, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "OrderNumber", "PaymentMethod", "Address", "Amount", "PaymentSuccess", "CompanyApprove", "DealerId", "CompanyId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 7, 842137, "Card", "Room 907", 75, false, false, 1, 1, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "Id", "Amount", "PaymentMethod", "InvoiceExist", "Address", "OrderId", "PaymentId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 1, 240, "Card", true, "Room 907", 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Id", "PaymentMethod", "Amount", "ReferenceNumber", "InvoiceId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 1, "Card", 240, 530000, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });

            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 1, 5, 160, 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 2, 8, 80, 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
	        migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 3, 2, 240, 13, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 4, 2, 13, 5, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 5, 2, 5, 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 6, 3, 3, 9, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 7, 10, 250, 13, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 8, 2, 2, 6, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 9, 4, 10, 3, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });
            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "Id", "Piece", "TotalAmountByProduct", "ProductId", "OrderId", "InsertDate", "InsertUserId", "UpdateDate" },
                values: new object[] { 10, 3, 75, 4, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null });


            migrationBuilder.CreateIndex(
                name: "IX_Chat_CompanyId",
                table: "Chat",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_DealerId",
                table: "Chat",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Email",
                table: "Company",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dealer_Email",
                table: "Dealer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OrderId",
                table: "Invoice",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CompanyId",
                table: "Order",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DealerId",
                table: "Order",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderNumber",
                table: "Order",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReject_OrderId",
                table: "OrderReject",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_InvoiceId",
                table: "Payment",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompanyId",
                table: "Product",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderReject");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Dealer");
        }
    }
}
