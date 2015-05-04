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

        public void addTransaction(DateTime date, Account acc, decimal amount, Curency currency, Category category, String comments, TransactionType type)
        {
                Account newAccount = db.Accounts.Single(n => n.ID == acc.ID);
                Curency newCurrency = db.Currencies.Single(n => n.ID == currency.ID);
                Category newCategory = db.Categories.Single(n => n.ID == category.ID);
                TransactionType newTransType = db.TransactionTypes.Single(n => n.ID == type.ID);

                Transaction trans = new Transaction(date, newAccount, amount, newCurrency, newCategory, comments, newTransType);
                trans.ID = null;
            
                db.Transactions.InsertOnSubmit(trans);
                db.SubmitChanges();
                Transactions.Add(trans);
        }

    }
}
