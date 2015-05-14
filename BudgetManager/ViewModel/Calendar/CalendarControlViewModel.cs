using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Managers;
using BudgetManager.ViewModel.Transactions;
using BudgetManager.ViewModel.Util;

namespace BudgetManager.ViewModel.Calendar
{
    public class CalendarControlViewModel : IPageViewModel
    {
       

        public string Name {
            get { return "Calendar"; }
        }

       

    }
}
