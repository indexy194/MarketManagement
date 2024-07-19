using Microsoft.EntityFrameworkCore;

namespace CoreBusiness
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = ""; // in case product name changes
        public double Price { get; set; }
        public int BeforeQty { get; set; }
        public int SoldQty { get; set; }
        public string CashierName { get; set; } = "";
        public ApplicationUser User { get; set; }
    }
    public class TransactionsConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(e => e.User)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.UserId);
            });
        }
    }
}
