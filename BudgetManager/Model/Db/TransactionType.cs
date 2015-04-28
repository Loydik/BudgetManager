using System;
using System.Data.Linq.Mapping;

namespace BudgetManager.DataAccess.Db
{
    [Table(Name = "transaction_types")]
    public class TransactionType
    {
        [Column(IsPrimaryKey = true, Name = "type_id", IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "name")]
        public String name { get; set; }

    }
}
