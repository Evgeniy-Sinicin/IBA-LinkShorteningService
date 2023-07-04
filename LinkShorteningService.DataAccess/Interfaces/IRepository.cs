using System.Linq;

namespace LinkShorteningService.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(object id);
        void Create(T item);
        void Update(T item);
        void Delete(object id);
        void DeleteAll();
    }
}
