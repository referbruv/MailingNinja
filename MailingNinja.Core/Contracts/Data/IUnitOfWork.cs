using MailingNinja.Contracts.Data.Repositories;

namespace MailingNinja.Contracts.Data
{
    public interface IUnitOfWork
    {
        INinjasRepository Ninjas { get; }
        Task<int> CommitAsync();
    }
}