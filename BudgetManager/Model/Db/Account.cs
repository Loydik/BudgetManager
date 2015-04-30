using System;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace BudgetManager.Model.Db
{
    [Table(Name="accounts")]
    public class Account
    {
        [Column(IsPrimaryKey = true, Name="account_id", IsDbGenerated=true)]
        public int ID {get; private set;}

        [Column(Name = "name")]
        public String Name {get; private set;}

        [Column(Name = "balance")]
        public decimal Balance { get; private set; }

        
        [Column(Name = "type_id")]
        private int accountType;
        private EntityRef<AccountType> _accountType = new EntityRef<AccountType>();

        [Association(Name = "FK_Accounts_AccountTypes", IsForeignKey = true, Storage = "_accountType", ThisKey = "accountType")]
        public AccountType AccountType
        {
            get { return _accountType.Entity; }
            private set { _accountType.Entity = value; }
        }


    }
}
