using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace BudgetManager.Model.Db
{
    [Table(Name="transactions")]
    public class Transaction
    {
        [Column(IsPrimaryKey = true, Name="transaction_id", IsDbGenerated=true)]
        public int? ID {get; set;}

        [Column(Name="date")]
        public DateTime Date {get; set;}


        [Column(Name = "account_id")]
        private int account;
        private EntityRef<Account> _account = new EntityRef<Account>();

        [Association(Name = "FK_transactions_accounts", IsForeignKey = true, Storage = "_account", ThisKey = "account")]
        public Account Account
        {
            get { return _account.Entity; }
            set { _account.Entity = value; }
        }

        
        [Column(Name = "amount")]
        public decimal Amount {get; set;}

        
        [Column(Name = "category_id")]
        private int category;
        private EntityRef<Category> _category = new EntityRef<Category>();

        [Association(Name = "FK_transactions_categories", IsForeignKey = true, Storage = "_category", ThisKey = "category")]
        public Category Category
        {
            get { return _category.Entity; }
            set { _category.Entity = value; }
        }

        
        [Column(Name = "comments")]
        public String Comments {get; set;}

       
        [Column(Name = "type_id")]
        private int transactionType;
        private EntityRef<TransactionType> _transactionType = new EntityRef<TransactionType>();

        [Association(Name = "FK_transactions_transaction_types", IsForeignKey = true, Storage = "_transactionType", ThisKey = "transactionType")]
        public TransactionType TransactionType
        {
            get { return _transactionType.Entity; }
            set { _transactionType.Entity = value; }
        }

        [Column(Name = "account_balance_after")]
        public decimal? AccountBalanceAfter { get; set; }

        //default parameterless constructor
        public Transaction() { }

        public Transaction(DateTime date, Account acc, decimal amount, Category category, String comments, TransactionType type)
        {
            this.Date = date;
            this.Account = acc;
            this.Amount = amount;
            this.Category = category;
            this.Comments = comments;
            this.TransactionType = type;
        }

        public Transaction(DateTime date, Account acc, decimal amount, Category category, String comments, TransactionType type, decimal? accountBalanceAfter)
        {
            this.Date = date;
            this.Account = acc;
            this.Amount = amount;
            this.Category = category;
            this.Comments = comments;
            this.TransactionType = type;
            this.AccountBalanceAfter = accountBalanceAfter;
        }
    }
}

