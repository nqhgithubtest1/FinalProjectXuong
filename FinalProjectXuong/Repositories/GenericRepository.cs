
using FinalProjectXuong.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProjectXuong.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public List<T> GetAll()
        {
            return _dbset.ToList();
        }

        public T GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public void Insert(T entity)
        {
            _dbset.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbset.SingleOrDefault(predicate);
        }
    }
}
