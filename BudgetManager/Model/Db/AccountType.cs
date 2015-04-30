using System;
using System.Data.Linq.Mapping;

namespace BudgetManager.Model.Db
{
    [Table(Name = "account_types")]
    public class AccountType
    {
        [Column(IsPrimaryKey = true, Name = "type_id", IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "name")]
        public String name { get; set; }

    }
}
