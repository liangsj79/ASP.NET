using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Models
{
    public class MovieDetailModel
    {
        // Movie banner properties
        public string BackdropUrl { get; set; }

        public string PosterUrl { get; set; }

        public string Title { get; set; }

        public string Tagline { get; set; }

        public int? RunTime { get; set; }

        public int ReleaseYear { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public decimal? Rating { get; set; }

        public string Overview { get; set; }

        public decimal? Price { get; set; }

        // Movie facts properties
        public string ReleaseDate { get; set; }

        public string Revenue { get; set; }

        public string Budget { get; set; }

        public string ImdbUrl { get; set; }

        // Trailers property
        public IEnumerable<Trailer> Trailers { get; set; }

        // Casts property
        public IEnumerable<CastResponseModel> Casts { get; set; }

        public IEnumerable<Genre> AllGenres { get; set; }

    }
}
