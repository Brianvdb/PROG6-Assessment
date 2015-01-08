using DomainModel.Database;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext database;

        public EntityRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public T Get(int id)
        {
            return database.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return database.Set<T>().ToList();
        }

        public T Add(T t)
        {
            //database.Set<T>().Add(t);
            database.Entry(t).State = EntityState.Added; 
            database.SaveChanges();
            return t;
        }

        public T Delete(T t)
        {
            database.Set<T>().Remove(t);
            database.SaveChanges();
            return t;
        }

        public void Update()
        {
            database.SaveChanges();
        }

        public void UpdateDatabase()
        {
            database.SaveChanges();
        }
    }
}
