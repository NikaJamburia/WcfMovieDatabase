using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieScoring.Model
{
    public class Movie : BaseModel { 
        public string Name { get; set; }
        public int Score { get; set; }
        [DataType(DataType.Date)]
        public DateTime AirDate { get; set; }
        public Director Director { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }

        public Movie() {}
        public Movie(int id, string name, int score, DateTime airDate, Director director, ICollection<Actor> actors, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Score = score;
            AirDate = airDate;
            Director = director;
            Actors = actors;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}