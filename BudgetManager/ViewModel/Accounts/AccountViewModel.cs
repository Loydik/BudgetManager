using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using BudgetManager.Model.Db;

namespace BudgetManager.ViewModel.Accounts
{
    public class AccountViewModel : ObservableObject
    {
        private Account _accountObj;

        public Account AccountObj
        {
            get
            { return _accountObj; }
            set
            {
                _accountObj = value;
                OnPropertyChanged("AccountObj");
            }
        }

        public int AccountID
        {
            get
            { return _accountObj.ID; }
        }

        public String AccountName
        {
            get
            { return _accountObj.Name; }
            set
            {
                _accountObj.Name = value;
                OnPropertyChanged("AccountName");
            }
        }

        public decimal AccountBalance
        {
            get
            { return _accountObj.Balance; }
        }

        public String AccountTypeName
        {
            get
            { return _accountObj.AccountType.Name; }
        }

        public AccountViewModel(Account obj)
        {
            this._accountObj = obj;
        }

       
    }
}
