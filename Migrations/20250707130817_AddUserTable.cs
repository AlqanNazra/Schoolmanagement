using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Teachers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_userId",
                table: "Teachers",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_userId",
                table: "Students",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_userId",
                table: "Students",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_userId",
                table: "Teachers",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_userId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_userId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_userId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_userId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Students");
        }
    }
}
