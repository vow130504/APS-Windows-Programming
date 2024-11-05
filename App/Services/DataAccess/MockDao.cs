using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using App.Model;
using App.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace App;

public class MockDao : IDao
{
    private readonly FullObservableCollection<Category> _categories;
    private readonly FullObservableCollection<Invoice> _pendingOrders;
    private readonly FullObservableCollection<Product> _products;

    public MockDao()
    {
        // Dữ liệu mẫu cho BEVERAGE với mã loại thức uống
        _products = new()
        {
            new() { Id = 1, Name = "Latte", Image = "/Assets/latte.jpg", TypeBeverageId = 1 },
            new() { Id = 2, Name = "Green Tea", Image = "/Assets/green_tea.jpg", TypeBeverageId = 2 },
            new() { Id = 3, Name = "Milk Tea", Image = "/Assets/milk_tea.jpg", TypeBeverageId = 2 },
            new() { Id = 4, Name = "Orange Juice", Image = "/Assets/orange_juice.jpg", TypeBeverageId = 3 },
            new() { Id = 5, Name = "Espresso", Image = "/Assets/espresso.jpg", TypeBeverageId = 1 },
            new() { Id = 6, Name = "Black Coffee", Image = "/Assets/black_coffee.jpg", TypeBeverageId = 1 },
            new() { Id = 7, Name = "Oolong Tea", Image = "/Assets/oolong_tea.jpg", TypeBeverageId = 2 },
            new() { Id = 8, Name = "Apple Juice", Image = "/Assets/apple_juice.jpg", TypeBeverageId = 3 },
            new() { Id = 9, Name = "Smoothie", Image = "/Assets/smoothie.jpg", TypeBeverageId = 4 },
            new() { Id = 10, Name = "Iced Latte", Image = "/Assets/iced_latte.jpg", TypeBeverageId = 1 },
            new() { Id = 11, Name = "Matcha Latte", Image = "/Assets/matcha_latte.jpg", TypeBeverageId = 2 },
            new() { Id = 12, Name = "Lemon Tea", Image = "/Assets/lemon_tea.jpg", TypeBeverageId = 2 },
            new() { Id = 13, Name = "Pineapple Juice", Image = "/Assets/pineapple_juice.jpg", TypeBeverageId = 3 },
            new() { Id = 14, Name = "Mango Smoothie", Image = "/Assets/mango_smoothie.jpg", TypeBeverageId = 4 },
            new() { Id = 15, Name = "Cappuccino", Image = "/Assets/cappuccino.jpg", TypeBeverageId = 1 }
        };

        // Dữ liệu mẫu cho CATEGORY
        _categories = new()
        {
            new("All", new FullObservableCollection<Product>(_products.ToList())),
            new("Coffee", new FullObservableCollection<Product>(_products.Where(p => p.TypeBeverageId == 1).ToList())),
            new("Tea", new FullObservableCollection<Product>(_products.Where(p => p.TypeBeverageId == 2).ToList())),
            new("Juice", new FullObservableCollection<Product>(_products.Where(p => p.TypeBeverageId == 3).ToList())),
            new("Smoothie", new FullObservableCollection<Product>(_products.Where(p => p.TypeBeverageId == 4).ToList()))
        };

        // Dữ liệu mẫu cho ORDERS
        _pendingOrders = new()
        {
            new()
            {
                InvoiceNumber = 1,
                TableNumber = 101,
                CreatedTime = DateTime.Parse("2024-10-31 12:40:00"),
                PaymentMethod = "Credit Card"
            },
            new()
            {
                InvoiceNumber = 2,
                TableNumber = 102,
                CreatedTime = DateTime.Parse("2024-10-31 13:10:00"),
                PaymentMethod = "Cash"
            },
            new()
            {
                InvoiceNumber = 3,
                TableNumber = 103,
                CreatedTime = DateTime.Parse("2024-10-31 14:00:00"),
                PaymentMethod = "Debit Card"
            },
            new()
            {
                InvoiceNumber = 4,
                TableNumber = 104,
                CreatedTime = DateTime.Parse("2024-10-31 14:30:00"),
                PaymentMethod = "Cash"
            },
            new()
            {
                InvoiceNumber = 5,
                TableNumber = 105,
                CreatedTime = DateTime.Parse("2024-10-31 15:00:00"),
                PaymentMethod = "Credit Card"
            }
        };
    }
    public FullObservableCollection<Product> GetAllBeverage()
    {
        return new FullObservableCollection<Product>(_products);
    }
    public Category GetCategory(string category)
    {
        var categoryResult = _categories.FirstOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
        return categoryResult ?? new Category
        (
            "Không có sản phẩm nào",
            new FullObservableCollection<Product>
            {
                new Product { Name = "Không có sản phẩm nào", Price = 0, Size = "M", Image = "Assets/no_product.jpg" }
            }
        );
    }

