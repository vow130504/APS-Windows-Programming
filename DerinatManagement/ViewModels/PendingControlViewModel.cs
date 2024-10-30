using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Model;

namespace DerinatManagement.ViewModels;
public class PendingControlViewModel
{
    public FullObservableCollection<Invoice> PendingInvoices { get; set; }
    public PendingControlViewModel()
    {
        PendingInvoices = new FullObservableCollection<Invoice>()
        {
            new Invoice() {
                ServiceType = "Dịch vụ 1",
                InvoiceItems = new FullObservableCollection<InvoiceItem>()
                {
                    new InvoiceItem() { Name = "Súp kem kiểu Paris", Quantity = 2, Price = 125000 },
                    new InvoiceItem() { Name = "Súp kem rau 4 mùa", Quantity = 1, Price = 125000 },
                    new InvoiceItem() { Name = "Đĩa thịt nguội Tây Ban Nha", Quantity = 1, Price = 125000 },
                    new InvoiceItem() { Name = "BLOODY MARY", Quantity = 2, Price = 30000 },
                    new InvoiceItem() { Name = "Cánh mì bò lò đẫm bóng & phomai", Quantity = 1, Price = 125000 }
                }
            },
            new Invoice() {
                ServiceType = "Dịch vụ 2",
                InvoiceItems = new FullObservableCollection<InvoiceItem>()
                {
                    new InvoiceItem() { Name = "Súp kem kiểu Paris", Quantity = 2, Price = 125000 },
                    new InvoiceItem() { Name = "Súp kem rau 4 mùa", Quantity = 1, Price = 125000 },
                    new InvoiceItem() { Name = "Đĩa thịt nguội Tây Ban Nha", Quantity = 1, Price = 125000 },
                    new InvoiceItem() { Name = "BLOODY MARY", Quantity = 2, Price = 30000 },
                    new InvoiceItem() { Name = "Cánh mì bò lò đẫm bóng & phomai", Quantity = 1, Price = 125000 }
                }
            },
            new Invoice() {
                ServiceType = "Dịch vụ 3",
                InvoiceItems = new FullObservableCollection<InvoiceItem>()
                {
                    new InvoiceItem() { Name = "Súp kem kiểu Paris", Quantity = 2, Price = 125000 },
                    new InvoiceItem() { Name = "Súp kem rau 4 mùa", Quantity = 1, Price = 125000 },
                    new InvoiceItem() { Name = "Đĩa thịt nguội Tây Ban Nha", Quantity = 1, Price = 125000 },
                    new InvoiceItem() { Name = "BLOODY MARY", Quantity = 2, Price = 30000 },
                    new InvoiceItem() { Name = "Cánh mì bò lò đẫm bóng & phomai", Quantity = 1, Price = 125000 }
                }
            }
        };
    }
    public void CompleteOrder(Invoice order)
    {
        PendingInvoices.Remove(order);
    }
}
