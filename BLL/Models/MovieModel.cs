using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class MovieModel
    {
        public Movie Record { get; set; }

        public string Name => Record.Name;

        [DisplayName("Release Date")]
        public string ReleaseDate => !Record.ReleaseDate.HasValue ? string.Empty : Record.ReleaseDate.Value.ToString("dd/MM/yyyy");
        public decimal TotalRevenue => Record.TotalRevenue;
        public string Director => Record.Director?.Name;

        public string Genres => Record.MovieGenres == null
     ? string.Empty
     : string.Join(",", Record.MovieGenres.Select(mg => mg.Genre?.Name));


        [DisplayName("Genres")]
        public List<int> GenreIds {
            get => Record.MovieGenres?.Select(mg => mg.GenreId).ToList();
            set => Record.MovieGenres = value.Select(v => new MovieGenre() { GenreId = v }).ToList(); 
        }

    }
}
