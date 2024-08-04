using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudWithImageUploading.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Std_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Std_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Std_Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Std_Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Std_Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsInfo",
                columns: table => new
                {
                    Std_Info_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Std_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsInfo", x => x.Std_Info_Id);
                    table.ForeignKey(
                        name: "FK_StudentsInfo_Students_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "Students",
                        principalColumn: "Std_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInfo_Student_Id",
                table: "StudentsInfo",
                column: "Student_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsInfo");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
