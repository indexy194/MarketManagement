using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness
{
    public class EmploymentTerms : CommonEntity
    {
        public int EmploymentTermsId { get; set; }
        public string UserId { get; set; }
        public int AgreedSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ApplicationUser User { get; set; }
    }
    public class EmploymentTermsConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmploymentTerms>(entity =>
            {
                entity.HasOne(e => e.User)
                .WithMany(e => e.EmploymentTerms)
                .HasForeignKey(e => e.UserId);
            });
        }
    }
}
