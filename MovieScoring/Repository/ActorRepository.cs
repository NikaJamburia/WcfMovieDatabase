using MovieScoring.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring.Repository
{
    public class ActorRepository : CrudRepository<Actor>
    {
        public override void Delete(int id)
        {
            Context.Actors.Remove(Context.Actors.Find(id));
            Context.SaveChanges();
        }

        public override Actor Get(int id)
        {
            Actor actor = Context.Actors.Find(id);
            Context.Entry(actor).Collection(d => d.Movies).Load();
            return actor;
        }

        public override List<Actor> GetAll()
        {
            List<Actor> actors = Context.Actors.ToList();
            foreach (Actor actor in actors)
            {
                Context.Entry(actor).Collection(a => a.Movies).Load();
            }

            return actors;
        }

        public override Actor Save(Actor entity)
        {
            Actor newActor = Context.Actors.Add(entity);
            Context.SaveChanges();
            return newActor;
        }

        public override Actor Update(int id, Actor entity)
        {
            Actor actorToUpdate = Context.Actors.Find(id);
            if (actorToUpdate != null)
            {
                Context.Entry(actorToUpdate).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
            return entity;
        }
    }
}