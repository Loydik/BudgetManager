using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.ViewModel.Util;
using System.Windows.Input;
using System.Windows;

namespace BudgetManager.ViewModel.Accounts
{
    public class DeleteAccountDialogWindowViewModel : ObservableObject
    {
        private string _answer;
        private ICommand _closeWindowCommand;
        private ICommand _confirmDeleteAccountCommand;


        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                RaisePropertyChanged("Answer");
            }
        }


        public ICommand ConfirmDeleteAccountCommand
        {
            get
            {
                if (_confirmDeleteAccountCommand == null)
                {
                    _confirmDeleteAccountCommand = new RelayCommand(
                        param => ConfirmDelete((Window)param), param => ConfirmDeleteCanExecute()
                    );
                }
                return _confirmDeleteAccountCommand;
            }
        }

        public void ConfirmDelete(Window x)
        {
            Mediator.Instance.NotifyListeners(ViewModelMessages.UserDeleteAccount, Answer);
            CloseWindow(x);
        }

        public Boolean ConfirmDeleteCanExecute()
        {

            if (Answer == "DELETE")
            {
                return true;
            }
            else { return false; }
        }


        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                {
                    _closeWindowCommand = new RelayCommand(
                        param => CloseWindow((Window)param)
                        );
                }
                return _closeWindowCommand;
            }
        }

        public void CloseWindow(Window x)
        {
            if (x != null)
            {
                x.Close();
            }
        }
    }
}
