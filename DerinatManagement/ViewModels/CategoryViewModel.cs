using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinatManagement.Model;

namespace DerinatManagement.ViewModels;
public class CategoryViewModel
{
    public Category Category
    {
        get; set;
    }
    public CategoryViewModel()
    {
        Category = new Category
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
}
