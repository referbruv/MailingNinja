using AutoMapper;
using MailingNinja.Contracts.Data.Entities;
using MailingNinja.Contracts.DTO;

namespace MailingNinja.Core.Mappers
{
    public class NinjaMapperProfile : Profile
    {
        public NinjaMapperProfile()
        {
            CreateMap<Ninja, NinjaDTO>()
                .ForMember(x => x.AddedOn, p => p.MapFrom(x => x.Added.ToShortDateString()))
                .ForMember(x => x.UpdatedOn, p => p.MapFrom(x => x.LastModified.HasValue ? x.LastModified.Value.ToShortDateString() : ""));
        }
    }
}