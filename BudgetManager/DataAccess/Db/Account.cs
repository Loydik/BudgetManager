using System;
using System.Data.Linq.Mapping;

namespace BudgetManager.DataAccess.Db
{
    [Table(Name="accounts")]
    public class Account
    {
        [Column(IsPrimaryKey = true, Name="account_id", IsDbGenerated=true)]
        public int ID {get; set;}

        [Column(Name = "name")]
        public String Name {get; set;}

        [Column(Name = "balance")]
        public decimal Balance {get; set;}

        [Column(Name = "type_id")]
        public int TypeID {get; set;}
    }
}
