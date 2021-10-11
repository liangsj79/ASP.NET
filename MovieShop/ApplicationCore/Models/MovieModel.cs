using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieModel
    {
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public int? RunTime { get; set; }
        public decimal? Rating { get; set; }
    }
}
