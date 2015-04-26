using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite.Linq;
using System.Data.SQLite;
using System.IO;
using System.Data.Linq;

namespace BudgetManager.DataAccess.Db
{
    [Database(Name="localdb")]
    public class Database
    {
        private String connectionString = "Data Source=" + Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\localdb.db";  
        private DataContext context;

        public Database()
        {
           //testing
           var connection = new SQLiteConnection(connectionString);
           context = new DataContext(connection);
           Categories = context.GetTable<Category>();
           var amount = Categories.Count();
        }

        public Table<Account> Accounts;
        public Table<Transaction> Transactions;
        public Table<Category> Categories;
        public Table<Curency> Currencies;
        public Table<AccountType> AccountTypes;
        public Table<TransactionType> TransactionTypes;
    }
}
