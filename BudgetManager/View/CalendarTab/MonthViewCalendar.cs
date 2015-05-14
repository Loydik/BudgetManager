using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BudgetManager.ViewModel.Calendar;
using BudgetManager.ViewModel.Transactions;

namespace BudgetManager.View.CalendarTab
{
    public class MonthViewCalendar : Calendar
    {  
        static MonthViewCalendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthViewCalendar), new FrameworkPropertyMetadata(typeof(MonthViewCalendar)));
        }

        public MonthViewCalendar()
        {
            this.DataContext = new MonthViewCalendarViewModel();
        }

    }
}
