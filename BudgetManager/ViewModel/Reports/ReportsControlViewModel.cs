using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BudgetManager.ViewModel.Reports
{
    public class ReportsControlViewModel : ObservableObject, IPageViewModel
    {

        private ReportGenerator _reportGenerator;
        private ICommand _generateReportCommand;
        private AccountsManager _accManager;
        private ObservableCollection<AccountViewModel> _accounts;
        private Account _selectedAccount;

        public string Name
        {
            get { return "Reports"; }
        }

        public ReportsControlViewModel()
        {
            _reportGenerator = new PdfReportGenerator();
            _accManager = new AccountsManager();
            Init();
        }

        private void Init()
        {
            Accounts = ConversionHelper.toObservableCollection(_accManager.Accounts.ToList(), l => new AccountViewModel(l));//getting data from manager and converting into Observable list
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

        public Account SelectedAccount
        {

            get { return _selectedAccount; }
            set { _selectedAccount = value; OnPropertyChanged("SelectedAccount"); }
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
            _reportGenerator.Generate(new TimePeriod(new DateTime(),new DateTime()), new List<Account>());
        }
    }
}
