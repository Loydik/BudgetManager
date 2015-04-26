using System;
using System.Data.Linq.Mapping;

namespace BudgetManager.DataAccess.Db
{
    [Table(Name = "categories")]
    public class Category
    {
        [Column(IsPrimaryKey = true, Name = "category_id", IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "name")]
        public String Name { get; set; }

    }
}
