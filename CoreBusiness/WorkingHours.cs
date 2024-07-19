using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness
{
    public class WorkingHours : CommonEntity
    {
        public int WorkingHoursID { get; set; }
        public string UserId { get; set; }
        public DateTime StartHours { get; set; }
        public DateTime EndHours { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
    }
    public class WorkingHoursConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingHours>(entity =>
            {
                entity.HasOne(e => e.User)
                      .WithMany(e => e.WorkingHours)
                      .HasForeignKey(e => e.UserId);
            });
        }
    }
}
