using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ESDAVDomain
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        bool Insert(T entity,string UserId);
        bool Update(T entity, string UserId);
        bool Update(object id, T entity, string UserId);
        bool DeleteEntity(T entity, string UserId);
        T GetById(object id);
        void reload(ref T entity);
        bool Delete(object id, string UserId);
        bool DeleteAll(List<T> list, string UserId);
        bool SaveAll(List<T> list, string UserId);
        bool UpdateAll(List<T> list, string UserId);
    }
}
