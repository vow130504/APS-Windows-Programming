using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Model;

namespace DerinatManagement;
public interface IDao
{
    public Category GetCategory(string type);
    public FullObservableCollection<TypeBeverage> GetListTypeBeverage();
    public FullObservableCollection<Invoice> GetPendingOrders();

}
