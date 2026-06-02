namespace lab_6.Models
{
 public class Movie
 {
 public string Title { get; set; } = string.Empty;
 public string Genre { get; set; } = string.Empty;
 public Director? Director { get; set; }
 public int Year { get; set; }
 public double Rating { get; set; }
 }
}
