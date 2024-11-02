using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;
using App.Services.DataAccess;

namespace App.ViewModels;
public class PendingControlViewModel
{
    public FullObservableCollection<Invoice> PendingInvoices { get; set; }
    public PendingControlViewModel()
    {
        IDao dao = ServiceFactory.GetChildOf(typeof(IDao)) as IDao;
        PendingInvoices = dao.GetPendingOrders();
    }
    public void CompleteOrder(Invoice order)
    {
        PendingInvoices.Remove(order);
    }
}
