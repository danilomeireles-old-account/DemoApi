using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace DemoApi.Persistence.Repositories.Base
{
    public class GenericRepository<T> where T : class
    {
        protected readonly DemoApiContext context;
        private readonly DbSet<T> entity;

        public GenericRepository()
        {
            context = new DemoApiContext();
            entity = context.Set<T>();
        }
                
        public T FindById(params object[] keyValues)
        {
            return entity.Find(keyValues);
        }

        public IEnumerable<T> FindAll()
        {
            return entity.ToList();
        }

        public virtual IEnumerable<T> FindAll(string orderByProperty)
        {
            return entity.ToList().OrderBy(orderByProperty);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return entity.Where(predicate);
        }

        public IEnumerable<T> FindAllByProperty(string propertyName, object propertyValue)
        {            
            return entity.Where(propertyName + "= @0", propertyValue);
        }

        public T FindOneByProperty(string propertyName, object propertyValue)
        {
            //Ex: List<Car> cars = dbContext.Cars.Where("CarYear = @0 and ABS = @1", 2006, true).ToList();
            return entity.Where(propertyName + "= @0", propertyValue).SingleOrDefault();
        }

        public bool Exists(string propertyName, object propertyValue)
        {            
            return entity.Where(propertyName + "= @0", propertyValue).Count() > 0;
        }              
        
        public void Add(T obj)
        {
            entity.Add(obj);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            entity.AddRange(collection);
        }        

        public void Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Remove(T obj)
        {
            entity.Remove(obj);
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            entity.RemoveRange(collection);
        }                    

        public void SaveChanges()
        {            
            context.SaveChanges();            
        }

        public void Dispose()
        {
            context.Dispose();
        }            
    }
}