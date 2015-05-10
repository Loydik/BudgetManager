using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Collections.ObjectModel;
using BudgetManager.Model.Managers;
using BudgetManager.Model.Db;

namespace BudgetManager.ViewModel.Accounts
{
    public class AccountsControlViewModel : ObservableObject, IPageViewModel
    {
        private AccountsManager _accManager;
        private IWindowFactory _windowFactory;
        private ObservableCollection<AccountViewModel> _allAccounts;

        public String Name
        {
            get { return "Accounts"; }
        }

        public ObservableCollection<AccountViewModel> AllAccounts
        {
            get
            { return _allAccounts; }
            set
            {
                _allAccounts = value;
                OnPropertyChanged("AllAccounts");
            }
        }

        public AccountsControlViewModel()
        {
            _accManager = new AccountsManager();
            this.init();
            _windowFactory = new ProductionWindowFactory();
        }

        private void init()
        {
            AllAccounts = Util.ConversionHelper.toObservableCollection<AccountViewModel, Account>(_accManager.Accounts.ToList(), l => new AccountViewModel(l));//getting data from manager and converting into Observable list
        }

    }
}
