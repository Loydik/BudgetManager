using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Collections.ObjectModel;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;
using System.Windows.Input;
using BudgetManager.View.TransactionsTab;
using System.Windows;

namespace BudgetManager.ViewModel.Transactions
{
    public class TransactionsControlViewModel : ObservableObject, IPageViewModel
    {   
        private TransactionsManager _transManager;
        private FinancialManager _financialManager;
        private ObservableCollection<TransactionViewModel> _allTransactions;
        private ICommand _openCreateNewTransactionWindowCommand;
        private ICommand _refreshCommand;
        private ICommand _deleteTransactionCommand;
        private ICommand _openEditTransactionWindowCommand;
        private IWindowFactory _windowFactory;

        public string Name
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
            _financialManager = new FinancialManager();
            Init();
            _windowFactory = new ProductionWindowFactory();
        }

        private void Init()
        {
            var sortedTransactions = _transManager.Transactions.OrderByDescending(n => n.Date).ThenByDescending(n=>n.ID).ToList();
            AllTransactions = ConversionHelper.toObservableCollection(sortedTransactions, l => new TransactionViewModel(l));//getting data from manager and converting into Observable list
        }

        public ICommand OpenCreateNewTransactionWindowCommand
        {
            get
            {
                if (_openCreateNewTransactionWindowCommand == null)
                {
                    _openCreateNewTransactionWindowCommand = new RelayCommand(
                        param => _windowFactory.CreateNewWindow(new CreateNewTransactionViewModel(), new CreateNewTransactionWindow())//, param => CreateNewTransactionCanExecute()
                    );
                }
                return _openCreateNewTransactionWindowCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new RelayCommand(
                        param => Refresh()
                    );
                }
                return _refreshCommand;
            }
        }

        private void Refresh()
        {
            _transManager.UpdateTransactions();
            Init();
        }

        public ICommand OpenEditTransactionWindowCommand
        {
            get
            {
                if (_openEditTransactionWindowCommand == null)
                {
                    _openEditTransactionWindowCommand = new RelayCommand(
                        param => _windowFactory.CreateNewWindow(new EditTransactionWindowViewModel((TransactionViewModel)param), new EditTransactionWindow())
                    );
                }
                return _openEditTransactionWindowCommand;
            }
        }


        public ICommand DeleteTransactionCommand
        {
            get
            {
                if (_deleteTransactionCommand == null)
                {
                    _deleteTransactionCommand = new RelayCommand(
                        param => DeleteTransaction((TransactionViewModel)param)
                    );
                }
                return _deleteTransactionCommand;
            }
        }

        private void DeleteTransaction(TransactionViewModel trans)
        {
            int? accountId = trans.AccountId;

            _transManager.DeleteTransaction(trans.TransactionID);
            _transManager.UpdateTransactions();
            _financialManager.UpdateTransactionsAfterBalancesinAccount(accountId, trans.Date, trans.TransactionID);
            _financialManager.RefreshAccountBalance(accountId);
            Refresh();
        }

      

    }
}
