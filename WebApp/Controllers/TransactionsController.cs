using Microsoft.AspNetCore.Mvc;
using CoreBusiness;
using WebApp.ViewModels;
using UseCases;
using Microsoft.AspNetCore.Authorization;
using UseCases.interfaces;

namespace WebApp.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ISearchTransactionsUseCase searchTransactionsUseCase;
        private readonly IListAllTransactionUseCase listAllTransactionUseCase;

        public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase,
            IListAllTransactionUseCase listAllTransactionUseCase)
        {
            this.searchTransactionsUseCase = searchTransactionsUseCase;
            this.listAllTransactionUseCase = listAllTransactionUseCase;
        }

        public IActionResult Index()
        {
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
            transactionsViewModel.Transactions = listAllTransactionUseCase.Execute();
            return View(transactionsViewModel);
        }

        public IActionResult Search(TransactionsViewModel transactionsViewModel)
        {
            var transactions = searchTransactionsUseCase.Execute(
                transactionsViewModel.CashierName??string.Empty,
                transactionsViewModel.StartDate,
                transactionsViewModel.EndDate);

            transactionsViewModel.Transactions = transactions;

            return View("Index", transactionsViewModel);
        }
    }
}
