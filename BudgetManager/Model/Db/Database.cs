//A class representing a database connection

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Linq;

namespace BudgetManager.Model.Db
{
    [Database(Name="BudgetManagerDatabase")]
    public class Database : DataContext
    {
        static String connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=" + Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\BudgetManagerDatabase.mdf;Integrated Security=True";  
        

        public Database(): base(connectionString)
        {

        }

        public Table<Account> Accounts;

        public Table<Transaction> Transactions;

        public Table<Category> Categories;

        public Table<Curency> Currencies;

        public Table<AccountType> AccountTypes;

        public Table<TransactionType> TransactionTypes;
    }
}
