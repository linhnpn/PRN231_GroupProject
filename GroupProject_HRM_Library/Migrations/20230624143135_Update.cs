using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupProject_HRM_Library.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Incomes_IncomeID",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveLogs_Incomes_IncomeID",
                table: "LeaveLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeLogs_Incomes_IncomeID",
                table: "OvertimeLogs");

            migrationBuilder.RenameColumn(
                name: "IncomeID",
                table: "OvertimeLogs",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_OvertimeLogs_IncomeID",
                table: "OvertimeLogs",
                newName: "IX_OvertimeLogs_EmployeeID");

            migrationBuilder.RenameColumn(
                name: "IncomeID",
                table: "LeaveLogs",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveLogs_IncomeID",
                table: "LeaveLogs",
                newName: "IX_LeaveLogs_EmployeeID");

            migrationBuilder.RenameColumn(
                name: "IncomeID",
                table: "Bonuses",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Bonuses_IncomeID",
                table: "Bonuses",
                newName: "IX_Bonuses_EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Employees_EmployeeID",
                table: "Bonuses",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveLogs_Employees_EmployeeID",
                table: "LeaveLogs",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeLogs_Employees_EmployeeID",
                table: "OvertimeLogs",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_Employees_EmployeeID",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveLogs_Employees_EmployeeID",
                table: "LeaveLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeLogs_Employees_EmployeeID",
                table: "OvertimeLogs");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "OvertimeLogs",
                newName: "IncomeID");

            migrationBuilder.RenameIndex(
                name: "IX_OvertimeLogs_EmployeeID",
                table: "OvertimeLogs",
                newName: "IX_OvertimeLogs_IncomeID");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "LeaveLogs",
                newName: "IncomeID");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveLogs_EmployeeID",
                table: "LeaveLogs",
                newName: "IX_LeaveLogs_IncomeID");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Bonuses",
                newName: "IncomeID");

            migrationBuilder.RenameIndex(
                name: "IX_Bonuses_EmployeeID",
                table: "Bonuses",
                newName: "IX_Bonuses_IncomeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_Incomes_IncomeID",
                table: "Bonuses",
                column: "IncomeID",
                principalTable: "Incomes",
                principalColumn: "IncomeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveLogs_Incomes_IncomeID",
                table: "LeaveLogs",
                column: "IncomeID",
                principalTable: "Incomes",
                principalColumn: "IncomeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeLogs_Incomes_IncomeID",
                table: "OvertimeLogs",
                column: "IncomeID",
                principalTable: "Incomes",
                principalColumn: "IncomeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
