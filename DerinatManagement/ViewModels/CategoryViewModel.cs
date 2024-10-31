using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Model;
using DerinatManagement.Services.DataAccess;

namespace DerinatManagement.ViewModels;
public class CategoryViewModel
{
    public Category Category
    {
        get; set;
    }
    public FullObservableCollection<TypeBeverage> ListTypeBeverages
    {
        get; set;
    }
    public CategoryViewModel()
    {
        IDao dao = new MockDao();
        
        Category = dao.GetCategory("Beverages");
        ListTypeBeverages = dao.GetListTypeBeverage();
    }
}
