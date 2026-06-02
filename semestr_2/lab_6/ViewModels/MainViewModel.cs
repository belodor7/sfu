using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using lab_6.Models;

namespace lab_6.ViewModels
{
 public class MainViewModel : INotifyPropertyChanged
 {
 // master collection - keep all movies here
 private ObservableCollection<Movie> AllMovies { get; } = new ObservableCollection<Movie>();

 // collection bound to the UI
 public ObservableCollection<Movie> Movies { get; } = new ObservableCollection<Movie>();
 public ObservableCollection<DirectorView> Directors { get; } = new ObservableCollection<DirectorView>();

 private Movie? _selectedMovie;
 public Movie? SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }

 // New movie fields
 public string NewTitle { get; set; } = string.Empty;
 public string NewGenre { get; set; } = string.Empty;
 public string NewDirectorName { get; set; } = string.Empty;
 public int NewYear { get; set; }
 public double NewRating { get; set; }

 // Filtering
 public ObservableCollection<string> Genres { get; } = new ObservableCollection<string>();
 public string SelectedGenre { get; set; } = string.Empty;
 public int? YearFrom { get; set; }
 public int? YearTo { get; set; }

 public ICommand AddMovieCommand { get; }
 public ICommand RemoveMovieCommand { get; }
 public ICommand SearchCommand { get; }
 public ICommand ResetFilterCommand { get; }
 public ICommand SortAscCommand { get; }
 public ICommand SortDescCommand { get; }
 public ICommand BuildDirectorsListCommand { get; }

 public MainViewModel()
 {
 // seed data into master collection
 AllMovies.Add(new Movie { Title = "Очень странные дела", Genre = "Фантастика", Director = new Director { Name = "Директор А" }, Year =2022, Rating =8.1 });
 AllMovies.Add(new Movie { Title = "Любовь и дружба", Genre = "Драма", Director = new Director { Name = "Директор Б" }, Year =2019, Rating =7.2 });
 AllMovies.Add(new Movie { Title = "Комедия", Genre = "Комедия", Director = new Director { Name = "Директор А" }, Year =2020, Rating =6.5 });

 // initialize Genres and Movies from master
 foreach (var g in AllMovies.Select(m => m.Genre).Distinct()) Genres.Add(g);
 ResetToAllMovies();

 AddMovieCommand = new RelayCommand(_ => AddMovie());
 RemoveMovieCommand = new RelayCommand(p => RemoveMovie(p as Movie));
 SearchCommand = new RelayCommand(_ => ApplyFilter());
 ResetFilterCommand = new RelayCommand(_ => ResetFilter());
 SortAscCommand = new RelayCommand(_ => SortByRating(true));
 SortDescCommand = new RelayCommand(_ => SortByRating(false));
 BuildDirectorsListCommand = new RelayCommand(_ => BuildDirectors());
 }

 private void AddMovie()
 {
 var director = AllMovies.Select(m => m.Director).FirstOrDefault(d => d?.Name == NewDirectorName) ?? new Director { Name = NewDirectorName };
 var movie = new Movie { Title = NewTitle, Genre = NewGenre, Director = director, Year = NewYear, Rating = NewRating };
 // add to master
 AllMovies.Add(movie);
 // update Genres
 if (!Genres.Contains(NewGenre)) Genres.Add(NewGenre);
 // if current filter allows the new movie, add to visible collection
 if (IsMovieMatchFilter(movie))
 Movies.Add(movie);
 OnPropertyChanged(nameof(Movies));
 }

 private void RemoveMovie(Movie? movie)
 {
 if (movie == null) return;
 // remove from master and visible
 if (AllMovies.Contains(movie)) AllMovies.Remove(movie);
 if (Movies.Contains(movie)) Movies.Remove(movie);
 // update Genres: remove genres with zero movies
 var remainingGenres = AllMovies.Select(m => m.Genre).Distinct().ToList();
 for (int i = Genres.Count -1; i >=0; i--)
 {
 if (!remainingGenres.Contains(Genres[i])) Genres.RemoveAt(i);
 }
 OnPropertyChanged(nameof(Movies));
 }

 private bool IsMovieMatchFilter(Movie m)
 {
 if (!string.IsNullOrEmpty(SelectedGenre) && m.Genre != SelectedGenre) return false;
 if (YearFrom.HasValue && m.Year < YearFrom.Value) return false;
 if (YearTo.HasValue && m.Year > YearTo.Value) return false;
 return true;
 }

 private void ApplyFilter()
 {
 var list = AllMovies.Where(m => IsMovieMatchFilter(m)).ToList();
 Movies.Clear();
 foreach (var m in list) Movies.Add(m);
 OnPropertyChanged(nameof(Movies));
 }

 private void ResetFilter()
 {
 SelectedGenre = string.Empty;
 YearFrom = null;
 YearTo = null;
 ResetToAllMovies();
 OnPropertyChanged(nameof(SelectedGenre));
 OnPropertyChanged(nameof(YearFrom));
 OnPropertyChanged(nameof(YearTo));
 }

 private void ResetToAllMovies()
 {
 Movies.Clear();
 foreach (var m in AllMovies) Movies.Add(m);
 }

 private void SortByRating(bool asc)
 {
 var ordered = asc ? Movies.OrderBy(m => m.Rating) : Movies.OrderByDescending(m => m.Rating);
 var list = ordered.ToList();
 Movies.Clear();
 foreach (var m in list) Movies.Add(m);
 }

 private void BuildDirectors()
 {
 Directors.Clear();
 var groups = AllMovies.GroupBy(m => m.Director?.Name ?? "");
 foreach (var g in groups)
 {
 var dv = new DirectorView { Name = g.Key, MoviesCount = g.Count(), AverageRating = g.Average(m => m.Rating), FirstYear = g.Min(m => m.Year) };
 Directors.Add(dv);
 }
 OnPropertyChanged(nameof(Directors));
 }

 public event PropertyChangedEventHandler? PropertyChanged;
 private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
 }

 public class DirectorView
 {
 public string Name { get; set; } = string.Empty;
 public int MoviesCount { get; set; }
 public double AverageRating { get; set; }
 public int FirstYear { get; set; }
 }
}
