using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using BudgetManager.Model.Managers;
using BudgetManager.Model.Db;

namespace BudgetManager.ViewModel.Transactions
{
    public class CreateNewTransactionViewModel : ObservableObject
    {
        private AccountsManager _accManager;
        private TransactionsManager _transManager;
        private DateTime _date;
        private decimal _amount;
        private String _comments;
        private TransactionType _selectedTransactionType;
        private Category _selectedCategory;

        public CreateNewTransactionViewModel()
        {
            _accManager = new AccountsManager();
            _transManager = new TransactionsManager();
        }

        public List<TransactionType> TransactionTypes
        {
            get { return _transManager.TransactionTypes; }
        }

        public TransactionType SelectedTransactionType
        {
            get
            { return _selectedTransactionType; }
            set
            {
                _selectedTransactionType = value;
                OnPropertyChanged("SelectedTransactionType");
            }
        }


        public DateTime Date
        {
            get { return _date; }
            set
            {
                    _date = value;
                    OnPropertyChanged("Date");
            }
        }

        public List<Account> Accounts
        {
            get { return _accManager.Accounts; }
        }

        public Account SelectedAccount
        { 
            get; set; 
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (value != 0)
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }

        public List<Category> Categories
        {
            get { return _transManager.TransactionCategories; }
        }

        public Category SelectedCategory
        {
            get
            { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public String Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");

            }
        }


    }
}
