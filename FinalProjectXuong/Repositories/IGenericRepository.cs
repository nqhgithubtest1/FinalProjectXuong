using System.Linq.Expressions;

namespace FinalProjectXuong.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        T FindBy(Expression<Func<T, bool>> predicate);
    }
}
