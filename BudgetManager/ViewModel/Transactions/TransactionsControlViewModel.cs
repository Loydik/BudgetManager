using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Collections.ObjectModel;
using BudgetManager.Model.Db;

namespace BudgetManager.ViewModel.Transactions
{
    public class TransactionsControlViewModel : ObservableObject
    {
        private ObservableCollection<TransactionViewModel> _allTransactions;
        private Database db;

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
             db = new Database();
            _allTransactions = Util.ConversionHelper.toObservableCollection<TransactionViewModel, Transaction>(db.Transactions.ToList(), l => new TransactionViewModel(l));//getting data from db and converting into Observable list
        }


    }
}
