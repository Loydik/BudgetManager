using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BudgetManager.Model.Db;
using BudgetManager.ViewModel.Util;
using BudgetManager.Model.Managers;
using System.Windows.Input;

namespace BudgetManager.ViewModel.Accounts
{
    public class CreateNewAccountWindowViewModel : ObservableObject
    {
        private AccountsManager _accManager;
        private const decimal MaxAmount = 999999999999999999m;
        private const decimal MinAmount = -999999999999999999m;

        private string _name;
        private decimal _balance;
        private AccountType _selectedAccountType;
        private Curency _selectedCurrency;
        private string _errorMessage;
        private ICommand _closeWindowCommand;
        private ICommand _createNewAccountCommand;

        public CreateNewAccountWindowViewModel()
        {
            _accManager = new AccountsManager();
            SelectedAccountType = AccountTypes.FirstOrDefault();
            SelectedCurrency = _accManager.GetApplicationCurrency();
        }

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public List<AccountType> AccountTypes
        {
            get { return _accManager.AccountTypes; }
        }

        public List<Curency> Currencies
        {
            get { return _accManager.Currencies; }
        }


        public AccountType SelectedAccountType
        {
            get { return _selectedAccountType; }
            set
            {
                _selectedAccountType = value;
                OnPropertyChanged("SelectedAccountType");
            }
        }

        public Curency SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged("SelectedCurrency");
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        #endregion


        #region ICommands

        public ICommand CreateNewAccountCommand
        {
            get
            {
                if (_createNewAccountCommand == null)
                {
                    _createNewAccountCommand = new RelayCommand(
                        param => CreateNewAccount((Window)param), param => CreateNewAccountCommandCanExecute()
                    );
                }
                return _createNewAccountCommand;
            }
        }

        public void CreateNewAccount(Window x)
        {


            _accManager.AddAccount(Name, Balance, SelectedAccountType, SelectedCurrency);
            CloseWindow(x);
        }

        public Boolean CreateNewAccountCommandCanExecute()
        {

            if (MinAmount <= _balance && _balance <= MaxAmount && !string.IsNullOrEmpty(Name))
            {
                return true;
            }
            else { return false; }
        }


        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                {
                    _closeWindowCommand = new RelayCommand(
                        param => CloseWindow((Window) param)
                        );
                }
                return _closeWindowCommand;
            }
        }

        public void CloseWindow(Window x)
        {
            if (x != null)
            {
                x.Close();
            }
        }

        #endregion


    }
}
