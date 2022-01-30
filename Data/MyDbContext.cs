using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
  public class MyDbContext : DbContext
  {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Book { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Book>().HasData(
        new Book
        {
          id = 1,
          title = "Гаррі Потер і філософський камінь",
          author = "Джоан Роулінг",
          genre = "Фентезі",
          year = 2000,
          isBusy = true,
        },
        new Book
        {
          id = 2,
          title = "Миллион миров",
          author = "Антон Карелин",
          genre = "Детектив",
          year = 2022,
          isBusy = false,
        },
        new Book
        {
          id = 3,
          title = "Уникум 1: Тайная дверь",
          author = "Дмитрий Билик",
          genre = "Пригоди",
          year = 2010,
          isBusy = true,
        },
        new Book
        {
          id = 4,
          title = "Смерть может танцевать",
          author = "Макс Вальтер",
          genre = "Пригоди",
          year = 2020,
          isBusy = true,
        },
        new Book
        {
          id = 5,
          title = "Важный разговор",
          author = "Касымов Тимур",
          genre = "Поезія",
          year = 2000,
          isBusy = false,
        },
        new Book
        {
          id = 6,
          title = "Свин Бося . Гоню",
          author = "Дмитрий Виз",
          genre = "Поезія",
          year = 2000,
          isBusy = false,
        },
        new Book
        {
          id = 7,
          title = "Викинг",
          author = "Mastadont аkа Aveolin",
          genre = "Фентезі",
          year = 2000,
          isBusy = false,
        },
        new Book
        {
          id = 8,
          title = "Ученик Рун",
          author = "Александр Якубович",
          genre = "Пригоди",
          year = 2001,
          isBusy = true,
        },
        new Book
        {
          id = 9,
          title = "Карусель теней",
          author = "Андрей Васильев",
          genre = "Фентезі",
          year = 2000,
          isBusy = true,
        },
        new Book
        {
          id = 10,
          title = "Гаррі Потер і таємна кімната",
          author = "Джоан Роулінг",
          genre = "Фентезі",
          year = 2000,
          isBusy = true,
        },
        new Book
        {
          id = 11,
          title = "Гаррі Потер і келих вогню",
          author = "Джоан Роулінг",
          genre = "Фентезі",
          year = 2000,
          isBusy = true,
        }
    );
    }
  }
}