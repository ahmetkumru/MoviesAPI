using System;
using System.Collections.Generic;

namespace MoviesAPI.Models
{
    public partial class Directors
    {
        public Directors()
        {
            Movies = new HashSet<Movies>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movies> Movies { get; set; }
    }
}
