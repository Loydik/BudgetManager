﻿using System;
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
            { return _accountObj.Id; }
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

        public String AccountBalanceToDisplay
        {
            get
            {
                
               return AccountBalance.ToString("F") + " " + CurrencySymbol;
                
            }
        }

        public String AccountTypeName
        {
            get
            { return _accountObj.AccountType.Name; }
        }

        public String CurrencySymbol
        {
            get
            { return _accountObj.Curency.Symbol; }
        }

        public bool IsNegativeBalance
        {
            get
            {
                if (AccountBalance < 0)
                {
                    return true;
                }

                return false;
            }
        }

        public AccountViewModel(Account obj)
        {
            this._accountObj = obj;
        }

        

    }
}
