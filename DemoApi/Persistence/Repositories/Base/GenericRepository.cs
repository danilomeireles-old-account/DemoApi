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
        private readonly DemoApiContext context;
        protected readonly DbSet<T> entity;

        public GenericRepository()
        {
            context = new DemoApiContext();
            entity = context.Set<T>();
        }
                
        public virtual T GetById(params object[] keyValues)
        {
            return entity.Find(keyValues);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return entity.ToList();
        }

        public virtual IEnumerable<T> GetAllOrderBy(string orderByProperty)
        {
            return entity.ToList().OrderBy(orderByProperty);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return entity.Where(predicate);
        }

        public virtual IEnumerable<T> GetAllByProperty(string propertyName, object propertyValue)
        {            
            return entity.Where(propertyName + "= @0", propertyValue);
        }

        public virtual IEnumerable<T> GetAllByPropertyILike(string propertyName, string propertyValue)
        {                                  
            return entity.Where(propertyName + ".ToLower().Contains(" + "\"" + propertyValue.ToLower() + "\"" + ")");
        }

        public virtual T GetOneByProperty(string propertyName, object propertyValue)
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