using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BudgetManager.View.CalendarTab
{
    /// <summary>
    /// Converts the specified short day name to its normal counterpart.
    /// </summary>
    [ValueConversion(typeof (string), typeof (string))]
    public class DayNameConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeFormatInfo dateTimeFormat = GetCurrentDateFormat();
            string[] shortestDayNames = dateTimeFormat.ShortestDayNames;
            string[] dayNames = dateTimeFormat.DayNames;

            for (int i = 0; i < shortestDayNames.Count(); i++)
            {
                if (shortestDayNames[i] == value.ToString())
                {
                    return dayNames[i];
                }
            }

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static DateTimeFormatInfo GetCurrentDateFormat()
        {
            if (CultureInfo.CurrentCulture.Calendar is GregorianCalendar)
            {
                return CultureInfo.CurrentCulture.DateTimeFormat;
            }
            foreach (Calendar cal in CultureInfo.CurrentCulture.OptionalCalendars)
            {
                if (cal is GregorianCalendar)
                {
                    DateTimeFormatInfo dtfi = new CultureInfo(CultureInfo.CurrentCulture.Name).DateTimeFormat;
                    dtfi.Calendar = cal;
                    return dtfi;
                }
            }
            DateTimeFormatInfo dt = new CultureInfo(CultureInfo.InvariantCulture.Name).DateTimeFormat;
            dt.Calendar = new GregorianCalendar();
            return dt;
        }
    }
}
