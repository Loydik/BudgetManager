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
    public class EditTransactionWindowViewModel : ObservableObject
    {

        private AccountsManager _accManager;
        private TransactionsManager _transManager;
        private FinancialManager _financialManager;
        private DateTime _date;
        private decimal _amount;
        private String _comments;
        private TransactionType _selectedTransactionType;
        private Category _selectedCategory;
        private Account _selectedAccount;
        private String _errorMessage;
        private int? _transactionID;

        private ICommand _updateTransactionCommand;
        private ICommand _closeWindowCommand;

        private decimal maxAmount;
        private decimal minAmount;


        public EditTransactionWindowViewModel(TransactionViewModel model)
        {
            _transactionID = model.TransactionID;
            _accManager = new AccountsManager();
            _transManager = new TransactionsManager();
            _financialManager = new FinancialManager();
            Date = model.Date;
            maxAmount = 999999999999999999m;
            minAmount = -999999999999999999m;
            Amount = model.Amount;
            Comments = model.Comments;
            SelectedTransactionType = _transManager.TransactionTypes.Single(n => n.ID == model.TransactionObj.TransactionType.ID);
            SelectedAccount = _accManager.Accounts.Single(n => n.Id == model.TransactionObj.Account.Id);
            SelectedCategory = _transManager.TransactionCategories.Single(n => n.ID == model.TransactionObj.Category.ID);
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
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
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


        public ICommand UpdateTransactionCommand
        {
            get
            {
                if (_updateTransactionCommand == null)
                {
                    _updateTransactionCommand = new RelayCommand(
                        param => UpdateTransaction((Window)param), param => UpdateTransactionCanExecute()
                    );
                }
                return _updateTransactionCommand;
            }
        }

        public void UpdateTransaction(Window x)
        {
            _transManager.UpdateTransactionFields(_transactionID, Date, SelectedAccount.Id, Amount, SelectedCategory.ID, Comments, SelectedTransactionType.ID);
            _transManager.UpdateTransactions();
            _financialManager.UpdateTransactionsAfterBalancesinAccount(SelectedAccount.Id, Date, _transactionID);
            _financialManager.RefreshAccountBalance(SelectedAccount.Id);

            this.CloseWindow(x);
        }

        public Boolean UpdateTransactionCanExecute()
        {
            
            if(_amount != 0 && minAmount<=_amount && _amount<=maxAmount && SelectedCategory != null && SelectedAccount != null && SelectedTransactionType != null)
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
