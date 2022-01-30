using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Models
{
  public class BookGenreViewModel
  {
    public List<Book>? Books { get; set; }
    public SelectList? Genres { get; set; }
    public SelectList? IsBusy { get; set; }
    public SelectList? Authors { get; set; }
    public SelectList? Years { get; set; }
    public bool? BookIsBusy { get; set; }
    public int? BookYear { get; set; }
    public string? BookAuthor { get; set; }
    public string? BookGenre { get; set; }
    public string? SearchString { get; set; }
  }
}