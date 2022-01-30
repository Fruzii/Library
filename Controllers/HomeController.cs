using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly MyDbContext _context;

  public HomeController(ILogger<HomeController> logger, MyDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  // public IActionResult Index()
  // {
  //   // var books = _context.Book.Where(x => x.isBusy == false).ToList();
  //   var books = _context.Book.ToList();
  //   return View(books);
  // }

  // GET: Movies
  public async Task<IActionResult> Index(
    string bookGenre,
    string searchString,
    bool? bookIsBusy,
    DateTime? bookYear,
    string bookAuthor,
    SortState sortOrder = SortState.TitleAsc
    )
  {
    ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
    ViewData["AuthorSort"] = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
    ViewData["GenreSort"] = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
    ViewData["YearSort"] = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
    ViewData["IsBusySort"] = sortOrder == SortState.IsBusyAsc ? SortState.IsBusyDesc : SortState.IsBusyAsc;

    // Use LINQ to get list of genres.
    IQueryable<string> genreQuery = from b in _context.Book
                                    orderby b.genre
                                    select b.genre;
    var isBusyQuery = from b in _context.Book
                      select b.isBusy;
    var authorQuery = from b in _context.Book
                      select b.author;
    var yearQuery = from b in _context.Book
                    select b.year;
    var books = from b in _context.Book
                select b;

    if (!string.IsNullOrEmpty(searchString))
    {
      books = books.Where(s => s.title!.Contains(searchString));
    }
    if (bookIsBusy != null)
    {
      books = books.Where(s => s.isBusy == bookIsBusy);
    }

    if (!string.IsNullOrEmpty(bookGenre))
    {
      books = books.Where(x => x.genre == bookGenre);
    }

    if (bookGenre != null)
    {
      books = books.Where(x => x.year == bookYear);
    }

    if (!string.IsNullOrEmpty(bookAuthor))
    {
      books = books.Where(x => x.author == bookAuthor);
    }

    books = sortOrder switch
    {
      SortState.TitleDesc => books.OrderByDescending(s => s.title),
      SortState.AuthorAsc => books.OrderBy(s => s.author),
      SortState.AuthorDesc => books.OrderByDescending(s => s.author),
      SortState.GenreAsc => books.OrderBy(s => s.genre),
      SortState.GenreDesc => books.OrderByDescending(s => s.genre),
      SortState.YearAsc => books.OrderBy(s => s.year),
      SortState.YearDesc => books.OrderByDescending(s => s.year),
      _ => books.OrderBy(s => s.title),
    };
    // SortState.IsBusyAsc => books.OrderBy(s => s.isBusy),
    // SortState.IsBusyDesc => books.OrderByDescending(s => s.isBusy),
    var bookGenreVM = new BookGenreViewModel
    {
      Authors = new SelectList(await authorQuery.Distinct().ToListAsync()),
      Years = new SelectList(await yearQuery.Distinct().ToListAsync()),
      Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
      IsBusy = new SelectList(await isBusyQuery.Distinct().ToListAsync()),
      Books = await books.ToListAsync()
    };

    return View(bookGenreVM);
  }

  // public async Task<IActionResult> Index(string searchString)
  // {
  //   var book = from b in _context.Book
  //              select b;

  //   if (!String.IsNullOrEmpty(searchString))
  //   {
  //     book = book.Where(s => s.title!.Contains(searchString));
  //   }

  //   return View(await book.ToListAsync());
  // }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }

  // GET: Book/Edit/5
  public async Task<IActionResult> Details(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var book = await _context.Book.FindAsync(id);
    if (book == null)
    {
      return NotFound();
    }
    return View(book);
  }

  // GET: Book/Edit/5
  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var book = await _context.Book.FindAsync(id);
    if (book == null)
    {
      return NotFound();
    }
    return View(book);
  }

  // POST: Book/Edit/5
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, Book book)
  {
    if (id != book.id)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      try
      {
        _context.Update(book);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        // if (!BookExists(book.id))
        if (book == null)
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return RedirectToAction(nameof(Index));
    }
    return View(book);
  }

  // GET: Books/Create
  public IActionResult Create()
  {
    return View();
  }

  // POST: Books/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(Book book)
  {
    _context.Add(book);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));

    // return View(book);
  }

  // GET: Books/Delete/5
  [ActionName("Delete")]
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var book = await _context.Book
        .FirstOrDefaultAsync(p => p.id == id);
    if (book == null)
    {
      return NotFound();
    }

    return View(book);
  }

  // POST: Movies/Delete/5
  [HttpPost, ActionName("Delete")]
  // [HttpPost]
  // [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    if (id != null)
    {
      var book = await _context.Book.FindAsync(id);
      _context.Book.Remove(book);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return NotFound();
  }
}
