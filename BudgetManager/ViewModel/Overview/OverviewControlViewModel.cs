using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;
using BudgetManager.Model.ReportGenerators;
using BudgetManager.ViewModel.Accounts;
using BudgetManager.ViewModel.Util;
using BudgetManager.View.OverviewTab;

namespace BudgetManager.ViewModel.Overview
{
    public class OverviewControlViewModel : ObservableObject, IPageViewModel
    {
        private AccountsManager _accountsManager;
        private VisualizationManager _visualizationManager;
        private ObservableCollection<AccountViewModel> _allAccounts;
        private ObservableCollection<AccountViewModel> _checkingAccounts;
        private ObservableCollection<AccountViewModel> _savingsAccounts;
        private DateTime _startDate;
        private DateTime _endDate;
        private ObservableCollection<VisualizationManager.CategoryDisplayClass> _spendingsVisualCategories;
        private ObservableCollection<VisualizationManager.CategoryDisplayClass> _incomeVisualCategories;

        private decimal _totalBalance;
        private Curency _currency;

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

        public string TotalBalanceToDisplay
        {
            get { return _totalBalance + " " + _currency.Symbol; }
           
        }

        public string ChartTitle
        {
            get { return "Total Spendings"; }
        }

        public string ChartSubtitle
        {
            get { return "Transactions by Categories (Last 30 days)"; }
        }


        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public ObservableCollection<VisualizationManager.CategoryDisplayClass> SpendingsVisualCategories
        {
            get { return _spendingsVisualCategories; }
            set
            {
                _spendingsVisualCategories = value;
                OnPropertyChanged("SpendingsVisualCategories");
            }
        }

        public ObservableCollection<VisualizationManager.CategoryDisplayClass> IncomeVisualCategories
        {
            get { return _incomeVisualCategories; }
            set
            {
                _incomeVisualCategories = value;
                OnPropertyChanged("IncomeVisualCategories");
            }
        }

        #region Construction
        public OverviewControlViewModel()
        {
            _accountsManager = new AccountsManager();
            _visualizationManager = new VisualizationManager();
            _startDate = DateTime.Now.AddDays(-30);
            _endDate = DateTime.Now;
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
            _totalBalance = _accountsManager.GetTotalBalance();
            _currency = _accountsManager.GetApplicationCurrency();
            SpendingsVisualCategories = _visualizationManager.GetVisualCategoriesForSpendings(new TimePeriod(StartDate,EndDate)).ToObservableCollection();

        }

        #endregion //Construction
    }
}
