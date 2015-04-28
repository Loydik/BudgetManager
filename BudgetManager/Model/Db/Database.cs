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
        static String connectionString = "Data Source=" + Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\localdb.db";  
        private DataContext context;

        public Database()
        {
           var connection = new SQLiteConnection(connectionString);
           context = new DataContext(connection);
        }

        public Table<Account> Accounts
        {
            get { return context.GetTable<Account>(); }
        }

        public Table<Transaction> Transactions
        { 
            get { return context.GetTable<Transaction>(); }
        }
        

        public Table<Category> Categories
        {
            get { return context.GetTable<Category>(); }
        }

        public Table<Curency> Currencies
        {
            get { return context.GetTable<Curency>(); }
        }

        public Table<AccountType> AccountTypes
        {
            get { return context.GetTable<AccountType>(); }
        }

        public Table<TransactionType> TransactionTypes
        {
            get { return context.GetTable<TransactionType>(); }
        }
    }
}
