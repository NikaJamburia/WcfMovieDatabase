using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScoring.Model
{
    public class Actor : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

        public Actor() {}
        public Actor(int id, string name, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
