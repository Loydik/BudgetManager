using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using BudgetManager.Model.Managers;
using BudgetManager.Model.Db;
using System.Windows.Input;
using System.Windows;

namespace BudgetManager.ViewModel.Transactions
{
    public class CreateNewTransactionViewModel : ObservableObject
    {
        private AccountsManager _accManager;
        private TransactionsManager _transManager;
        private FinancialManager _financialManager;
        private DateTime _date;
        private decimal _amount;
        private String _comments;
        private TransactionType _selectedTransactionType;
        private Category _selectedCategory;
        private String _errorMessage;

        private ICommand _createNewTransactionCommand;
        private ICommand _closeWindowCommand;

        private const decimal MaxAmount = 999999999999999999m;
        //private const decimal MinAmount = -999999999999999999m;

        #region Construction

        public CreateNewTransactionViewModel()
        {
            _accManager = new AccountsManager();
            _transManager = new TransactionsManager();
            _financialManager = new FinancialManager();
            Date = DateTime.Now;
            SelectedTransactionType = TransactionTypes.FirstOrDefault();
            SelectedCategory = Categories.FirstOrDefault();
            SelectedAccount = Accounts.FirstOrDefault();

        }

        #endregion


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
                   _amount = Math.Round(value,2);
                    OnPropertyChanged("Amount");
                
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
                        param => CreateNewTransaction((Window)param), param => CreateNewTransactionCanExecute()
                    );
                }
                return _createNewTransactionCommand;
            }
        }

        public void CreateNewTransaction(Window x)
        {
            _transManager.AddTransaction(Date, SelectedAccount.Id, Amount, SelectedCategory.ID, Comments, SelectedTransactionType.ID);
            var addedTransaction = _transManager.Transactions.Last();

            if (Date < DateTime.Today)
            {
                _financialManager.UpdateTransactionsAfterBalancesinAccount(SelectedAccount.Id, Date, addedTransaction.ID);
            }

            _financialManager.UpdateAccountBalance(SelectedAccount.Id, Amount, SelectedTransactionType, addedTransaction.ID);
            
            //We are notifying that Transactions have changed
            Mediator.Instance.NotifyListeners(ViewModelMessages.TransactionsChanged, "TransactionAdded");

            this.CloseWindow(x);
        }

        public Boolean CreateNewTransactionCanExecute()
        {
            
            if(_amount != 0 && 0<=_amount && _amount<=MaxAmount && SelectedCategory != null && SelectedAccount != null && SelectedTransactionType != null)
            {
                return true;
            }
            else { return false; }
        }

        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                {
                    _closeWindowCommand = new RelayCommand(
                        param => CloseWindow((Window) param)
                    );
                }
                return _closeWindowCommand;
            }
        }

        public void CloseWindow(Window x)
        {
            if (x != null)
            {
                x.Close();
            }
        }

        #endregion
    }
}
