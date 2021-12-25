namespace MailingNinja.Contracts.Data.Entities
{
    public abstract class AuditableEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Added { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }

    public class Ninja : AuditableEntity
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Class { get; set; }
        public string ColorCode { get; set; }
    }
}