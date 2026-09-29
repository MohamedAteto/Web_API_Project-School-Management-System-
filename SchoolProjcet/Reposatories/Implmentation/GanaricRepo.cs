
using Microsoft.EntityFrameworkCore;
using School.AppContext;

namespace SchoolProjcet.Reposatories.Implmentation
{
    public class GanaricRepo<T> : IGenaricRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GanaricRepo(AppDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);

            if (item is null)
                throw new ArgumentException($"Entity with id {id} not found.");

            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {

            return _dbSet.Find(id)
                ?? throw new ArgumentException($"Entity with id {id} not found.");
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }


        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }
    }
}
