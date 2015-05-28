using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;
using BudgetManager.Model.ReportGenerators;

namespace BudgetManager.Model.Managers
{
    public class ReportsManager
    {
        private readonly Database _db;
        public List<Account> Accounts { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public ReportsManager()
        {
            _db = new Database();
            Accounts = _db.Accounts.ToList();
            UpdateTransactions();
        }

        public void UpdateTransactions()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, _db.Transactions);
            Transactions = _db.Transactions.ToList();
        }

        public decimal GetTotalSpendingsOfAccount(int? accountId)
        {

            if (accountId != null)
            {
                decimal total = 0;
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.TransactionType.Name == "Withdrawal").ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                return total;
            }

            return 0;
        }

        public decimal GetTotalSpendingsOfAccount(int? accountId, TimePeriod period)
        {

            if (accountId != null)
            {
                decimal total = 0;
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.TransactionType.Name == "Withdrawal" && n.Date>=period.StartDate && n.Date<=period.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                return total;
            }

            return 0;
        }

        public decimal GetTotalSpendingsOfAccount(int? accountId, TimePeriod period, Category category)
        {

            if (accountId != null)
            {
                decimal total = 0;
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.TransactionType.Name == "Withdrawal" && n.Category == category && n.Date >= period.StartDate && n.Date <= period.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                return total;
            }

            return 0;
        }

        public decimal GetTotalIncomeOfAccount(int? accountId)
        {

            if (accountId != null)
            {
                decimal total = 0;
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.TransactionType.Name == "Deposit").ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                return total;
            }

            return 0;
        }

        public decimal GetTotalIncomeOfAccount(int? accountId, TimePeriod period)
        {

            if (accountId != null)
            {
                decimal total = 0;
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.TransactionType.Name == "Deposit" && n.Date >= period.StartDate && n.Date <= period.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                return total;
            }

            return 0;
        }

        public decimal GetTotalIncomeOfAccount(int? accountId, TimePeriod period, Category category)
        {
            
            if (accountId != null)
            {
                decimal total = 0;
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.TransactionType.Name == "Deposit" && n.Category == category && n.Date >= period.StartDate && n.Date <= period.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    total = total + transaction.Amount;
                }

                return total;
            }

            return 0;
        }

        public decimal GetInitialAccountBalanceAtDate(int accountId, DateTime date)
        {
            decimal balance = 0;

            var transaction =
                    Transactions.Where(n => n.Account.Id == accountId && n.Date <= date).OrderBy(n=>n.ID).FirstOrDefault();

            if (transaction != null)
            {
                if (transaction.TransactionType.Name == "Deposit")
                {
                    balance = (decimal) transaction.AccountBalanceAfter - transaction.Amount;
                }

                else if (transaction.TransactionType.Name == "Withdrawal")
                {
                    balance = (decimal) transaction.AccountBalanceAfter + transaction.Amount;
                }
            }
            else
            {
                balance = Accounts.Single(n => n.Id == accountId).InitialBalance;
            }

            return balance;
        }

        public decimal GetFinalAccountBalanceAtDate(int accountId, DateTime date)
        {
            decimal balance = 0;

            var transaction =
                    Transactions.Where(n => n.Account.Id == accountId && n.Date <= date).OrderBy(n => n.ID).LastOrDefault();

            if (transaction != null)
            {
                if (transaction.TransactionType.Name == "Deposit")
                {
                    balance = (decimal)transaction.AccountBalanceAfter;
                }

                else if (transaction.TransactionType.Name == "Withdrawal")
                {
                    balance = (decimal)transaction.AccountBalanceAfter;
                }
            }
            else
            {
                balance = Accounts.Single(n => n.Id == accountId).InitialBalance;
            }

            return balance;
        }

        public List<Category> GetUsedCategories(TimePeriod period)
        {
            List<Category> categories = new List<Category>();

                var transactions =
                    Transactions.Where(n => n.Date >= period.StartDate && n.Date <= period.EndDate).ToList();

            if (transactions.Count() != 0)
            {
                foreach (var transaction in transactions)
                {
                    categories.Add(transaction.Category);
                }

                categories = categories.Distinct().ToList();
            }


            return categories;
        }

        public List<Category> GetUsedCategories(int? accountId, TimePeriod period)
        {
            List<Category> categories = new List<Category>();

            if (accountId != null)
            {
                var transactions =
                    Transactions.Where(n => n.Account.Id == accountId && n.Date >= period.StartDate && n.Date <= period.EndDate).ToList();

                foreach (var transaction in transactions)
                {
                    categories.Add(transaction.Category);
                }

                categories = categories.Distinct().ToList();

            }

            return categories;
        }

    }
}
