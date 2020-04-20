using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MovieScoring.Repository
{
    public class DbContextFactory : IDbContextFactory<EFContext>
    {
        public EFContext Create()
        {
            return EFContext.getInstance();
        }
    }
}