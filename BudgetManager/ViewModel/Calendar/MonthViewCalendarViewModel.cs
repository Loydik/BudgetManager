using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Managers;
using BudgetManager.ViewModel.Transactions;
using BudgetManager.ViewModel.Util;

namespace BudgetManager.ViewModel.Calendar
{
    public class MonthViewCalendarViewModel : ObservableObject
    {
        private TransactionsManager _transactionsManager;
        private ObservableCollection<TransactionViewModel> _allTransactions;

         public ObservableCollection<TransactionViewModel> Transactions
        {
            get
            { return _allTransactions; }
            set
            {
                _allTransactions = value;
                OnPropertyChanged("Transactions");
            }
        }

         public MonthViewCalendarViewModel()
        {
            _transactionsManager = new TransactionsManager();
            Init();
        }

        private void Init()
        {
            var sortedTransactions = _transactionsManager.Transactions.OrderBy(n => n.Date).ToList();
            Transactions = ConversionHelper.ToObservableCollection(sortedTransactions, l => new TransactionViewModel(l));//getting data from manager and converting into Observable list
        }
    }
}
