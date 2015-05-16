using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;

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

    }
}
