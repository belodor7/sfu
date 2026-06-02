using System.Collections.ObjectModel;

namespace lab_6.Models
{
 public class Director
 {
 public string Name { get; set; } = string.Empty;
 public ObservableCollection<Movie> Movies { get; set; } = new();
 }
}
