using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    about_heading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    about_detail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Careers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    career_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    career_detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    career_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hiring = table.Column<int>(type: "int", nullable: true),
                    closing_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Careers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category_Customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Category_partners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_partners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_heading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SolutionsType",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionsType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customers_Category_Customers_category_id",
                        column: x => x.category_id,
                        principalTable: "Category_Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    partner_logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    partner_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    partner_detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.id);
                    table.ForeignKey(
                        name: "FK_Partners_Category_partners_category_id",
                        column: x => x.category_id,
                        principalTable: "Category_partners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    solution_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    solution_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Solutions_SolutionsType_solution_type_id",
                        column: x => x.solution_type_id,
                        principalTable: "SolutionsType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolutionDetails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    solution_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_SolutionDetails_Solutions_solution_id",
                        column: x => x.solution_id,
                        principalTable: "Solutions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_category_id",
                table: "Customers",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_category_id",
                table: "Partners",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_SolutionDetails_solution_id",
                table: "SolutionDetails",
                column: "solution_id");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_solution_type_id",
                table: "Solutions",
                column: "solution_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "Careers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "SolutionDetails");

            migrationBuilder.DropTable(
                name: "Category_Customers");

            migrationBuilder.DropTable(
                name: "Category_partners");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "SolutionsType");
        }
    }
}
