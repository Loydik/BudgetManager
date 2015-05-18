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
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                //create an emtpy observable collection object
                var observableCollection = new ObservableCollection<T>();

                //loop through all the records and add to observable collection object
                foreach (var item in enumerableList)
                    observableCollection.Add(item);

                //return the populated observable collection
                return observableCollection;
            }
            return null;
        }

        //generic method to convert list of IEnumerable items into observable list
        public static ObservableCollection<T> ToObservableCollection<T, TU>(IEnumerable<TU> original, Func<TU,T> del) where T : ObservableObject                                                                              
        {
            ObservableCollection<T> observable = new ObservableCollection<T>();

            if (original.Count() != 0 && original != null)
            {
                foreach (TU obj in original)
                {
                    observable.Add(del(obj));
                }
            }

            return observable;
        }
    }
}
