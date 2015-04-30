using System;
using System.Data.Linq.Mapping;

namespace BudgetManager.Model.Db
{
    [Table(Name = "transaction_types")]
    public class TransactionType
    {
        [Column(IsPrimaryKey = true, Name = "type_id", IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "name")]
        public String Name { get; set; }

    }
}
