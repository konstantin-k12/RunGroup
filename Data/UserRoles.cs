using Microsoft.AspNetCore.Identity;

namespace RunGroupWebApp.Data
{
    public class UserRoles : IdentityRole
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
}
