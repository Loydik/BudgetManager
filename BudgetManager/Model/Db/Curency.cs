using System;
using System.Data.Linq.Mapping;

namespace BudgetManager.Model.Db
{
    [Table(Name = "currency")]
    public class Curency
    {
        [Column(IsPrimaryKey = true, Name = "currency_id", IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "name")]
        public String name { get; set; }

        [Column(Name = "symbol")]
        public String Symbol { get; set; }

    }
}
