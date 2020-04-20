using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring.Model
{
    public class Review : BaseModel
    {
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public Movie Movie { get; set; }
        public int Score { get; set; }

        public Review() {}

        public Review(int id, string text, string authorname, Movie movie, int score, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Text = text;
            AuthorName = authorname;
            Movie = movie;
            Score = score;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}