using MovieScoring.Model;
using MovieScoring.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring
{
    public class MovieRepository : CrudRepository<Movie>
    {
        public override void Delete(int id)
        {
            Context.Movies.Remove(Context.Movies.Find(id));
            Context.SaveChanges();
        }

        public override Movie Get(int id)
        {
            Movie movie = Context.Movies.Find(id);
            Context.Entry(movie).Reference(m => m.Director).Load();
            Context.Entry(movie).Collection(m => m.Actors).Load();
            return movie;
        }

        public override List<Movie> GetAll()
        {
            List<Movie> movies = Context.Movies.ToList();
            foreach (Movie movie in movies)
            {
                Context.Entry(movie).Reference(m => m.Director).Load();
                Context.Entry(movie).Collection(m => m.Actors).Load();
            }

            return movies;
        }

        public override Movie Save(Movie entity)
        {
            Movie newMovie = Context.Movies.Add(entity);
            Context.SaveChanges();
            return newMovie;
        }

        public override Movie Update(int id, Movie entity)
        {
            Movie movieToUpdate = Context.Movies.Find(id);
            if (movieToUpdate != null)
            {
                Context.Entry(movieToUpdate).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
            return entity;
        }
    }
}