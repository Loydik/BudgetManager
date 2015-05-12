//Code taken from http://www.codeproject.com/Articles/35277/MVVM-Mediator-Pattern

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.ViewModel.Util
{
     /// <summary>
     /// Pattern used for cross-ViewModelCommunication
     /// </summary>

    public enum ViewModelMessages { UserDeleteAccount = 1 };

    public sealed class Mediator
    {

         #region Data

         private static readonly Mediator _instance = new Mediator();
         private volatile object _locker = new object();

         private MultiDictionary<ViewModelMessages, Action<Object>> internalList = new MultiDictionary<ViewModelMessages, Action<object>>();

         #endregion



         #region Construction

         static Mediator()
         {

         }

         private Mediator()
         {

         }

         #endregion


         public static Mediator Instance
         {
             get { return _instance; }
         }

         /// <summary>
         /// Registers a Colleague to a specific message
         /// </summary>
         /// <param name="callback">The callback to use 
         /// when the message it seen</param>
         /// <param name="message">The message to 
         /// register to</param>
         public void Register(Action<Object> callback, ViewModelMessages message)
         {
             internalList.AddValue(message, callback);
         }


         /// <summary>
         /// Notify all colleagues that are registed to the 
         /// specific message
         /// </summary>
         /// <param name="message">The message for the notify by</param>
         /// <param name="args">The arguments for the message</param>

         public void NotifyListeners(ViewModelMessages message, object args)
         {
             if (internalList.ContainsKey(message))
               {
                    //forward the message to all listeners
                    foreach (Action<object> callback in internalList[message])
                           callback(args);
                }
         }
    }
}
