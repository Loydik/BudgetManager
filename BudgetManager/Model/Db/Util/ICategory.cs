using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.Db.Util
{
    public interface ICategory
    {
        String getName();
        void setName(String str);
    }
}