    public FullObservableCollection<Category> GetListTypeBeverage()
    {
        return new FullObservableCollection<Category>(_categories);
    }
    public int GetProductPrice(int beverageId, string size)
    {
        // Bảng giá dựa trên beverageId và kích thước sản phẩm
        var priceTable = new Dictionary<int, Dictionary<string, int>>
    {
        { 1, new Dictionary<string, int> { { "S", 40000 }, { "M", 45000 }, { "L", 50000 } } }, // Latte
        { 2, new Dictionary<string, int> { { "S", 30000 }, { "M", 35000 }, { "L", 38000 } } }, // Green Tea
        { 3, new Dictionary<string, int> { { "S", 28000 }, { "M", 30000 }, { "L", 35000 } } }, // Milk Tea
        { 4, new Dictionary<string, int> { { "S", 25000 }, { "M", 27000 }, { "L", 30000 } } }, // Orange Juice
        { 5, new Dictionary<string, int> { { "S", 40000 }, { "M", 45000 } } }, // Espresso
        { 6, new Dictionary<string, int> { { "S", 35000 }, { "M", 37000 }, { "L", 40000 } } }, // Black Coffee
        { 7, new Dictionary<string, int> { { "S", 32000 }, { "M", 35000 }, { "L", 38000 } } }, // Oolong Tea
        { 8, new Dictionary<string, int> { { "S", 27000 }, { "M", 29000 }, { "L", 31000 } } }, // Apple Juice
        { 9, new Dictionary<string, int> { { "M", 45000 }, { "L", 50000 } } }, // Smoothie
        { 10, new Dictionary<string, int> { { "S", 42000 }, { "M", 47000 }, { "L", 50000 } } }, // Iced Latte
        { 11, new Dictionary<string, int> { { "S", 43000 }, { "M", 48000 }, { "L", 51000 } } }, // Matcha Latte
        { 12, new Dictionary<string, int> { { "S", 28000 }, { "M", 32000 }, { "L", 35000 } } }, // Lemon Tea
        { 13, new Dictionary<string, int> { { "S", 27000 }, { "M", 29000 }, { "L", 31000 } } }, // Pineapple Juice
        { 14, new Dictionary<string, int> { { "M", 47000 }, { "L", 50000 } } }, // Mango Smoothie
        { 15, new Dictionary<string, int> { { "S", 42000 }, { "M", 45000 }, { "L", 48000 } } }  // Cappuccino
    };

        // Kiểm tra beverageId và kích thước trong bảng giá
        if (priceTable.ContainsKey(beverageId))
        {
            if (priceTable[beverageId].ContainsKey(size))
            {
                return priceTable[beverageId][size];  // Trả về giá nếu tìm thấy beverageId và kích thước
            }
            else
            {
                Console.WriteLine($"Kích thước '{size}' không có sẵn cho beverageId '{beverageId}'.");
                return -1;  // Trả về -1 nếu không tìm thấy kích thước
            }
        }
        else
        {
            Console.WriteLine($"BeverageId '{beverageId}' không có trong bảng giá.");
            return -1;  // Trả về -1 nếu không tìm thấy beverageId
        }
    }



