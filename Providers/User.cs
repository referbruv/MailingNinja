using System.Runtime.Serialization;

namespace RazorViewEngineApp.Providers
{
    [DataContract]
    public class Reader
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string UserName { get; set; }
        
        [DataMember]
        public string EmailAddress { get; set; }
    }

    public class User : Reader
    {
        public string Role { get; set; }
    }

    public class Roles
    {
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Reader = "Reader";
        public const string None = "None";
    }
}