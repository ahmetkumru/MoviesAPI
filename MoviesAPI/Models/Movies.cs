using System;
using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public partial class Movies
    {
        public Movies()
        {
            MovieCategorie = new HashSet<MovieCategorie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int? DirectorId { get; set; }

        public virtual Directors Director { get; set; }
        public virtual ICollection<MovieCategorie> MovieCategorie { get; set; }
    }
}
