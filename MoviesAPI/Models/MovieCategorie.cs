using System;
using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public partial class MovieCategorie
    {
        public int MovieId { get; set; }
        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Movies Movie { get; set; }
    }
}
