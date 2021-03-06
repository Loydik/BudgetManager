﻿using System;
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
        private ObservableCollection<TransactionViewModel> _transactionsToDisplay;
        private List<String> _filterTypes;
        private string _selectedFilterType;
        private string _filterString;
        private ICommand _openCreateNewTransactionWindowCommand;
        private ICommand _refreshCommand;
        private ICommand _deleteTransactionCommand;
        private ICommand _openEditTransactionWindowCommand;
        private IWindowFactory _windowFactory;
        private ICommand _searchTransactionsCommand;
        private ICommand _showAllTransactionsCommand;
        private string _mediatorTransactionsMessage;
        private string _mediatorCategoriesMessage;
        private string _mediatorAccountsMessage;

        #region Properties

        public string Name
        {
            get { return "Transactions"; }
        }

        public ObservableCollection<TransactionViewModel> AllTransactions
        {
            get { return _allTransactions; }
            set
            {
                _allTransactions = value;
                OnPropertyChanged("AllTransactions");
            }
        }

        public ObservableCollection<TransactionViewModel> TransactionsToDisplay
        {
            get { return _transactionsToDisplay; }
            set
            {
                _transactionsToDisplay = value;
                OnPropertyChanged("TransactionsToDisplay");
            }
        }

        public List<String> FilterTypes
        {
            get { return _filterTypes; }
            set
            {
                _filterTypes = value;
                OnPropertyChanged("FilterTypes");
            }
        }

        public String SelectedFilterType
        {
            get { return _selectedFilterType; }
            set
            {
                _selectedFilterType = value;
                OnPropertyChanged("SelectedFilterType");
            }
        }

        public String FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged("FilterString");
            }
        }

        public String MediatorTransactionsMessage
        {
            get { return _mediatorTransactionsMessage; }
            set {
                if (value == "TransactionAdded" || value == "TransactionEdited")
                if (value == "TransactionAdded" || value == "TransactionEdited")
                    {
                     _mediatorTransactionsMessage = value; 
                     Refresh();
                     OnPropertyChanged("MediatorTransactionsMessage");
                }
            }
        }

        public String MediatorCategoriesMessage
        {
            get { return _mediatorCategoriesMessage; }
            set
            {
                if (value == "CategoryDeleted")
                {
                    _mediatorCategoriesMessage = value;
                    Refresh();
                    OnPropertyChanged("MediatorCategoriesMessage");
                }
            }
        }

        public String MediatorAccountsMessage
        {
            get { return _mediatorAccountsMessage; }
            set
            {
                if (value == "AccountDeleted")
                {
                    _mediatorAccountsMessage = value;
                    _financialManager.UpdateAccounts();
                    Refresh();
                }
                else if (value == "AccountAdded")
                {
                    _mediatorAccountsMessage = value;
                    _financialManager.UpdateAccounts();
                }
                OnPropertyChanged("MediatorCategoriesMessage");
                }
        }

        #endregion


        public TransactionsControlViewModel()
        {
            _transManager = new TransactionsManager();
            _financialManager = new FinancialManager();
            Init();
            _windowFactory = new ProductionWindowFactory();
            _filterTypes = new List<string>(){"Accounts", "Categories", "Comments"};
            _selectedFilterType = _filterTypes.FirstOrDefault(); 
            
            //register to the mediator for the 
            //TransactionsChanged message
            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorTransactionsMessage = (String)o; }, ViewModelMessages.TransactionsChanged);

            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorCategoriesMessage = (String)o; }, ViewModelMessages.CategoriesChanged);

            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorAccountsMessage = (String)o; }, ViewModelMessages.AccountsChanged);
        }

        private void Init()
        {
            var sortedTransactions = _transManager.Transactions.OrderByDescending(n => n.Date).ThenByDescending(n=>n.ID).ToList();
            AllTransactions = ConversionHelper.ToObservableCollection(sortedTransactions, l => new TransactionViewModel(l));//getting data from manager and converting into Observable list
            TransactionsToDisplay = AllTransactions;
        }

        #region ICommands

        public ICommand OpenCreateNewTransactionWindowCommand
        {
            get
            {
                if (_openCreateNewTransactionWindowCommand == null)
                {
                    _openCreateNewTransactionWindowCommand = new RelayCommand(
                        param =>
                            OpenCreateNewTransactionWindow(new CreateNewTransactionViewModel(),
                                new CreateNewTransactionWindow()) //, param => CreateNewTransactionCanExecute()
                        );
                }
                return _openCreateNewTransactionWindowCommand;
            }
        }

        private void OpenCreateNewTransactionWindow(CreateNewTransactionViewModel viewModel, CreateNewTransactionWindow window)
        {
            if (_financialManager.Accounts.Any())
            {
                _windowFactory.CreateNewWindow(viewModel, window);
            }
            else
            {
                _windowFactory.ShowMessage("You have to create at least one account");
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
                        param =>
                            _windowFactory.CreateNewWindow(
                                new EditTransactionWindowViewModel((TransactionViewModel) param),
                                new EditTransactionWindow())
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
                        param => DeleteTransaction((TransactionViewModel) param)
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
            Mediator.Instance.NotifyListeners(ViewModelMessages.TransactionsChanged, "TransactionDeleted");
        }

        #endregion

        
        public ICommand SearchTransactionsCommand
        {
             get
            {
                if (_searchTransactionsCommand == null)
                {
                    _searchTransactionsCommand = new RelayCommand(
                        param => SearchTransactions(), param => SearchTransactionsCanExecute()
                        );
                }
                return _searchTransactionsCommand;
            }
        }

        private void SearchTransactions()
        {
            if (_selectedFilterType == "Accounts")
            {
                TransactionsToDisplay = AllTransactions.Where(n => n.AccountName.Contains(FilterString)).ToList().ToObservableCollection();
                
            } 
            else if (_selectedFilterType == "Categories")
            {
                TransactionsToDisplay = AllTransactions.Where(n => n.CategoryName.Contains(FilterString)).ToList().ToObservableCollection();
            }
            else if (_selectedFilterType == "Comments")
            {
                try
                {
                    TransactionsToDisplay = AllTransactions.Where(n => n.Comments.Contains(FilterString)).ToList().ToObservableCollection();
                }
                catch (NullReferenceException)
                {
                    TransactionsToDisplay = null;
                }
            }
        }

        private bool SearchTransactionsCanExecute()
        {
            if (!string.IsNullOrEmpty(_filterString))
            {
                return true;
            }

            return false;
        }

        public ICommand ShowAllTransactionsCommand
        {
            get
            {
                if (_showAllTransactionsCommand == null)
                {
                    _showAllTransactionsCommand = new RelayCommand(
                        param => ShowAllTransactions()
                        );
                }
                return _showAllTransactionsCommand;
            }
        }

        private void ShowAllTransactions()
        {
            TransactionsToDisplay = AllTransactions;
        }


    }
}
