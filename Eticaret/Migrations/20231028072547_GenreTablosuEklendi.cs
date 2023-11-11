using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Migrations
{
    /// <inheritdoc />
    public partial class GenreTablosuEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "ApplyUsers",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "ApplyUsers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "ApplyUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "ApplyUsers",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FotoPath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "ApplyUsers",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "ApplyUsers",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApplyUsers",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ApplyUsers",
                newName: "Aciklama");
        }
    }
}
