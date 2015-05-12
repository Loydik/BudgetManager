using System;
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

namespace BudgetManager.ViewModel.Accounts
{
    public class AccountsControlViewModel : ObservableObject, IPageViewModel
    {
        private AccountsManager _accManager;
        private IWindowFactory _windowFactory;
        private ObservableCollection<AccountViewModel> _allAccounts;

        private ICommand _openCreateNewAccountWindowCommand;
        private ICommand _refreshCommand;

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

        #region Construction
        public AccountsControlViewModel()
        {
            _accManager = new AccountsManager();
            Init();
            _windowFactory = new ProductionWindowFactory();
        }

        private void Init()
        {
            AllAccounts = ConversionHelper.toObservableCollection(_accManager.Accounts.ToList(), l => new AccountViewModel(l));//getting data from manager and converting into Observable list
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

        #endregion

    }
}
