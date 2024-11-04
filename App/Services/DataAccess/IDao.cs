using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;
using Microsoft.UI.Xaml.Controls;

namespace App;
public interface IDao
{
    Category GetCategory(string type);
    FullObservableCollection<Category> GetListTypeBeverage();
    FullObservableCollection<Invoice> GetPendingOrders();
    Task<int> CreateOrder(Invoice invoice);
    Task AddOrderDetail(int orderId, InvoiceItem item);
    List<string> GetAllPaymentMethod();
    bool CompletePendingOrder(Invoice order);
    FullObservableCollection<Product> GetAllBeverage();
}
