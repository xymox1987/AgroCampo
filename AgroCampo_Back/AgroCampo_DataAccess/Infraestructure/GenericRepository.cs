using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ESDAVDomain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ESDAVDataAccess.Infraestructure
{
    public class GenericRepository<T, TContext> : IGenericRepository<T> where T : class where TContext : DbContext
    {
        public readonly TContext context;

        public GenericRepository(TContext context)
        {
            this.context = context;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {

            try
            {
                var query = context.Set<T>().Where(predicate);
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return context.Set<T>();//.Where(i => i.fechaEliminacion == null); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(T entity, string UserId)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                context.Add(entity);
                context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(T entity, string UserId)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(object id, T entity, string UserId)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }


                var original = context.Set<T>().Find(id);

                

                if (original != null)
                {
                    context.Entry(original).CurrentValues.SetValues(entity);



                    context.SaveChanges();

                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool DeleteEntity(T entity, string UserId)
        {
            try
            {
                if (entity == null) return false;

                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();


                return true;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public T GetById(object id)
        {
            try
            {
                T entity = null;

                if (id.GetType() == typeof(System.Object[]))
                {
                    var param_S =((System.Object[]) id).Cast<object>().ToArray();
                    entity = context.Set<T>().Find(param_S);
                }
                else
                {
                    entity = context.Set<T>().Find(id);
                }

                return entity == null ? null : entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> Table => context.Set<T>();

        public bool Delete(object id, string UserId)
        {
            try
            {


                var entity = GetById(id);
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                 
                this.context.Entry(entity).State = EntityState.Deleted;
                this.context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public bool DeleteAll(List<T> list, string UserId)
        {
            try
            {
                context.Set<T>().RemoveRange(list);
                this.context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public bool SaveAll(List<T> list, string UserId)
        {
            try
            {

                context.Set<T>().AddRange(list);
                context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateAll(List<T> list, string UserId)
        {
            try
            {
               
                context.Set<T>().UpdateRange(list);
                context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void reload(ref T entity)
        {
            context.Entry(entity).Reload();

        }
    }
}
