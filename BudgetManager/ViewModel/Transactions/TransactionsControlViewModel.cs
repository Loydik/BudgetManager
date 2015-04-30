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
            init();
        }

        private void init()
        { 
            db = new Database();
            this.createObservableTransactions(db.Transactions.ToList());
        }

        private void createObservableTransactions(List<Transaction> transactions)
        {
            _allTransactions = new ObservableCollection<TransactionViewModel>();

            if (transactions.Count != 0 && transactions != null)
            {
                foreach (Transaction transactionObj in transactions)
                {
                    _allTransactions.Add(new TransactionViewModel(transactionObj));
                }
            }
        }
    }
}
