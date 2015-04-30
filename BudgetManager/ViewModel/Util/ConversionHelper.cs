using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BudgetManager.ViewModel.Util
{
    public static class ConversionHelper
    {

        //generic method to convert list of IEnumerable items into observable list
        public static ObservableCollection<T> toObservableCollection<T, U>(IEnumerable<U> original, Func<U,T> del) where T : ObservableObject
                                                                                          
        {
            ObservableCollection<T> observable = new ObservableCollection<T>();

            if (original.Count() != 0 && original != null)
            {
                foreach (U obj in original)
                {
                    observable.Add(del(obj));
                }
            }

            return observable;
        }
    }
}
