using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Managers;
using BudgetManager.ViewModel.Accounts;
using BudgetManager.ViewModel.Util;

namespace BudgetManager.ViewModel.Overview
{
    public class OverviewControlViewModel : ObservableObject, IPageViewModel
    {
        private AccountsManager _accountsManager;
        private ObservableCollection<AccountViewModel> _allAccounts;
        private ObservableCollection<AccountViewModel> _checkingAccounts;
        private ObservableCollection<AccountViewModel> _savingsAccounts;
        private decimal _totalBalance;

        public string Name
        {
            get { return "Overview"; }
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

        public ObservableCollection<AccountViewModel> CheckingAccounts
        {
            get { return _checkingAccounts; }
            set
            {
                _checkingAccounts = value;
                OnPropertyChanged("CheckingAccounts");
            }
        }

        public ObservableCollection<AccountViewModel> SavingsAccounts
        {
            get { return _savingsAccounts; }
            set
            {
                _savingsAccounts = value;
                OnPropertyChanged("SavingsAccounts");
            }
        }

        public decimal TotalBalance
        {
            get { return _totalBalance; }
            set
            {
                _totalBalance = value;
                OnPropertyChanged("TotalBalance");
            }
        }
        


        #region Construction
        public OverviewControlViewModel()
        {
            _accountsManager = new AccountsManager();
            Init();
        }

        private void Init()
        {
            AllAccounts = ConversionHelper.ToObservableCollection(_accountsManager.Accounts.ToList(), l => new AccountViewModel(l));//getting data from manager and converting into Observable list
            CheckingAccounts =
                ConversionHelper.ToObservableCollection(
                    _accountsManager.Accounts.Where(n => n.AccountType.Name == "Checking").ToList(),
                    l => new AccountViewModel(l));
            SavingsAccounts =
                ConversionHelper.ToObservableCollection(
                    _accountsManager.Accounts.Where(n => n.AccountType.Name == "Savings").ToList(),
                    l => new AccountViewModel(l));
            TotalBalance = _accountsManager.GetTotalBalance();
        }

        #endregion //Construction
    }
}
