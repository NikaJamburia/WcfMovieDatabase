using MovieScoring.Model;
using MovieScoring.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring
{
    public class ReviewRepository : CrudRepository<Review>
    {
        public override void Delete(int id)
        {
            Context.Reviews.Remove(Context.Reviews.Find(id));
            Context.SaveChanges();
        }

        public override Review Get(int id)
        {
            Review review = Context.Reviews.Find(id);
            Context.Entry(review).Reference(r => r.Movie).Load();
            Context.Entry(review.Movie).Reference(r => r.Director).Load();


            return review;
        }

        public override List<Review> GetAll()
        {
            List<Review> reviews = Context.Reviews.ToList();
            foreach (Review review in reviews)
            {
                Context.Entry(review).Reference(r => r.Movie).Load();
                Context.Entry(review.Movie).Reference(r => r.Director).Load();
            }

            return reviews;
        }

        public override Review Save(Review entity)
        {
            Review review = Context.Reviews.Add(entity);
            Context.SaveChanges();
            return review;
        }

        public override Review Update(int id, Review entity)
        {
            Review reviewToUpdate = Context.Reviews.Find(id);
            if (reviewToUpdate != null)
            {
                Context.Entry(reviewToUpdate).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
            return entity;
        }
    }
}