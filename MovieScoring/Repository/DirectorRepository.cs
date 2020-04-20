using MovieScoring.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring.Repository
{
    public class DirectorRepository : CrudRepository<Director>
    {
        public override void Delete(int id)
        {
            Context.Directors.Remove(Context.Directors.Find(id));
            Context.SaveChanges();
        }

        public override Director Get(int id)
        {
            Director director = Context.Directors.Find(id);
            Context.Entry(director).Collection(d => d.Movies).Load();
            return director;
        }

        public override List<Director> GetAll()
        {
            List<Director> directors = Context.Directors.ToList();
            foreach (Director director in directors)
            {
                Context.Entry(director).Reference(d => d.Movies).Load();
            }

            return directors;
        }

        public override Director Save(Director entity)
        {
            Director newDirector = Context.Directors.Add(entity);
            Context.SaveChanges();
            return newDirector;
        }

        public override Director Update(int id, Director entity)
        {
            Director directorToUpdate = Context.Directors.Find(id);
            if (directorToUpdate != null)
            {
                Context.Entry(directorToUpdate).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
            return entity;
        }
    }
}