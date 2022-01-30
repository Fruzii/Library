using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    author = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    genre = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    year = table.Column<int>(type: "int", nullable: false),
                    isBusy = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "id", "author", "genre", "isBusy", "title", "year" },
                values: new object[,]
                {
                    { 1, "Джоан Роулінг", "Фентезі", true, "Гаррі Потер і філософський камінь", 2000 },
                    { 2, "Антон Карелин", "Детектив", false, "Миллион миров", 2022 },
                    { 3, "Дмитрий Билик", "Пригоди", true, "Уникум 1: Тайная дверь", 2010 },
                    { 4, "Макс Вальтер", "Пригоди", true, "Смерть может танцевать", 2020 },
                    { 5, "Касымов Тимур", "Поезія", false, "Важный разговор", 2000 },
                    { 6, "Дмитрий Виз", "Поезія", false, "Свин Бося . Гоню", 2000 },
                    { 7, "Mastadont аkа Aveolin", "Фентезі", false, "Викинг", 2000 },
                    { 8, "Александр Якубович", "Пригоди", true, "Ученик Рун", 2001 },
                    { 9, "Андрей Васильев", "Фентезі", true, "Карусель теней", 2000 },
                    { 10, "Джоан Роулінг", "Фентезі", true, "Гаррі Потер і таємна кімната", 2000 },
                    { 11, "Джоан Роулінг", "Фентезі", true, "Гаррі Потер і келих вогню", 2000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
