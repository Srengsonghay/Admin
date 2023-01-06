using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class AddUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetNavigationMenu_AspNetNavigationMenu_ParentMenuId",
                table: "AspNetNavigationMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleMenuPermission_AspNetNavigationMenu_NavigationMenuId",
                table: "AspNetRoleMenuPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Category_Customers_category_id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_jobApplications_Careers_career_id",
                table: "jobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Category_partners_category_id",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_SolutionDetails_Solutions_solution_id",
                table: "SolutionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_SolutionsType_solution_type_id",
                table: "Solutions");

            migrationBuilder.DeleteData(
                table: "AspNetNavigationMenu",
                keyColumn: "Id",
                keyValue: new Guid("3c1702c5-c34f-4468-b807-3a1d5545f734"));

            migrationBuilder.DeleteData(
                table: "AspNetNavigationMenu",
                keyColumn: "Id",
                keyValue: new Guid("913bf559-db46-4072-bd01-f73f3c92e5d5"));

            migrationBuilder.DeleteData(
                table: "AspNetNavigationMenu",
                keyColumn: "Id",
                keyValue: new Guid("94c22f11-6dd2-4b9c-95f7-9dd4ea1002e6"));

            migrationBuilder.DeleteData(
                table: "AspNetNavigationMenu",
                keyColumn: "Id",
                keyValue: new Guid("f704bdfd-d3ea-4a6f-9463-da47ed3657ab"));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetNavigationMenu_AspNetNavigationMenu_ParentMenuId",
                table: "AspNetNavigationMenu",
                column: "ParentMenuId",
                principalTable: "AspNetNavigationMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleMenuPermission_AspNetNavigationMenu_NavigationMenuId",
                table: "AspNetRoleMenuPermission",
                column: "NavigationMenuId",
                principalTable: "AspNetNavigationMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Category_Customers_category_id",
                table: "Customers",
                column: "category_id",
                principalTable: "Category_Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_jobApplications_Careers_career_id",
                table: "jobApplications",
                column: "career_id",
                principalTable: "Careers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Category_partners_category_id",
                table: "Partners",
                column: "category_id",
                principalTable: "Category_partners",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolutionDetails_Solutions_solution_id",
                table: "SolutionDetails",
                column: "solution_id",
                principalTable: "Solutions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_SolutionsType_solution_type_id",
                table: "Solutions",
                column: "solution_type_id",
                principalTable: "SolutionsType",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetNavigationMenu_AspNetNavigationMenu_ParentMenuId",
                table: "AspNetNavigationMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleMenuPermission_AspNetNavigationMenu_NavigationMenuId",
                table: "AspNetRoleMenuPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Category_Customers_category_id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_jobApplications_Careers_career_id",
                table: "jobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Category_partners_category_id",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_SolutionDetails_Solutions_solution_id",
                table: "SolutionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_SolutionsType_solution_type_id",
                table: "Solutions");

            migrationBuilder.InsertData(
                table: "AspNetNavigationMenu",
                columns: new[] { "Id", "ActionName", "Area", "ControllerName", "DisplayOrder", "ExternalUrl", "IsExternal", "Name", "ParentMenuId", "Visible" },
                values: new object[,]
                {
                    { new Guid("3c1702c5-c34f-4468-b807-3a1d5545f734"), "EditUser", null, "Admin", 3, null, false, "Edit User", new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"), false },
                    { new Guid("913bf559-db46-4072-bd01-f73f3c92e5d5"), "CreateRole", null, "Admin", 3, null, false, "Create Role", new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"), true },
                    { new Guid("94c22f11-6dd2-4b9c-95f7-9dd4ea1002e6"), "EditRolePermission", null, "Admin", 3, null, false, "Edit Role Permission", new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"), false },
                    { new Guid("f704bdfd-d3ea-4a6f-9463-da47ed3657ab"), "", null, "", 2, "https://www.google.com/", true, "External Google Link", new Guid("13e2f21a-4283-4ff8-bb7a-096e7b89e0f0"), true }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetNavigationMenu_AspNetNavigationMenu_ParentMenuId",
                table: "AspNetNavigationMenu",
                column: "ParentMenuId",
                principalTable: "AspNetNavigationMenu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleMenuPermission_AspNetNavigationMenu_NavigationMenuId",
                table: "AspNetRoleMenuPermission",
                column: "NavigationMenuId",
                principalTable: "AspNetNavigationMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Category_Customers_category_id",
                table: "Customers",
                column: "category_id",
                principalTable: "Category_Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jobApplications_Careers_career_id",
                table: "jobApplications",
                column: "career_id",
                principalTable: "Careers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Category_partners_category_id",
                table: "Partners",
                column: "category_id",
                principalTable: "Category_partners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolutionDetails_Solutions_solution_id",
                table: "SolutionDetails",
                column: "solution_id",
                principalTable: "Solutions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_SolutionsType_solution_type_id",
                table: "Solutions",
                column: "solution_type_id",
                principalTable: "SolutionsType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
