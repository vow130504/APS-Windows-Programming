using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Services.DataAccess;

namespace DerinatManagement.Model;
public class TypeBeverage: INotifyPropertyChanged
{
    public string TypeName
    {
        get; set;
    }
    public FullObservableCollection<Product> Products { get; set; }
    public TypeBeverage()
    {
        IDao dao= new MockDao();
        Products= dao.GetCategory(TypeName).Products;
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
