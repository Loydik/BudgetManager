using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.ReportGenerators;
using BudgetManager.ViewModel.Util;
using System.Windows.Input;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;
using BudgetManager.View.TransactionsTab;
using BudgetManager.ViewModel.Accounts;
using BudgetManager.ViewModel.Transactions;
using Microsoft.Win32;

namespace BudgetManager.ViewModel.Reports
{
    public class ReportsControlViewModel : ObservableObject, IPageViewModel
    {

        private ReportGenerator _reportGenerator;
        private ICommand _generateReportCommand;
        private AccountsManager _accManager;
        private ObservableCollection<AccountViewModel> _accounts;
        private AccountViewModel _selectedAccount;
        private DateTime _startDate;
        private DateTime _endDate;
        private Dictionary<int, string> _reportFormats;
        private int _selectedFormatKey;
        private bool _allAccountsChecked;
        private bool _comboboxIsEnabled;
        private IWindowFactory _windowFactory;

        public string Name
        {
            get { return "Reports"; }
        }

        public ReportsControlViewModel()
        {
            _reportGenerator = new PdfReportGenerator();
            _accManager = new AccountsManager();
            _windowFactory = new ProductionWindowFactory();
            Init();
            _reportFormats = new Dictionary<int, string>();
            _reportFormats.Add(1,"PDF");
            _startDate = DateTime.Now.AddDays(-1);
            _endDate = DateTime.Now;
            _comboboxIsEnabled = true;
            SelectedFormatKey = 1;
            SelectedAccount = Accounts.FirstOrDefault();
        }

        private void Init()
        {
            Accounts = ConversionHelper.ToObservableCollection(_accManager.Accounts.ToList(), l => new AccountViewModel(l));//getting data from manager and converting into Observable list
        }

        public ObservableCollection<AccountViewModel> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        public AccountViewModel SelectedAccount
        {

            get { return _selectedAccount; }
            set { _selectedAccount = value; OnPropertyChanged("SelectedAccount"); }
        }

        public bool AllAccountsChecked
        {
            get { return _allAccountsChecked; }
            set { 
                _allAccountsChecked = value;

                if (_allAccountsChecked)
                {
                    ComboboxIsEnabled = false;
                    SelectedAccount = null;
                }
                else if (_allAccountsChecked == false)
                {
                    ComboboxIsEnabled = true;
                    SelectedAccount = Accounts.FirstOrDefault();
                }

                OnPropertyChanged("AllAccountsChecked");
            }
        }

        public bool ComboboxIsEnabled
        {
            get { return _comboboxIsEnabled; }
            set
            {
                _comboboxIsEnabled = value; OnPropertyChanged("ComboboxIsEnabled");
            }
        }

        public Dictionary<int, string> ReportFormats
        {
            get { return _reportFormats; }
            set
            {
                _reportFormats = value;
                OnPropertyChanged("ReportFormats");
            }
        }

        public int SelectedFormatKey
        {
            get { return _selectedFormatKey; }
            set
            {
                _selectedFormatKey = value;
                OnPropertyChanged("SelectedFormatKey");
            }
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

        public ICommand GenerateReportCommand
        {
            get
            {
                if (_generateReportCommand == null)
                {
                    _generateReportCommand = new RelayCommand(
                        param => GenerateReport()
                    );
                }
                return _generateReportCommand;
            }
        }

        private void GenerateReport()
        {
            TimePeriod period = new TimePeriod(StartDate, EndDate);
            List<Account> accountsList = new List<Account>();
            
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.FileName = "Money Manager Report";
            dlg.Filter = "PDF document |*.pdf";

            if (dlg.ShowDialog() == true && dlg.CheckPathExists)
            {

                string fileName = dlg.FileName;

                if (AllAccountsChecked)
                {
                    foreach (var acc in Accounts)
                    {
                        accountsList.Add(acc.AccountObj);
                    }

                    _reportGenerator.Generate(period, accountsList, fileName);
                }

                else
                {
                    accountsList.Add(SelectedAccount.AccountObj);
                    _reportGenerator.Generate(period, accountsList, fileName);
                }
            }

        }
    }
}
