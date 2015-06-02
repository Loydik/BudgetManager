using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using BudgetManager.Model.Db;

namespace BudgetManager.ViewModel.Transactions
{
    public class TransactionViewModel : ObservableObject
    {
        private Transaction _transactionObj;

        public Transaction TransactionObj
        {
            get { return _transactionObj; }
            set
            {
                _transactionObj = value;
                OnPropertyChanged("TransactionObj");
            }
        }

        public int? TransactionID
        {
            get { return _transactionObj.ID; }
            set
            {
                _transactionObj.ID = value;
                OnPropertyChanged("TransactionID");
            }
        }

        public DateTime Date
        {
            get { return _transactionObj.Date; }
            set
            {
                _transactionObj.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public String DateToDisplay
        {
            get { return _transactionObj.Date.ToString("dd/MM/yyyy"); }
        }

        public String AccountName
        {
            get { return _transactionObj.Account.Name; }
        }

        public int? AccountId
        {
            get { return _transactionObj.Account.Id; }
        }

        public decimal Amount
        {
            get { return _transactionObj.Amount; }
            set
            {
                _transactionObj.Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public String AmountToDisplay
        {
            get
            {
                //if (this.TransactionType == "Withdrawal")
                //{
                    //return "-" + Amount.ToString("F") + " " + TransactionObj.Account.Curency.Symbol;
                //}
                //else
                //{
                    return Amount.ToString("F") + " " + TransactionObj.Account.Curency.Symbol;
                //}
            }
        }

        public decimal? AccountBalanceAfter
        {
            get { return _transactionObj.AccountBalanceAfter; }
        }


        public String CategoryName
        {
            get { return _transactionObj.Category.Name; }
        }

        public String Comments
        {
            get { return _transactionObj.Comments; }
            set
            {
                _transactionObj.Comments = value;
                OnPropertyChanged("Comments");
            }
        }

        public String TransactionType
        {
            get { return _transactionObj.TransactionType.Name; }
        }

        public Boolean IsWithdrawal
        {
            get
            {
                if (TransactionType == "Withdrawal")
                {
                    return true;
                }

                return false;
            }
        }

        public TransactionViewModel()
        { 
        
        }

        public TransactionViewModel(Transaction obj)
        {
            _transactionObj = obj;
        }

    }
}
