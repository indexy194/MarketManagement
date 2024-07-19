using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreBusiness
{
    public class ApplicationUser : IdentityUser
    {
        public string ImageUrl { get; set; }
        public virtual ICollection<EmploymentTerms> EmploymentTerms { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<WorkingHours> WorkingHours { get; set; }
    }
    public class UserConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser {Id = "test" }
                );
        }
    }
}
