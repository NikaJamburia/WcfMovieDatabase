using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScoring.Repository
{
    public abstract class CrudRepository<T> : Repository
    {
        public abstract T Save(T entity);
        public abstract List<T> GetAll();
        public abstract T Get(int id);
        public abstract void Delete(int id);
        public abstract T Update(int id, T entity);
    }
}
