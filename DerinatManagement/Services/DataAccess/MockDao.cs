using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Model;
using DerinatManagement.Services.DataAccess;

namespace DerinatManagement.Services.DataAccess;
public class MockDao : IDao
{
    public Category GetCategory(string type)
    {
        return new Category
        {
            Products = new FullObservableCollection<Product>
                {
                    new Product
                    {
                        Name = "Súp kem kiểu Paris",
                        Price = 125000,
                        Image = "/Assets/soup_paris.jpg"
                    },
                    new Product
                    {
                        Name = "Súp kem rau 4 mùa",
                        Price = 125000,
                        Image = "/Assets/soup_paris.jpg"
                    },
                    new Product
                    {
                        Name = "Đĩa thịt nguội Tây Ban Nha",
                        Price = 125000,
                        Image = "/Assets/soup_paris.jpg"
                    },
                    new Product
                    {
                        Name = "BLOODY MARY",
                        Price = 30000,
                        Image = "/Assets/soup_paris.jpg"
                    },
                    new Product
                    {
                        Name = "Cánh mì bò lò đẫm bóng & phomai",
                        Price = 125000,
                        Image = "/Assets/soup_paris.jpg"
                    }
                }
        };
    }

    public FullObservableCollection<TypeBeverage> GetListTypeBeverage()
    {
        return new FullObservableCollection<TypeBeverage>
        {
             new TypeBeverage()
                {
                    TypeName="Coffee"
                },
             new TypeBeverage()
                {
                    TypeName="Tea"
                },
             new TypeBeverage()
                    {
                        TypeName="Juice"
                    },
             new TypeBeverage()
             {
                        TypeName="Smoothie"
                    }
        };
    }

    public FullObservableCollection<Invoice> GetPendingOrders()
    {
        return new FullObservableCollection<Invoice>();
        //    {
        //        new Invoice
        //        {
        //            ServiceType = "Dịch vụ 1",
        //            InvoiceItems = new FullObservableCollection<InvoiceItem>
        //            {
        //                new InvoiceItem { Name = "Súp kem kiểu Paris", Quantity = 2, Price = 125000 },
        //                new InvoiceItem { Name = "Súp kem rau 4 mùa", Quantity = 1, Price = 125000 },
        //                new InvoiceItem { Name = "Đĩa thịt nguội Tây Ban Nha", Quantity = 1, Price = 125000 },
        //                new InvoiceItem { Name = "BLOODY MARY", Quantity = 2, Price = 30000 },
        //                new InvoiceItem { Name = "Cánh mì bò lò đẫm bóng & phomai", Quantity = 1, Price = 125000 }
        //            }
        //        },
        //        new Invoice
        //        {
        //            ServiceType = "Dịch vụ 2",
        //            InvoiceItems = new FullObservableCollection<InvoiceItem>
        //            {
        //                new InvoiceItem { Name = "Súp kem kiểu Paris", Quantity = 2, Price = 125000 },
        //                new InvoiceItem { Name = "Súp kem rau 4 mùa", Quantity = 1, Price = 125000 },
        //                new InvoiceItem { Name = "Đĩa thịt nguội Tây Ban Nha", Quantity = 1, Price = 125000 },
        //                new InvoiceItem { Name = "BLOODY MARY", Quantity = 2, Price = 30000 },
        //                new InvoiceItem { Name = "Cánh mì bò lò đẫm bóng & phomai", Quantity = 1, Price = 125000 }
        //            }
        //        },
        //        new Invoice
        //        {
        //            ServiceType = "Dịch vụ 3",
        //            InvoiceItems = new FullObservableCollection<InvoiceItem>
        //            {
        //                new InvoiceItem { Name = "Súp kem kiểu Paris", Quantity = 2, Price = 125000 },
        //                new InvoiceItem { Name = "Súp kem rau 4 mùa", Quantity = 1, Price = 125000 },
        //                new InvoiceItem { Name = "Đĩa thịt nguội Tây Ban Nha", Quantity = 1, Price = 125000 },
        //                new InvoiceItem { Name = "BLOODY MARY", Quantity = 2, Price = 30000 },
        //                new InvoiceItem { Name = "Cánh mì bò lò đẫm bóng & phomai", Quantity = 1, Price = 125000 }
        //            }
        //        }
        //    };
    }
}
