using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

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
        
        public virtual T GetById(int id, string[] includes)
        {
            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);
            
            return query.AsNoTracking().Where("Id" + "= @0", id).FirstOrDefault();
        }        

        public virtual T GetById(string idPropertyName, object idPropertyValue, string[] includes)
        {
            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);

            return query.AsNoTracking().Where(idPropertyName + "= @0", idPropertyValue).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {            
            return entity.ToList();
        }

        public virtual IEnumerable<T> GetAll(string[] includes)
        {
            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);

            return query.ToList();
        }

        public virtual IEnumerable<T> GetAllOrderBy(string orderByProperty)
        {
            return entity.ToList().OrderBy(orderByProperty);
        }

        public virtual IEnumerable<T> GetAllOrderBy(string orderByProperty, string[] includes)
        {
            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);

            return query.OrderBy(orderByProperty).ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return entity.Where(predicate);
        }
                
        public virtual IEnumerable<T> GetAllByProperty(string propertyName, object propertyValue)
        {
            Type propertyType = GetPropertyType(propertyName);
            return entity.Where(propertyName + "= @0", Convert.ChangeType(propertyValue, propertyType));
        }        
                
        public virtual IEnumerable<T> GetAllByProperty(string propertyName, string propertyValue, string[] includes)
        {
            Type propertyType = GetPropertyType(propertyName);

            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);
           
            return query.Where(propertyName + "= @0", Convert.ChangeType(propertyValue, propertyType));
        }        

        public virtual IEnumerable<T> GetAllByProperty(string propertyName, object propertyValue, string[] includes)
        {
            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);
            
            int intValue;
            bool isInt = int.TryParse(propertyValue.ToString(), out intValue);

            if (isInt)                           
                return query.Where(propertyName + "= @0", Convert.ToInt32(propertyValue));
            else
                return query.Where(propertyName + "= @0", propertyValue);
        }        

        public virtual IEnumerable<T> GetAllByPropertyILike(string propertyName, string propertyValue)
        {
            return entity.Where(propertyName + ".ToLower().Contains(" + "\"" + propertyValue.ToLower() + "\"" + ")");
        }

        public virtual IEnumerable<T> GetAllByPropertyILike(string propertyName, string propertyValue, string[] includes)
        {
            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);

            return query.Where(propertyName + ".ToLower().Contains(" + "\"" + propertyValue.ToLower() + "\"" + ")");
        }

        public virtual T GetOneByProperty(string propertyName, object propertyValue)
        {
            Type propertyType = GetPropertyType(propertyName);
            return entity.Where(propertyName + "= @0", Convert.ChangeType(propertyValue, propertyType)).SingleOrDefault();
        }

        public virtual T GetOneByProperty(string propertyName, object propertyValue, string[] includes)
        {
            Type propertyType = GetPropertyType(propertyName);

            var query = entity.AsQueryable();
            foreach (string include in includes)
                query = query.Include(include);

            return query.Where(propertyName + "= @0", Convert.ChangeType(propertyValue, propertyType)).SingleOrDefault();
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

        private Type GetPropertyType(string propertyName)
        {
            Type propertyType = null;

            if (!propertyName.Contains("."))
                propertyType = typeof(T).GetProperty(propertyName).PropertyType;
            else
            {
                string[] propertyNames = propertyName.Split(new char[] { '.' }, StringSplitOptions.None);
                propertyType = typeof(T).GetProperty(propertyNames[0]).PropertyType;

                for (int i = 1; i < propertyNames.Length; i++)
                    propertyType = propertyType.GetProperty(propertyNames[i]).PropertyType;
            }

            return propertyType;
        }
    }
}