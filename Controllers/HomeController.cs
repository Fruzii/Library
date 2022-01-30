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
    string searchString,
    string sortOrder,
    string currentFilter,
    string bookAuthor,
    string currAuthor,
    string bookGenre,
    string currGenre,
    bool? bookIsBusy,
    bool? currIsBusy,
    int? bookYear,
    int? currYear,
    int? pageNumber
    )
  {
    ViewData["CurrentSort"] = sortOrder;
    // ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "TitleDesc" : "TitleAsc";
    ViewData["TitleSort"] = sortOrder == "TitleAsc" ? "TitleDesc" : "TitleAsc";
    ViewData["AuthorSort"] = sortOrder == "AuthorAsc" ? "AuthorDesc" : "AuthorAsc";
    ViewData["GenreSort"] = sortOrder == "GenreAsc" ? "GenreDesc" : "GenreAsc";
    ViewData["YearSort"] = sortOrder == "YearAsc" ? "YearDesc" : "YearAsc";
    ViewData["IsBusySort"] = sortOrder == "IsBusyAsc" ? "IsBusyDesc" : "IsBusyAsc";

    if (searchString == null)
    {
      searchString = currentFilter;
    }
    if (bookIsBusy == null)
    {
      bookIsBusy = currIsBusy;
    }
    if (bookAuthor == null)
    {
      bookAuthor = currAuthor;
    }
    if (bookGenre == null)
    {
      bookGenre = currGenre;
    }
    if (bookYear == null)
    {
      bookYear = currYear;
    }
  
    ViewData["CurrentFilter"] = searchString;
    ViewData["CurrYear"] = currYear;
    ViewData["CurrAuthor"] = currAuthor;
    ViewData["CurrGenre"] = currGenre;
    ViewData["CurrIsBusy"] = bookIsBusy;
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

    if (bookYear != null)
    {
      books = books.Where(x => x.year == bookYear);
    }

    if (!string.IsNullOrEmpty(bookAuthor))
    {
      books = books.Where(x => x.author == bookAuthor);
    }

    switch (sortOrder)
    {
      case "TitleDesc":
        books = books.OrderByDescending(s => s.title);
        break;
      case "AuthorAsc":
        books = books.OrderBy(s => s.author);
        break;
      case "AuthorDesc":
        books = books.OrderByDescending(s => s.author);
        break;
      case "GenreAsc":
        books = books.OrderBy(s => s.genre);
        break;
      case "GenreDesc":
        books = books.OrderByDescending(s => s.genre);
        break;
      case "YearAsc":
        books = books.OrderBy(s => s.year);
        break;
      case "YearDesc":
        books = books.OrderByDescending(s => s.year);
        break;
      default:
        books = books.OrderBy(s => s.title);
        break;
    };
    // .IsBusyAsc => books.OrderBy(s => s.isBusy),
    // .IsBusyDesc => books.OrderByDescending(s => s.isBusy),

    var AuthorsList = new List<SelectListItem>();
    foreach (string item in authorQuery)
    {
      AuthorsList.Add(new SelectListItem { Value = item, Text = item, Selected = item == currAuthor});
    }

    var YearsList = new List<SelectListItem>();
    foreach (int item in yearQuery)
    {
      YearsList.Add(new SelectListItem { Value = item.ToString(), Text = item.ToString(), Selected = item == currYear});
    }

    var GenresList = new List<SelectListItem>();
    foreach (string item in genreQuery)
    {
      GenresList.Add(new SelectListItem { Value = item, Text = item, Selected = item == currGenre});
    }

    var IsBusyList = new List<SelectListItem>();
    foreach (bool item in isBusyQuery)
    {
      IsBusyList.Add(new SelectListItem { Value = item.ToString(), Text = item ? "Зайняті" : "Вільні", Selected = item == currIsBusy});
    }

    var bookGenreVM = new BookGenreViewModel
    {
      Authors = new SelectList(AuthorsList.DistinctBy(c => c.Text).ToList(), "Value", "Text"),
      Years = new SelectList(YearsList.DistinctBy(c => c.Text).ToList(), "Value", "Text"),
      Genres = new SelectList(GenresList.DistinctBy(c => c.Text).ToList(), "Value", "Text"),
      IsBusy = new SelectList(IsBusyList.DistinctBy(c => c.Text).ToList(), "Value", "Text"),
      // IsBusy = new SelectList(await isBusyQuery.Distinct().ToListAsync()),
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
