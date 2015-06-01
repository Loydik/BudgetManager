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
        private Boolean _isPieChartVisible;
        private Boolean _isBarChartVisible;
        private ObservableCollection<String> _transactionTypes; 
        private String _selectedType;
        private ObservableCollection<String> _chartTypes;
        private String _selectedChartType;

        private String _mediatorAccountsMessage;
        private String _mediatorTransactionsMessage;
        private String _mediatorCategoriesMessage;

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

        public String MediatorAccountsMessage
        {
            get { return _mediatorAccountsMessage; }
            set
            {
                if (value == "AccountDeleted" || value == "AccountAdded")
                {
                    _mediatorAccountsMessage = value;
                    _accountsManager.UpdateAccounts();
                    _visualizationManager.UpdateTransactions();
                    Init();
                    InitializeCharts();
                    OnPropertyChanged("MediatorAccountsMessage");
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
                    _accountsManager.UpdateAccounts();
                    _visualizationManager.UpdateTransactions();
                    Init();
                    InitializeCharts();
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
                    _visualizationManager.UpdateTransactions();
                    Init();
                    InitializeCharts();
                    OnPropertyChanged("MediatorTransactionsMessage");
                }
            }
        }

        public string TotalBalanceToDisplay
        {
            get { return _totalBalance + " " + _currency.Symbol; }

        }

        public ObservableCollection<String> TransactionTypes
        {
            get { return _transactionTypes; }
            set
            {
                _transactionTypes = value;
                OnPropertyChanged("TransactionTypes");
            }
        }

        public String SelectedType
        {
            get { return _selectedType; }

            set
            {
                _selectedType = value;
                RaisePropertyChanged("ChartTitle");
                InitializeCharts();
                OnPropertyChanged("SelectedType");
            }
        }

        public ObservableCollection<String> ChartTypes
        {
            get { return _chartTypes; }
            set
            {
                _chartTypes = value;
                OnPropertyChanged("ChartTypes");
            }
        }

        public String SelectedChartType
        {
            get { return _selectedChartType; }

            set
            {
                _selectedChartType = value;

                if (value == "Pie Chart")
                {
                    IsPieChartVisible = true;
                    IsBarChartVisible = false;
                }

                else if (value == "Bar Chart")
                {
                    IsBarChartVisible = true;
                    IsPieChartVisible = false;
                }
                //InitializeCharts();// bad for performance but visually cool!!!
                OnPropertyChanged("SelectedChartType");
            }
        }

        public string ChartTitle
        {
            get
            {
                if (SelectedType == "Spendings")
                {
                    return "Total Spendings";
                }

                    return "Total Income";
                
            }
        }

        public string ChartSubtitle
        {
            get { return "Transactions by Categories from " + StartDate.ToString("d-MM-yyyy") + " to " + EndDate.ToString("d-MM-yyyy"); }
        }


        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged("ChartSubtitle");
                InitializeCharts();
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged("ChartSubtitle");
                InitializeCharts();
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

        public bool IsPieChartVisible
        {
            get { return _isPieChartVisible; }
            set
            {
                _isPieChartVisible = value;
                OnPropertyChanged("IsPieChartVisible");
            }
        }

        public bool IsBarChartVisible
        {
            get { return _isBarChartVisible; }
            set
            {
                _isBarChartVisible = value;
                OnPropertyChanged("IsBarChartVisible");
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

            //register to the mediator for the 
            //AccountsChanged message
            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorAccountsMessage = (String)o; }, ViewModelMessages.AccountsChanged);

            //register to the mediator for the 
            //TransactionsChanged message
            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorTransactionsMessage = (String)o; }, ViewModelMessages.TransactionsChanged);

            Mediator.Instance.Register(

                //Callback delegate, when message is seen
                (Object o) => { MediatorCategoriesMessage = (String)o; }, ViewModelMessages.CategoriesChanged);
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
            
            TransactionTypes = new ObservableCollection<string>(){"Spendings", "Income"};
            SelectedType = TransactionTypes.FirstOrDefault();
            
            ChartTypes = new ObservableCollection<string>() { "Pie Chart", "Bar Chart" };
            SelectedChartType = ChartTypes.FirstOrDefault();

        }

        private void InitializeCharts()
        {
            if (SelectedType == "Spendings")
            {
                SpendingsVisualCategories =
                    _visualizationManager.GetVisualCategoriesForSpendings(new TimePeriod(StartDate, EndDate))
                        .ToObservableCollection();
            }
            else if (SelectedType == "Income")
            {
                SpendingsVisualCategories =
                    _visualizationManager.GetVisualCategoriesForIncome(new TimePeriod(StartDate, EndDate))
                        .ToObservableCollection();
            }
        }

        #endregion //Construction
    }
}
