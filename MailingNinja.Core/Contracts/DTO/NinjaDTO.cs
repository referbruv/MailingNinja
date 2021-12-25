using MailingNinja.Contracts.Data.Entities;

namespace MailingNinja.Contracts.DTO
{
    public class NinjaDTO
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Class { get; set; }
        public string ColorCode { get; set; }
        public string AddedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}