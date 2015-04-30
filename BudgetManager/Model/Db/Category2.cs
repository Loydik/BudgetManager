using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db.Util;

namespace BudgetManager.Model.Db
{
    public partial class Category : ICategory
    {
        //Extra Methods

        public String getName()
        {
            return this.Name;
        }

        public void setName(String name)
        {
            this.Name = name;
        }


    }
}
