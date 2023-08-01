using Microsoft.EntityFrameworkCore;
using Sanitize.Core;
using Sanitize.Data.Context;
using Sanitize.Repository.Interfaces;

namespace Sanitize.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SensitiveContext _context;
        private DbSet<T>? _entities;

        public Repository(SensitiveContext context)
        {
            _context = context;
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw;
            }
        }

        public virtual IQueryable<T> GetAll
        {
            get
            {
                return Entities;
            }
        }

        public T? GetById(object id)
        {
            return Entities.Find(id);
        }

        private DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }

                return _entities;
            }
        }
    }
}
