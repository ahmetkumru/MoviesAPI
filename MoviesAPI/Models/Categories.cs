using System;
using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public partial class Categories
    {
        public Categories()
        {
            MovieCategorie = new HashSet<MovieCategorie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieCategorie> MovieCategorie { get; set; }
    }
}
