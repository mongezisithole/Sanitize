using Sanitize.Core;

namespace Sanitize.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        T? GetById(object id);

        IQueryable<T> GetAll { get; }
    }
}
