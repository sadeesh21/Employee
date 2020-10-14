using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeApp.Migrations
{
    public partial class Managerintegrity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employee_ManagerID",
                table: "Employee",
                column: "ManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_ManagerID",
                table: "Employee",
                column: "ManagerID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_ManagerID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ManagerID",
                table: "Employee");
        }
    }
}
