using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GVPB.Identity.Infraestructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ManagementServices");

            migrationBuilder.EnsureSchema(
                name: "UserManagement");

            migrationBuilder.CreateTable(
                name: "EnvVariable",
                schema: "ManagementServices",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvVariable", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    LogDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestUsers",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "UserManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordLength = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Rule = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvVariable",
                schema: "ManagementServices");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "RequestUsers",
                schema: "UserManagement");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "UserManagement");
        }
    }
}
