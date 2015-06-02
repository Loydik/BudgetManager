using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;
using System.Configuration;


namespace BudgetManager.Model.Managers
{
    public class AccountsManager
    {
        private readonly Database _db;
        public List<Account> Accounts { get; private set; }
        public List<AccountType> AccountTypes { get; private set; }
        public List<Curency> Currencies { get; private set; }

        public List<Transaction> Transactions { get; private set; }

        public AccountsManager()
        {
            _db = new Database();
            UpdateAccounts();
            AccountTypes = _db.AccountTypes.ToList();
            Currencies = _db.Currencies.ToList();
            Transactions = _db.Transactions.ToList();
        }

        public void AddAccount(String name, decimal balance, AccountType type, Curency curr)
        {
            var accType = _db.AccountTypes.Single(n => n.ID == type.ID);
            var currency = _db.Currencies.Single(n => n.ID == curr.ID);

            Account entity = new Account(name, balance, accType, currency);

            Accounts.Add(entity);
            _db.Accounts.InsertOnSubmit(entity);
            _db.SubmitChanges();
        }

        public void DeleteAccount(int? id)
        {
            UpdateAccounts();
            Account entity = _db.Accounts.Single(n => n.Id == id);
            _db.Accounts.DeleteOnSubmit(entity);
            _db.SubmitChanges();
        }

        public void UpdateAccounts()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, _db.Accounts);
            Accounts = _db.Accounts.ToList();
        }

        public decimal GetTotalBalance()
        {
            decimal balance = 0;

            if (Accounts.Any())
            {
                foreach (var account in Accounts)
                {
                    balance = balance + account.Balance;
                }

                return balance;
            }

            return balance;
        }

        public Curency GetApplicationCurrency()
        {
            String currency = ConfigurationManager.AppSettings["currency"];

            if (currency != null)
            {
                Curency cur = Currencies.Single(n => n.Name == currency);
                return cur;
            }
            
            
            return Currencies.FirstOrDefault();
            

        }
    }
}
