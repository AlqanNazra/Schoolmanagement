using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    id_student = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_enrollment = table.Column<int>(type: "integer", nullable: false),
                    nama = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    waktu_registrasi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.id_student);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    id_kelas = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nama_kelas = table.Column<string>(type: "text", nullable: false),
                    deskripsi_kelas = table.Column<string>(type: "text", nullable: false),
                    Pendaftaranid_enrollment = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.id_kelas);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    id_enrollment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    daftar_kelas = table.Column<int>(type: "integer", nullable: false),
                    id_kelas = table.Column<int>(type: "integer", nullable: false),
                    id_student = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.id_enrollment);
                    table.ForeignKey(
                        name: "FK_Enrollments_Classes_id_kelas",
                        column: x => x.id_kelas,
                        principalTable: "Classes",
                        principalColumn: "id_kelas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_id_student",
                        column: x => x.id_student,
                        principalTable: "Students",
                        principalColumn: "id_student",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    id_teacher = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_kelas = table.Column<int>(type: "integer", nullable: false),
                    nama_teacher = table.Column<string>(type: "text", nullable: false),
                    email_teacher = table.Column<string>(type: "text", nullable: false),
                    bidang = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.id_teacher);
                    table.ForeignKey(
                        name: "FK_Teachers_Classes_id_kelas",
                        column: x => x.id_kelas,
                        principalTable: "Classes",
                        principalColumn: "id_kelas",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Pendaftaranid_enrollment",
                table: "Classes",
                column: "Pendaftaranid_enrollment");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_id_kelas",
                table: "Enrollments",
                column: "id_kelas");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_id_student_id_kelas",
                table: "Enrollments",
                columns: new[] { "id_student", "id_kelas" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_email_teacher",
                table: "Teachers",
                column: "email_teacher",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Enrollments_Pendaftaranid_enrollment",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
