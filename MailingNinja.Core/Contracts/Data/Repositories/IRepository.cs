namespace MailingNinja.Contracts.Data.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();
        T Get(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
        int Count();
    }
}