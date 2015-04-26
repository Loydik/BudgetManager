using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace BudgetManager.DataAccess.Db
{
    [Table(Name="transactions")]
    public class Transaction
    {
        [Column(IsPrimaryKey = true, Name="transaction_id", IsDbGenerated=true)]
        public int ID {get; set;}

        [Column(Name="date")]
        public DateTime Date {get; set;}


        [Column(Name = "Account")]
        private int account;
        private EntityRef<Account> _account = new EntityRef<Account>();

        [Association(Name = "FK_Transactions_Accounts", IsForeignKey = true, Storage = "_account", ThisKey = "account_id")]
        public Account Account
        {
            get { return _account.Entity; }
            set { _account.Entity = value; }
        }

        
        [Column(Name = "amount")]
        public decimal Amount {get; set;}


        [Column(Name = "Currency")]
        private int currency;
        private EntityRef<Curency> _currency = new EntityRef<Curency>();

        [Association(Name = "FK_Transactions_Currencies", IsForeignKey = true, Storage = "_currency", ThisKey = "currency_id")]
        public Curency Curency
        {
            get { return _currency.Entity; }
            set { _currency.Entity = value; }
        }

        
        [Column(Name = "Category")]
        private int category;
        private EntityRef<Category> _category = new EntityRef<Category>();

        [Association(Name = "FK_Transactions_Categories", IsForeignKey = true, Storage = "_category", ThisKey = "category_id")]
        public Category Category
        {
            get { return _category.Entity; }
            set { _category.Entity = value; }
        }

        
        [Column(Name = "comments")]
        public String Comments {get; set;}

       
        [Column(Name = "TransactionType")]
        private int transactionType;
        private EntityRef<TransactionType> _transactionType = new EntityRef<TransactionType>();

        [Association(Name = "FK_Transactions_TransactionTypes", IsForeignKey = true, Storage = "_transactionType", ThisKey = "type_id")]
        public TransactionType TransactionType
        {
            get { return _transactionType.Entity; }
            set { _transactionType.Entity = value; }
        }
    }
}

