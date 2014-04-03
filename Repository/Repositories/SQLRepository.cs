using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SQLRepository : IRepository
    {
        private ObjectContext Context { get; set; }

        public SQLRepository(ObjectContext context)
        {

            try
            {
                //Context = context.CreateObjectSet();
                Context = context;
            }
            catch (Exception ex)
            {
                throw new SQLRepositoryException(string.Format("La entidad no pertenece al contexto. ErrMsg:{0}", ex.Message), ex.InnerException);
            }
        }

        public void Insert<T>(T entity) where T : class
        {
            ObjectSetFromContext<T>().AddObject(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            ObjectSetFromContext<T>().DeleteObject(entity);
        }

        public IQueryable<T> SearchFor<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class
        {
            return ObjectSetFromContext<T>().Where(predicate);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return ObjectSetFromContext<T>();
        }

        public ObjectSet<T> ObjectSetFromContext<T>() where T : class
        {
            return Context.CreateObjectSet<T>();
        }


        public void Update<T>(T entity) where T : class
        {
            ObjectSetFromContext<T>().ApplyCurrentValues(entity);
        }
    }
}
