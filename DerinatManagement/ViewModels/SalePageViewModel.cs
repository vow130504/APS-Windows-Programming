using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DerinatManagement.Model;

namespace DerinatManagement.ViewModels;

public partial class SalePageViewModel : ObservableRecipient
{
    public FullObservableCollection<Product> Products
    {
        get; set;
    }

    public SalePageViewModel()
    {
        // Khởi tạo danh sách sản phẩm với một vài giá trị mẫu
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
        };
    }

}
