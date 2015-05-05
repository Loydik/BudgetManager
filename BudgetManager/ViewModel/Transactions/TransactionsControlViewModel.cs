using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Collections.ObjectModel;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;

namespace BudgetManager.ViewModel.Transactions
{
    public class TransactionsControlViewModel : ObservableObject, IPageViewModel
    {   
        private TransactionsManager _transManager;
        private ObservableCollection<TransactionViewModel> _allTransactions;

        public String Name
        {
            get { return "Transactions"; }
        }

        public ObservableCollection<TransactionViewModel> AllTransactions
        {
            get
            { return _allTransactions; }
            set
            {
                _allTransactions = value;
                OnPropertyChanged("AllTransactions");
            }
        }


        public TransactionsControlViewModel()
        {
            _transManager = new TransactionsManager();
            _allTransactions = Util.ConversionHelper.toObservableCollection<TransactionViewModel, Transaction>(_transManager.Transactions, l => new TransactionViewModel(l));//getting data from db and converting into Observable list
        }

    }
}
