﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Collections.ObjectModel;
using BudgetManager.Model.Managers;
using BudgetManager.Model.Db;
using System.Windows.Input;
using BudgetManager.ViewModel.Accounts;
using BudgetManager.View.AccountsTab;
using BudgetManager.ViewModel.Transactions;

namespace BudgetManager.ViewModel.Accounts
{
    public class AccountsControlViewModel : ObservableObject, IPageViewModel
    {
        private AccountsManager _accManager;
        private IWindowFactory _windowFactory;
        private ObservableCollection<AccountViewModel> _allAccounts;
        private string _deleteAccountConfirmation;
        private string _mediatorAccountMessage;
        private string _mediatorTransactionsMessage;

        private ICommand _openCreateNewAccountWindowCommand;
        private ICommand _refreshCommand;
        private ICommand _deleteAccountCommand;

        #region Properties

        public String Name
        {
            get { return "Accounts"; }
        }

        public ObservableCollection<AccountViewModel> AllAccounts
        {
            get { return _allAccounts; }
            set
            {
                _allAccounts = value;
                OnPropertyChanged("AllAccounts");
            }
        }

        public String MediatorAccountMessage
        {
            get { return _mediatorAccountMessage; }
            set {
                if (value == "AccountAdded")
                {
                    _mediatorAccountMessage = value;
                    Refresh();
                    RaisePropertyChanged("MediatorAccountMessage");
                }
            }
        }

        public String MediatorTransactionsMessage
        {
            get { return _mediatorTransactionsMessage; }
            set
            {
                if (value == "TransactionAdded" || value == "TransactionDeleted" || value == "TransactionEdited")
                {
                    _mediatorTransactionsMessage = value;
                    Refresh();
                    RaisePropertyChanged("MediatorAccountMessage");
                }
            }
        }

        public String DeleteAccountConfirmation
        {
            get { return _deleteAccountConfirmation; }
            set { _deleteAccountConfirmation = value; RaisePropertyChanged("DeleteAccountConfirmation"); }
        }

        #endregion


        #region Construction
        public AccountsControlViewModel()
        {
            _accManager = new AccountsManager();
            Init();
            _windowFactory = new ProductionWindowFactory();

            //register to the mediator for the 
            //DeleteAccount message
            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { DeleteAccountConfirmation = (String) o; }, ViewModelMessages.UserDeleteAccount);

            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorAccountMessage = (String) o; }, ViewModelMessages.AccountsChanged);

            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorTransactionsMessage = (String)o; }, ViewModelMessages.TransactionsChanged);
        }

        private void Init()
        {
            AllAccounts = ConversionHelper.ToObservableCollection(_accManager.Accounts.ToList(), l => new AccountViewModel(l));//getting data from manager and converting into Observable list
        }

        #endregion //Construction

        #region ICommands

        public ICommand OpenCreateNewAccountWindowCommand
        {
            get
            {
                if (_openCreateNewAccountWindowCommand == null)
                {
                    _openCreateNewAccountWindowCommand = new RelayCommand(
                        param => _windowFactory.CreateNewWindow(new CreateNewAccountWindowViewModel(), new CreateNewAccountWindow())
                    );
                }
                return _openCreateNewAccountWindowCommand;
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
            _accManager.UpdateAccounts();
            Init();
        }


        public ICommand DeleteAccountCommand
        {
            get
            {
                if (_deleteAccountCommand == null)
                {
                    _deleteAccountCommand = new RelayCommand(
                        param => DeleteAccount((AccountViewModel)param)
                    );
                }
                return _deleteAccountCommand;
            }
        }

        private void DeleteAccount(AccountViewModel acc)
        {
            if (acc != null)
            {

                _windowFactory.CreateNewWindow(new DeleteAccountDialogWindowViewModel(acc.AccountName),
                    new DeleteAccountDialogWindow());

                if (DeleteAccountConfirmation == "DELETE")
                {
                    _accManager.DeleteAccount(acc.AccountID);
                    _deleteAccountConfirmation = "";
                    Mediator.Instance.NotifyListeners(ViewModelMessages.AccountsChanged, "AccountDeleted");
                    Refresh();
                }
            }
        }

        #endregion

    }
}
