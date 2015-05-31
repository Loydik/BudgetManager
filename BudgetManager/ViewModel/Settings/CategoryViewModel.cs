using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.Model.Db;
using BudgetManager.ViewModel.Util;

namespace BudgetManager.ViewModel.Settings
{
    public class CategoryViewModel : ObservableObject
    {
        private Category _categoryObj;

        public Category CategoryObj
        {
            get { return _categoryObj; }
            set
            {
                _categoryObj = value;
                OnPropertyChanged("CategoryObj");
            }
        }

        public int CategoryId
        {
            get { return _categoryObj.ID; }
            set
            {
                _categoryObj.ID = value;
                OnPropertyChanged("CategoryId");
            }
        }

        public String CategoryName
        {
            get { return _categoryObj.Name; }
            set
            {
                _categoryObj.Name = value;
                OnPropertyChanged("CategoryName");
            } 
        }

        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category obj)
        {
            _categoryObj = obj;
        }
    }
}
