using BudgetManager.Model.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.ReportGenerators
{
    public abstract class ReportGenerator
    {
        public abstract void Generate(TimePeriod timePeriod, List<Account> accounts);
    }
}
