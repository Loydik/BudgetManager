using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using BudgetManager.Model.Managers;
using BudgetManager.Model.Db;
using System.Windows.Input;

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
        private String _errorMessage;

        private ICommand _createNewTransactionCommand;


        public CreateNewTransactionViewModel()
        {
            _accManager = new AccountsManager();
            _transManager = new TransactionsManager();
            Date = DateTime.Now;
            _comments = "";
            _errorMessage = "You have an error!";
        }

        #region Properties

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

        public String ErrorMessage
        {
            get
            { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        #endregion // Properties


        #region ICommands

        public ICommand CreateNewTransactionCommand
        {
            get
            {
                if (_createNewTransactionCommand == null)
                {
                    _createNewTransactionCommand = new RelayCommand(
                        param => CreateNewTransaction(), param => CreateNewTransactionCanExecute()
                    );
                }
                return _createNewTransactionCommand;
            }
        }

        public void CreateNewTransaction()
        {
            
            //Transaction newTransaction = new Transaction(Date, SelectedAccount, Amount, _transManager.Currencies.First(), SelectedCategory, Comments, SelectedTransactionType);
            _transManager.addTransaction(Date, SelectedAccount, Amount, _transManager.Currencies.First(), SelectedCategory, Comments, SelectedTransactionType);
        }

        public Boolean CreateNewTransactionCanExecute()
        {
            
            if(_amount != 0)
            {
                return true;
            }
            else { return false; }
        }

        #endregion
    }
}
