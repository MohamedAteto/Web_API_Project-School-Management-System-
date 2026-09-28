namespace SchoolProjcet.Reposatories.Implmentation
{
    public interface IGenaricRepo<T> where T : class
    {
        public ICollection<T> GetAll();
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        public T GetById(int id);
        public IQueryable<T> GetQueryable();
    }
}
