using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Model;
using DerinatManagement.Services.DataAccess;

namespace DerinatManagement.ViewModels;
public class PendingControlViewModel
{
    public FullObservableCollection<Invoice> PendingInvoices { get; set; }
    public PendingControlViewModel()
    {
        IDao dao = new MockDao();
        PendingInvoices = dao.GetPendingOrders();
    }
    public void CompleteOrder(Invoice order)
    {
        PendingInvoices.Remove(order);
    }
}
