using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpRoleMapModel_Employees_fk_emp_id",
                table: "EmpRoleMapModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpRoleMapModel_Roles_fk_role_id",
                table: "EmpRoleMapModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmpRoleMapModel",
                table: "EmpRoleMapModel");

            migrationBuilder.RenameTable(
                name: "EmpRoleMapModel",
                newName: "RolesMap");

            migrationBuilder.RenameIndex(
                name: "IX_EmpRoleMapModel_fk_role_id",
                table: "RolesMap",
                newName: "IX_RolesMap_fk_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_EmpRoleMapModel_fk_emp_id",
                table: "RolesMap",
                newName: "IX_RolesMap_fk_emp_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesMap",
                table: "RolesMap",
                column: "pk_role_map_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesMap_Employees_fk_emp_id",
                table: "RolesMap",
                column: "fk_emp_id",
                principalTable: "Employees",
                principalColumn: "pk_emp_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesMap_Roles_fk_role_id",
                table: "RolesMap",
                column: "fk_role_id",
                principalTable: "Roles",
                principalColumn: "pk_role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesMap_Employees_fk_emp_id",
                table: "RolesMap");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesMap_Roles_fk_role_id",
                table: "RolesMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesMap",
                table: "RolesMap");

            migrationBuilder.RenameTable(
                name: "RolesMap",
                newName: "EmpRoleMapModel");

            migrationBuilder.RenameIndex(
                name: "IX_RolesMap_fk_role_id",
                table: "EmpRoleMapModel",
                newName: "IX_EmpRoleMapModel_fk_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_RolesMap_fk_emp_id",
                table: "EmpRoleMapModel",
                newName: "IX_EmpRoleMapModel_fk_emp_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmpRoleMapModel",
                table: "EmpRoleMapModel",
                column: "pk_role_map_id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpRoleMapModel_Employees_fk_emp_id",
                table: "EmpRoleMapModel",
                column: "fk_emp_id",
                principalTable: "Employees",
                principalColumn: "pk_emp_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpRoleMapModel_Roles_fk_role_id",
                table: "EmpRoleMapModel",
                column: "fk_role_id",
                principalTable: "Roles",
                principalColumn: "pk_role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
