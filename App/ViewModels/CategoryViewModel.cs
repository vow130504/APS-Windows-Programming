using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;
using App.Services.DataAccess;

namespace App.ViewModels;
public class CategoryViewModel
{
    public FullObservableCollection<TypeBeverage> ListTypeBeverages
    {
        get; set;
    }
    public CategoryViewModel()
    {
        IDao dao = ServiceFactory.GetChildOf(typeof(IDao)) as IDao;
        ListTypeBeverages = dao.GetListTypeBeverage();
    }
}
