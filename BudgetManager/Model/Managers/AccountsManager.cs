using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;


namespace BudgetManager.Model.Managers
{
    public class AccountsManager
    {
        private Database db;
        public List<Account> Accounts { get; private set; }
        

        public AccountsManager()
        {
            db = new Database();
            Accounts = db.Accounts.ToList();
        }

        public void addAccount(Account acc)
        {
            Accounts.Add(acc);
            db.Accounts.InsertOnSubmit(acc);
        }
    }
}
