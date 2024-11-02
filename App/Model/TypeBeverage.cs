using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.DataAccess;

namespace App.Model;
public class TypeBeverage: INotifyPropertyChanged
{
    public string TypeName
    {
        get; set;
    }
    public FullObservableCollection<Product> Products { get; set; }
    public TypeBeverage(string name)
    {
        TypeName = name;
        IDao dao= ServiceFactory.GetChildOf(typeof(IDao)) as IDao;
        Products = dao.GetCategory(TypeName).Products;
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
