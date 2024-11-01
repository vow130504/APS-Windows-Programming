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
