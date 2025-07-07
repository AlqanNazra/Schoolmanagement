using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Updatemigrasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Enrollments_Pendaftaranid_enrollment",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Classes_id_kelas",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_id_kelas",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "id_kelas",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "id_enrollment",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "daftar_kelas",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "deskripsi_kelas",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "Pendaftaranid_enrollment",
                table: "Classes",
                newName: "id_guru");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_Pendaftaranid_enrollment",
                table: "Classes",
                newName: "IX_Classes_id_guru");

            migrationBuilder.AddColumn<DateTime>(
                name: "waktu_pendaftaran",
                table: "Enrollments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "KelasGuru",
                columns: table => new
                {
                    KelasDiajarkanid_kelas = table.Column<int>(type: "integer", nullable: false),
                    Pengajarid_teacher = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KelasGuru", x => new { x.KelasDiajarkanid_kelas, x.Pengajarid_teacher });
                    table.ForeignKey(
                        name: "FK_KelasGuru_Classes_KelasDiajarkanid_kelas",
                        column: x => x.KelasDiajarkanid_kelas,
                        principalTable: "Classes",
                        principalColumn: "id_kelas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KelasGuru_Teachers_Pengajarid_teacher",
                        column: x => x.Pengajarid_teacher,
                        principalTable: "Teachers",
                        principalColumn: "id_teacher",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_email",
                table: "Students",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KelasGuru_Pengajarid_teacher",
                table: "KelasGuru",
                column: "Pengajarid_teacher");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_id_guru",
                table: "Classes",
                column: "id_guru",
                principalTable: "Teachers",
                principalColumn: "id_teacher",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_id_guru",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "KelasGuru");

            migrationBuilder.DropIndex(
                name: "IX_Students_email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "waktu_pendaftaran",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "id_guru",
                table: "Classes",
                newName: "Pendaftaranid_enrollment");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_id_guru",
                table: "Classes",
                newName: "IX_Classes_Pendaftaranid_enrollment");

            migrationBuilder.AddColumn<int>(
                name: "id_kelas",
                table: "Teachers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_enrollment",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "daftar_kelas",
                table: "Enrollments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "deskripsi_kelas",
                table: "Classes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_id_kelas",
                table: "Teachers",
                column: "id_kelas");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Enrollments_Pendaftaranid_enrollment",
                table: "Classes",
                column: "Pendaftaranid_enrollment",
                principalTable: "Enrollments",
                principalColumn: "id_enrollment");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Classes_id_kelas",
                table: "Teachers",
                column: "id_kelas",
                principalTable: "Classes",
                principalColumn: "id_kelas",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
