using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace hospitalAPI.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public string RoleOfUser{ get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Contract { get; set; }
        public string License { get; set; }
        public string PESEL { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("hospitalDBContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<hospitalAPI.Models.User> Users { get; set; }


        public System.Data.Entity.DbSet<hospitalAPI.Models.Patient> Patients { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<hospitalAPI.Models.Photo> Photos { get; set; }
    }
}