    public FullObservableCollection<Invoice> GetPendingOrders()
    {
        return new FullObservableCollection<Invoice>(_pendingOrders);
    }

    public async Task<int> CreateOrder(Invoice invoice)
    {
        var newOrderId = _pendingOrders.Count + 1;
        invoice.InvoiceNumber = newOrderId;
        invoice.CreatedTime = DateTime.Now;
        _pendingOrders.Add(invoice);
        await Task.Delay(50);
        return newOrderId;
    }

    public async Task AddOrderDetail(int orderId, InvoiceItem item)
    {
        var invoice = _pendingOrders.FirstOrDefault(o => o.InvoiceNumber == orderId);
        if (invoice != null)
        {
            // Mô phỏng thêm sản phẩm vào hóa đơn
        }
        await Task.Delay(50);
    }
    public List<string> GetAllPaymentMethod()
    {
        return new() { "Tiền mặt", "Thẻ tín dụng", "Ví điện tử" };
    }
    public bool CompletePendingOrder(Invoice order)
    {
        var invoice = _pendingOrders.FirstOrDefault(o => o.InvoiceNumber == order.InvoiceNumber);
        if (invoice != null)
        {
            invoice.MarkAsPaid();
            _pendingOrders.Remove(invoice);
            return true;
        }
        return false;
    }
    public async Task<RevenueData> GetRevenueData(DateTime selectedDate, DateTime previousDate)
    {
        // Giả lập dữ liệu doanh thu
        await Task.Delay(100); // Giả lập một thao tác không đồng bộ

        return new RevenueData
        {
            OrderCount = 10,
            TotalRevenue = 500000,
            CashAmount = 300000
        };
    }

    public async Task<RevenueData> GetYesterdayRevenue(DateTime previousDate)
    {
        await Task.Delay(100);

        return new RevenueData
        {
            TotalRevenue = 450000
        };
    }



    public async Task<List<TopProduct>> GetTopProducts(DateTime selectedDate)
    {
        await Task.Delay(100);

        return new List<TopProduct>
        {
            new TopProduct { Name = "Cà phê", Revenue = 200000 },
            new TopProduct { Name = "Trà sữa", Revenue = 150000 },
            new TopProduct { Name = "Nước ngọt", Revenue = 100000 },
            new TopProduct { Name = "Sinh tố", Revenue = 50000 },
            new TopProduct { Name = "Trà", Revenue = 30000 },
        };
    }

    public async Task<List<TopCategory>> GetTopCategories(DateTime selectedDate)
    {
        await Task.Delay(100);

        return new List<TopCategory>
        {
            new TopCategory { Name = "Nước uống", Revenue = 300000 },
            new TopCategory { Name = "Đồ ăn nhẹ", Revenue = 200000 },
            new TopCategory { Name = "Trà", Revenue = 100000 },
            new TopCategory { Name = "Cà phê", Revenue = 50000 },
            new TopCategory { Name = "Sinh tố", Revenue = 30000 },
        };
    }

    public async Task<List<TopSeller>> GetTopSellers(DateTime selectedDate)
    {
        await Task.Delay(100);

        return new List<TopSeller>
        {
            new TopSeller { Name = "Trà sữa", Amount = 10 },
            new TopSeller { Name = "Cà phê", Amount = 7 },
            new TopSeller { Name = "Trà", Amount = 5 },
            new TopSeller { Name = "Đồ ăn nhẹ", Amount = 4 },
            new TopSeller { Name = "Sinh tố", Amount = 1 },
        };
    }

    public class RevenueData
    {
        public int OrderCount
        {
            get; set;
        }
        public int TotalRevenue
        {
            get; set;
        }
        public int CashAmount
        {
            get; set;
        }
    }
}
