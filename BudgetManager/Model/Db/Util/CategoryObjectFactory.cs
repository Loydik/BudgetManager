using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Model.Db.Util
{
    public class CategoryObjectFactory
    {
        private Dictionary<String, ICategory> categories = new Dictionary<string,ICategory>();

        public int ObjectsCreated
        { 
            get {return categories.Count;}
        }

        public ICategory GetCategory(string name)
        {
            ICategory category = null;

            if (categories.ContainsKey(name))
            {
                category = categories[name];
            }
            else 
            {
                category = new Category();
                category.setName(name);
                categories.Add(name, category);
            }

            return category;
        }
    }
}
