using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.Entity;

namespace DAL.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base()
        {

        }
        public virtual DbSet<Dive> Dives { get; set; }

      
    }
}
