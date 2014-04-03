using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SQLRepositoryException : Exception
    {
        public SQLRepositoryException()
            : base()
        { }

        public SQLRepositoryException(string message)
            : base(message)
        { }

        public SQLRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public void LogError()
        { }
    }

    public class SQLRepositoryByEntityType<T> : IRepository<T> where T : class
    {
        private ObjectSet<T> Context { get; set; }

        public SQLRepositoryByEntityType(ObjectContext context)
        {
            try
            {
                Context = context.CreateObjectSet<T>();
                
            }
            catch (Exception ex)
            {
                throw new SQLRepositoryException(string.Format("La entidad no pertenece al contexto. ErrMsg:{0}", ex.Message), ex.InnerException);
            }
        }

        public void Insert(T entity)
        {

            Context.AddObject(entity);
        }

        public void Delete(T entity)
        {
            Context.DeleteObject(entity);
        }

        public IQueryable<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return Context; 
        }


        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
