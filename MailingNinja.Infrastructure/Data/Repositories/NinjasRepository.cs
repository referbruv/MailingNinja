using MailingNinja.Contracts.Data.Entities;
using MailingNinja.Contracts.Data.Repositories;
using MailingNinja.Infrastructure.Data.Context;

namespace MailingNinja.Infrastructure.Data.Repositories
{
    public class NinjasRepository : Repository<Ninja>, INinjasRepository
    {
        public NinjasRepository(DatabaseContext context) : base(context)
        {
        }
    }
}