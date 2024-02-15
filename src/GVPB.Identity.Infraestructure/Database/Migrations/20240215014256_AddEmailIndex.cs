using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GVPB.Identity.Infraestructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "UserManagement",
                table: "Users",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "UserManagement",
                table: "Users");
        }
    }
}
