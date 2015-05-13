using System;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace BudgetManager.Model.Db
{
    [Table(Name="accounts")]
    public class Account
    {
        [Column(IsPrimaryKey = true, Name="account_id", IsDbGenerated=true)]
        public int Id {get; private set;}

        [Column(Name = "name")]
        public String Name {get; set;}

        [Column(Name = "balance")]
        public decimal Balance { get; set; }

        [Column(Name = "initial_balance")]
        public decimal InitialBalance { get; private set; }

        #pragma warning disable 0169

        [Column(Name = "type_id")]
        private int accountType;
        private EntityRef<AccountType> _accountType = new EntityRef<AccountType>();

        [Association(Name = "FK_accounts_account_types", IsForeignKey = true, Storage = "_accountType", ThisKey = "accountType")]
        public AccountType AccountType
        {
            get { return _accountType.Entity; }
            private set { _accountType.Entity = value; }
        }

        [Column(Name = "currency_id")]
        private int currency;
        private EntityRef<Curency> _currency = new EntityRef<Curency>();

        [Association(Name = "FK_accounts_currency", IsForeignKey = true, Storage = "_currency", ThisKey = "currency")]
        public Curency Curency
        {
            get { return _currency.Entity; }
            set { _currency.Entity = value; }
        }

        #pragma warning restore 0169

        //default parameterless constructor
        public Account()
        {

        }

        public Account(string name, decimal balance,  AccountType type, Curency curr)
        {
            Name = name;
            InitialBalance = balance;
            Balance = InitialBalance;
            AccountType = type;
            Curency = curr;
        }


    }
}
