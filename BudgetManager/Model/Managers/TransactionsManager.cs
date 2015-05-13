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
        private Database _db;
        public List<Transaction> Transactions { get; private set; }
        public List<TransactionType> TransactionTypes { get; private set; }
        public List<Category> TransactionCategories { get; private set; }

        public List<Curency> Currencies { get; private set; }

        public TransactionsManager()
        {
            _db = new Database();
            Transactions = _db.Transactions.ToList();
            TransactionTypes = _db.TransactionTypes.ToList();
            TransactionCategories = _db.Categories.ToList();
            Currencies = _db.Currencies.ToList();
        }

        public void AddTransaction(DateTime date, int accountId, decimal amount, int categoryId, String comments, int transactionTypeId)
        {
                Account account = _db.Accounts.Single(n => n.Id == accountId);
                Category category = _db.Categories.Single(n => n.ID == categoryId);
                TransactionType transType = _db.TransactionTypes.Single(n => n.ID == transactionTypeId);
                Transaction trans = new Transaction(date, account, amount, category, comments, transType);
                
                _db.Transactions.InsertOnSubmit(trans);
                _db.SubmitChanges();
                
                Transactions.Add(trans);
        }

        public void UpdateTransactions()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, _db.Transactions);
            Transactions = _db.Transactions.ToList();
        }

        public void UpdateTransactionFields(int? id, DateTime date, int accountId, decimal amount, int categoryId, String comments, int transactionTypeId)
        {
            Transaction entity = _db.Transactions.Single(n => n.ID == id);
            entity.Date = date;
            entity.Account = _db.Accounts.Single(n => n.Id == accountId);
            entity.Amount = amount;
            entity.Category = _db.Categories.Single(n => n.ID == categoryId);
            entity.TransactionType = _db.TransactionTypes.Single(n => n.ID == transactionTypeId);
            entity.Comments = comments;
            _db.SubmitChanges();

            Transactions.Remove(Transactions.Single(n => n.ID == id));
            Transactions.Add(entity);
        }

        public void DeleteTransaction(int? id)
        {
            Transaction entity = _db.Transactions.Single(n=>n.ID == id);
            _db.Transactions.DeleteOnSubmit(entity);
            _db.SubmitChanges();
        }

    }
}
