using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.ReportGenerators
{
    public class TimePeriod
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public TimePeriod(DateTime start, DateTime end)
        {
            _startDate = start;
            _endDate = end;
        }
    }
}
