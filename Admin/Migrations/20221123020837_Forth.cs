using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    public partial class Forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Careers_career_id",
                table: "JobApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "year",
                table: "JobApplication");

            migrationBuilder.RenameTable(
                name: "JobApplication",
                newName: "jobApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_career_id",
                table: "jobApplications",
                newName: "IX_jobApplications_career_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_jobApplications",
                table: "jobApplications",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_jobApplications_Careers_career_id",
                table: "jobApplications",
                column: "career_id",
                principalTable: "Careers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobApplications_Careers_career_id",
                table: "jobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_jobApplications",
                table: "jobApplications");

            migrationBuilder.RenameTable(
                name: "jobApplications",
                newName: "JobApplication");

            migrationBuilder.RenameIndex(
                name: "IX_jobApplications_career_id",
                table: "JobApplication",
                newName: "IX_JobApplication_career_id");

            migrationBuilder.AddColumn<float>(
                name: "year",
                table: "JobApplication",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Careers_career_id",
                table: "JobApplication",
                column: "career_id",
                principalTable: "Careers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
