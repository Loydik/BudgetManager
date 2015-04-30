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
            get
            { return _transactionObj; }
            set
            {
                _transactionObj = value;
                OnPropertyChanged("TransactionObj");
            }
        }

        public int TransactionID
        {
            get
            { return _transactionObj.ID; }
            set
            {
                _transactionObj.ID = value;
                OnPropertyChanged("TransactionID");
            }
        }

        public DateTime Date
        {
            get
            { return _transactionObj.Date; }
            set
            {
                _transactionObj.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public String AccountName
        {
            get
            { return _transactionObj.Account.Name; }
        }

        public decimal Amount
        {
            get
            { return _transactionObj.Amount; }
            set
            {
                _transactionObj.Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public String CurrencySymbol
        {
            get
            { return _transactionObj.Curency.Symbol; }
        }

        public String CategoryName
        {
            get
            { return _transactionObj.Category.Name; }
        }

        public String Comments
        {
            get
            { return _transactionObj.Comments; }
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


        public TransactionViewModel(Transaction obj)
        {
            _transactionObj = obj;
        }

    }
}
