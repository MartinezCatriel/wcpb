using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Repository.Repositories
{
    public class XMLRepository<T> : IRepository<T> where T : class
    {
        protected object xml;

        public XMLRepository(string filename)
        {
            /*FileName = filename;

            XmlSerializer serializer = new XmlSerializer(typeof(List<Configuration>)
                , null
                , new Type[] { typeof(BinaryConfiguration) }
                , new XmlRootAttribute("Configurations")
                , "http://ofimbres.wordpress.com/");
            using (StreamReader myWriter = new StreamReader(FileName))
            {
                configurations = (List<Configuration>)serializer.Deserialize(myWriter);
                myWriter.Close();
            }*/
        }

        public void Insert(T entity)
        {

        }

        public void Delete(T entity)
        {

        }

        public IQueryable<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }


        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
