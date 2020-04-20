using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring.Model
{
    public class Director : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public Director()
        {
        }
        public Director(int id, string name, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}