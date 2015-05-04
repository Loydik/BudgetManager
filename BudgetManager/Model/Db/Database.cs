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
           //var connection = new SQLiteConnection(connectionString);
           //context = new DataContext(connection);
        }

        public Table<Account> Accounts;
        ////{
            //get { return context.GetTable<Account>(); }
        //}

        public Table<Transaction> Transactions;
       // { 
            //get { return context.GetTable<Transaction>(); }
        //}


        public Table<Category> Categories;
        //{
            //get { return context.GetTable<Category>(); }
       // }

        public Table<Curency> Currencies;
      //  {
            //get { return context.GetTable<Curency>(); }
      //  }

        public Table<AccountType> AccountTypes;
       // {
            //get { return context.GetTable<AccountType>(); }
        //}

        public Table<TransactionType> TransactionTypes;
        //{
            //get { return context.GetTable<TransactionType>(); }
        //}

        /*
        public void Submit()
        {
             //context.Connection.Open();
            // context.Log = Console.Out;
             //context.SubmitChanges();
            this.SubmitChanges();
        }*/

    }
}
