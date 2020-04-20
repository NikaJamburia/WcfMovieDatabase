using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScoring.Repository
{
    public abstract class Repository
    {
        protected EFContext Context = EFContext.getInstance();
    }
}