using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plugins.DataStore.SQL
{
    public class TransactionSQLRepository : ITransactionRepository
    {
        private readonly MarketContext db;

        public TransactionSQLRepository(MarketContext db)
        {
            this.db = db;
        }

        public void Add(string cashierName, string userId, int productId, string productName, double price, int beforeQty, int soldQty)
        {
            var transaction = new Transaction
            {
                ProductId = productId,
                UserId = userId,
                ProductName = productName,
                TimeStamp = DateTime.Now,
                Price = price,
                BeforeQty = beforeQty,
                SoldQty = soldQty,
                CashierName = cashierName
            };

            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
            {
                return db.Transactions.Where(x => x.TimeStamp.Date == date.Date);
            }
            else
            {
                return db.Transactions.Where(x =>
                    EF.Functions.Like(x.CashierName, $"%{cashierName}%") &&
                    x.TimeStamp.Date == date.Date);
            }
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return db.Transactions.ToList();
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
            {
                if (startDate.Date == endDate.Date) return db.Transactions.Where(x => x.TimeStamp.Date == endDate.Date);
                return db.Transactions.Where(x => 
                    x.TimeStamp.Date >= startDate.Date && 
                    x.TimeStamp.Date <= endDate.Date);
            }
            else
            {
                
                return db.Transactions.Where(x =>
                    x.CashierName.Contains(cashierName) &&
                    x.TimeStamp.Date >= startDate.Date &&
                    x.TimeStamp.Date <= endDate.Date);
            }
        }
    }
}
