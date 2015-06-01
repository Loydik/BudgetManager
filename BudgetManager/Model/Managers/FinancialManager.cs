using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;

namespace BudgetManager.Model.Managers
{
    public class FinancialManager
    {
        private readonly Database _db;
        public List<Account> Accounts { get; private set; }

        public List<Transaction> Transactions { get; private set; }

        public FinancialManager()
        {
            _db = new Database();
            Accounts = _db.Accounts.ToList();
            Transactions = _db.Transactions.ToList();
        }

        public void UpdateTransactions()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, _db.Transactions);
            Transactions = _db.Transactions.ToList();
        }

        public void UpdateAccountBalance(int? id, decimal value, TransactionType type, int? transactionId)
        {
            UpdateTransactions();

            var account = Accounts.Single(n => n.Id == id);

            if (type.Name.Equals("Withdrawal"))
            {
                account.Balance = account.Balance - value;
            }
            else if (type.Name.Equals("Deposit"))
            {
                account.Balance = account.Balance + value;
            }

            var transaction = Transactions.OrderBy(n=>n.Date).ThenBy(n=>n.ID).Last();
            transaction.AccountBalanceAfter = account.Balance;

            _db.SubmitChanges();
        }

        public void RefreshAccountBalance(int? id)
        {
            UpdateTransactions();

            var totalDeposit = Transactions.Where(n => n.TransactionType.Name.Equals("Deposit")).Select(n => n.Amount).ToList().Sum();
            var totalWithdrawal = Transactions.Where(n => n.TransactionType.Name.Equals("Withdrawal")).Select(n => n.Amount).ToList().Sum();

            var difference = totalDeposit - totalWithdrawal;

            var account = Accounts.Single(n => n.Id == id);
            account.Balance = account.InitialBalance + difference;

            _db.SubmitChanges();
        }

        public void UpdateTransactionsAfterBalancesinAccount(int? accountId, DateTime afterDate, int? transactionId)
        {
            UpdateTransactions();

            //getting transactions after changed one
            var transactions = Transactions.Where(n => n.Account.Id == accountId && n.Date >= afterDate).ToList();
            transactions = transactions.OrderBy(n => n.Date).ThenBy(n => n.ID).ToList();
            decimal? initialBalance = null;
            Transaction previousTransaction = null;
            Transaction firstTransaction = null;
            
            //We check whether account has any transactions at all
            if (transactions.Count() != 0)
            {
                firstTransaction = transactions.First();

                try
                {
                    var previousTransactions =
                        Transactions.Where(n => n.Date <= afterDate && n.ID != transactionId).OrderBy(n => n.ID);
                    previousTransaction = previousTransactions.Last();

                    int i = 2;
                    int count = previousTransactions.Count();

                    while (true)
                    {
                        if (previousTransaction.Date == afterDate && previousTransaction.ID > transactionId)
                        {
                            previousTransaction = previousTransactions.ElementAt(count - i);
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

#pragma warning disable 0168// we do not want "declared but never used warning!"

                catch (InvalidOperationException ex)
                {
                    //We simply go ahead 
                }


#pragma warning restore 0168

            if (previousTransaction != null)
            {
                initialBalance = previousTransaction.AccountBalanceAfter;
            }
            else if(firstTransaction != null)
            {
                initialBalance = firstTransaction.Account.InitialBalance;
            }

            decimal? balanceAfter = initialBalance;

            if (initialBalance != null)
            {
                foreach (var transaction in transactions)
                {
                    if (transaction.TransactionType.Name.Equals("Withdrawal"))
                    {
                        balanceAfter = balanceAfter - transaction.Amount;
                        transaction.AccountBalanceAfter = balanceAfter;
                    }
                    else if (transaction.TransactionType.Name.Equals("Deposit"))
                    {
                        balanceAfter = balanceAfter + transaction.Amount;
                        transaction.AccountBalanceAfter = balanceAfter;
                    }
                }
            }

            _db.SubmitChanges();
        }
        }
    }
}
