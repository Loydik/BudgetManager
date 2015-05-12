using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;
using System.Reflection;

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

        public void AddTransaction(DateTime date, int accountID, decimal amount, int currencyID, int categoryID, String comments, int transactionTypeID)
        {
                Account account = db.Accounts.Single(n => n.ID == accountID);
                Curency currency = db.Currencies.Single(n => n.ID == currencyID);
                Category category = db.Categories.Single(n => n.ID == categoryID);
                TransactionType transType = db.TransactionTypes.Single(n => n.ID == transactionTypeID);

                Transaction trans = new Transaction(date, account, amount, category, comments, transType);
            
                db.Transactions.InsertOnSubmit(trans);
                db.SubmitChanges();

                Transactions.Add(trans);
        }

        public void UpdateTransactions()
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.Transactions);
            Transactions = db.Transactions.ToList();
        }

        public void UpdateTransactionFields(int? id, DateTime date, int accountID, decimal amount, int categoryID, String comments, int transactionTypeID)
        {
            Transaction entity = db.Transactions.Single(n => n.ID == id);
            entity.Date = date;
            entity.Account = db.Accounts.Single(n => n.ID == accountID);
            entity.Amount = amount;
            entity.Category = db.Categories.Single(n => n.ID == categoryID);
            entity.TransactionType = db.TransactionTypes.Single(n => n.ID == transactionTypeID);
            entity.Comments = comments;
            db.SubmitChanges();

            Transactions.Remove(Transactions.Single(n => n.ID == id));
            Transactions.Add(entity);
        }

        public void DeleteTransaction(int? id)
        {
            Transaction entity = db.Transactions.Single(n=>n.ID == id);
            db.Transactions.DeleteOnSubmit(entity);
            db.SubmitChanges();
        }

    }
}
