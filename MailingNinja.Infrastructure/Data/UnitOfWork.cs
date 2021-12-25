using MailingNinja.Contracts.Data;
using MailingNinja.Contracts.Data.Repositories;
using MailingNinja.Infrastructure.Data.Context;
using MailingNinja.Infrastructure.Data.Repositories;

namespace MailingNinja.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            this._context = context;
        }

        public INinjasRepository Ninjas => new NinjasRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}