using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;

namespace BudgetManager.Model.Managers
{
    public class TransactionsManager
    {
        private Database db;
        public List<Transaction> Transactions { get; private set; }
        public List<TransactionType> TransactionTypes { get; private set; }
        public List<Category> TransactionCategories { get; private set; }

        public List<Curency> Currencies { get; private set; }

        public TransactionsManager()
        {
            db = new Database();
            Transactions = db.Transactions.ToList();
            TransactionTypes = db.TransactionTypes.ToList();
            TransactionCategories = db.Categories.ToList();
            Currencies = db.Currencies.ToList();
        }

        public void addTransaction(DateTime date, int accountID, decimal amount, int currencyID, int categoryID, String comments, int transactionTypeID)
        {
                Account account = db.Accounts.Single(n => n.ID == accountID);
                Curency currency = db.Currencies.Single(n => n.ID == currencyID);
                Category category = db.Categories.Single(n => n.ID == categoryID);
                TransactionType transType = db.TransactionTypes.Single(n => n.ID == transactionTypeID);

                Transaction trans = new Transaction(date, account, amount, currency, category, comments, transType);
                trans.ID = null;
            
                db.Transactions.InsertOnSubmit(trans);
                db.SubmitChanges();
                Transactions.Add(trans);
        }

    }
}
