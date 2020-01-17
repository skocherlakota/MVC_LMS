using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC_LMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {  
        }
        public DbSet<MVC_LMS.Models.Book> Book { get; set; }
        public DbSet<MVC_LMS.Models.Copi> Copi { get; set; }
        public DbSet<MVC_LMS.Models.Reserve> Reserves { get; set; }
        public DbSet<MVC_LMS.Models.Borrow> Borrows  { get; set; }
    }
}
