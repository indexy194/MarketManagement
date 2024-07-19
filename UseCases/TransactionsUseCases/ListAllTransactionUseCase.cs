using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;
using UseCases.interfaces;

namespace UseCases.TransactionsUseCases
{
    public class ListAllTransactionUseCase : IListAllTransactionUseCase
    {
        private readonly ITransactionRepository transactionRepositorys;

        public ListAllTransactionUseCase(ITransactionRepository transactionRepositorys)
        {
            this.transactionRepositorys = transactionRepositorys;
        }
        public IEnumerable<Transaction> Execute()
        {
            return transactionRepositorys.GetTransactions();
        }
    }
}
