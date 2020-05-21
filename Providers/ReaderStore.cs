using System.Collections.Generic;

namespace RazorViewEngineApp.Providers
{
    public class ReaderStore
    {
        public static List<User> Users => new List<User>
        {
            new User {
                Id = 1001,
                UserName = "Admin",
                EmailAddress = "reader1001@me.com",
                Role = Roles.Admin
            },
            new User {
                Id = 1002,
                UserName = "Reader",
                EmailAddress = "reader1002@me.com",
                Role = Roles.Reader
            },
            new User {
                Id = 1003,
                UserName = "Editor",
                EmailAddress = "reader1003@me.com",
                Role = Roles.Editor
            }
        };

        public static List<Reader> Readers => new List<Reader>() {
            new Reader {
                Id = 1003,
                EmailAddress = "reader1003@me.com",
                UserName = "reader1003"
            },
            new Reader {
                Id = 1002,
                EmailAddress = "reader1002@me.com",
                UserName = "reader1002"
            }
        };
    }
}