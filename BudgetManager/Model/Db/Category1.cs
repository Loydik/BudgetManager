using System;
using System.Data.Linq.Mapping;
using BudgetManager.Model.Db.Util;

namespace BudgetManager.Model.Db
{
    //Table Mapping

    [Table(Name = "categories")]
    public partial class Category : ICategory
    {
        [Column(IsPrimaryKey = true, Name = "category_id", IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(Name = "name")]
        public String Name { get; set; }

    }
}
