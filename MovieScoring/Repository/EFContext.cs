using MovieScoring.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieScoring.Repository
{
    public class EFContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }

        private EFContext() : base("name=MovieScoringDatabase")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private static EFContext instance;

        public static EFContext getInstance()
        {
            if(instance == null)
            {
                instance = new EFContext();
            }
            return instance;
        }
    }
}