using System.Linq;
using AutoMapper;
using MailingNinja.Contracts.Data;
using MailingNinja.Contracts.DTO;
using MediatR;

namespace MailingNinja.Core.Handlers.Items.Queries
{
    public class GetAllForGivenDate : IRequest<IEnumerable<NinjaDTO>>
    {
        public GetAllForGivenDate(DateTime tillDate)
        {
            TillDate = tillDate;
        }

        public DateTime TillDate { get; }
    }

    public class GetAllForGivenDateHandler : IRequestHandler<GetAllForGivenDate, IEnumerable<NinjaDTO>>
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public GetAllForGivenDateHandler(IUnitOfWork db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<NinjaDTO>> Handle(GetAllForGivenDate request, CancellationToken cancellationToken)
        {
            var ninjaEntitiesCreatedTillDate = _db.Ninjas.GetAll().Where(x => x.Added < request.TillDate);
            var mappedDtos = ninjaEntitiesCreatedTillDate.Select(x => _mapper.Map<NinjaDTO>(x));

            return await Task.FromResult<IEnumerable<NinjaDTO>>(mappedDtos);
        }
    }
}