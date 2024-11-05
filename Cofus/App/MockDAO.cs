using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App;
using Windows.System;

public class MockCoffeeShopDAO : ICoffeeShopDAO
{

    private List<Material> mockMaterials;
    private List<App.User> mockUsers;
    public MockCoffeeShopDAO()
    {
        mockMaterials = new List<Material>
        {
            new Material { MaterialCode = "C001", MaterialName = "Cafe Arabica", Quantity = 100, Category = "Cafe", Unit = "Kg", UnitPrice = 150000, ImportDate = new DateTime(2024, 1, 15), ExpirationDate = new DateTime(2024, 3, 1) },
            new Material { MaterialCode = "C002", MaterialName = "Cafe Robusta", Quantity = 80, Category = "Cafe", Unit = "Kg", UnitPrice = 120000, ImportDate = new DateTime(2024, 2, 10), ExpirationDate = new DateTime(2024, 4, 20) },
            new Material { MaterialCode = "C003", MaterialName = "Cafe Mocha", Quantity = 60, Category = "Cafe", Unit = "Kg", UnitPrice = 180000, ImportDate = new DateTime(2024, 1, 25), ExpirationDate = new DateTime(2024, 3, 15) },
            new Material { MaterialCode = "S001", MaterialName = "Sữa tươi", Quantity = 50, Category = "Sữa", Unit = "Lít", UnitPrice = 20000, ImportDate = new DateTime(2024, 1, 20), ExpirationDate = new DateTime(2024, 2, 28) },
            new Material { MaterialCode = "S002", MaterialName = "Sữa đặc", Quantity = 50, Category = "Sữa", Unit = "Lon", UnitPrice = 20000, ImportDate = new DateTime(2024, 1, 22), ExpirationDate = new DateTime(2024, 4, 5) },
            new Material { MaterialCode = "S003", MaterialName = "Sữa hạnh nhân", Quantity = 30, Category = "Sữa", Unit = "Lít", UnitPrice = 30000, ImportDate = new DateTime(2024, 2, 5), ExpirationDate = new DateTime(2024, 3, 10) },
            new Material { MaterialCode = "S004", MaterialName = "Sữa dừa", Quantity = 40, Category = "Sữa", Unit = "Lít", UnitPrice = 25000, ImportDate = new DateTime(2024, 1, 18), ExpirationDate = new DateTime(2024, 2, 15) },
            new Material { MaterialCode = "T001", MaterialName = "Trà xanh", Quantity = 70, Category = "Trà", Unit = "Kg", UnitPrice = 80000, ImportDate = new DateTime(2024, 1, 10), ExpirationDate = new DateTime(2024, 3, 1) },
            new Material { MaterialCode = "T002", MaterialName = "Trà đen", Quantity = 90, Category = "Trà", Unit = "Kg", UnitPrice = 75000, ImportDate = new DateTime(2024, 2, 12), ExpirationDate = new DateTime(2024, 4, 15) },
            new Material { MaterialCode = "S005", MaterialName = "Sữa yến mạch", Quantity = 25, Category = "Sữa", Unit = "Lít", UnitPrice = 35000, ImportDate = new DateTime(2024, 2, 8), ExpirationDate = new DateTime(2024, 3, 20) },
            new Material { MaterialCode = "C004", MaterialName = "Cafe Espresso", Quantity = 50, Category = "Cafe", Unit = "Kg", UnitPrice = 170000, ImportDate = new DateTime(2024, 1, 30), ExpirationDate = new DateTime(2024, 4, 10) },
            new Material { MaterialCode = "C005", MaterialName = "Cafe Latte", Quantity = 40, Category = "Cafe", Unit = "Kg", UnitPrice = 160000, ImportDate = new DateTime(2024, 2, 15), ExpirationDate = new DateTime(2024, 4, 25) },
            new Material { MaterialCode = "S006", MaterialName = "Sữa dâu", Quantity = 35, Category = "Sữa", Unit = "Lít", UnitPrice = 30000, ImportDate = new DateTime(2024, 2, 20), ExpirationDate = new DateTime(2024, 3, 30) },
            new Material { MaterialCode = "S007", MaterialName = "Sữa socola", Quantity = 45, Category = "Sữa", Unit = "Lít", UnitPrice = 28000, ImportDate = new DateTime(2024, 1, 25), ExpirationDate = new DateTime(2024, 3, 5) },
            new Material { MaterialCode = "T003", MaterialName = "Trà sữa Thái", Quantity = 50, Category = "Trà", Unit = "Kg", UnitPrice = 70000, ImportDate = new DateTime(2024, 2, 18), ExpirationDate = new DateTime(2024, 5, 1) },
            new Material { MaterialCode = "T004", MaterialName = "Trà hoa nhài", Quantity = 60, Category = "Trà", Unit = "Kg", UnitPrice = 85000, ImportDate = new DateTime(2024, 1, 12), ExpirationDate = new DateTime(2024, 3, 20) },
            new Material { MaterialCode = "T005", MaterialName = "Trà ô long", Quantity = 75, Category = "Trà", Unit = "Kg", UnitPrice = 90000, ImportDate = new DateTime(2024, 2, 2), ExpirationDate = new DateTime(2024, 4, 12) },
            new Material { MaterialCode = "S008", MaterialName = "Sữa tách béo", Quantity = 55, Category = "Sữa", Unit = "Lít", UnitPrice = 32000, ImportDate = new DateTime(2024, 1, 28), ExpirationDate = new DateTime(2024, 2, 25) },
            new Material { MaterialCode = "T006", MaterialName = "Trà cam thảo", Quantity = 65, Category = "Trà", Unit = "Kg", UnitPrice = 78000, ImportDate = new DateTime(2024, 1, 16), ExpirationDate = new DateTime(2024, 3, 8) },
            new Material { MaterialCode = "S009", MaterialName = "Sữa hạt điều", Quantity = 20, Category = "Sữa", Unit = "Lít", UnitPrice = 40000, ImportDate = new DateTime(2024, 2, 10), ExpirationDate = new DateTime(2024, 3, 22) }

        };
        mockUsers = new List<App.User>
        {
            new App.User { Username = "123", Password = "123" },
            new App.User { Username = "user2", Password = "pass2" },
            new App.User { Username = "admin", Password = "admin123" }
        };
    }

    // Implement ICoffeeShopDAO methods for Material
    public List<Material> GetAllMaterials() => mockMaterials;
    public Material GetMaterialByCode(string code) => mockMaterials.FirstOrDefault(m => m.MaterialCode == code);
    public bool AddMaterial(Material material)
    {
        if (mockMaterials.Any(m => m.MaterialCode == material.MaterialCode))
            return false;
        mockMaterials.Add(material);
        return true;
    }
    public bool UpdateMaterial(Material material)
    {
        var existingMaterial = GetMaterialByCode(material.MaterialCode);
        if (existingMaterial == null) return false;

        existingMaterial.MaterialName = material.MaterialName;
        existingMaterial.Quantity = material.Quantity;
        existingMaterial.Category = material.Category;
        existingMaterial.Unit = material.Unit;
        existingMaterial.UnitPrice = material.UnitPrice;
        existingMaterial.ImportDate = material.ImportDate;
        existingMaterial.ExpirationDate = material.ExpirationDate;
        return true;
    }
    public bool DeleteMaterial(string code)
    {
        var material = GetMaterialByCode(code);
        if (material == null) return false;
        mockMaterials.Remove(material);
        return true;
    }

    // Implement ICoffeeShopDAO methods for User
    public List<App.User> GetAllUsers() => mockUsers;
    public App.User GetUserByUsername(string username) => mockUsers.FirstOrDefault(u => u.Username == username);
    public bool AddUser(App.User user)
    {
        if (mockUsers.Any(u => u.Username == user.Username))
            return false;
        mockUsers.Add(user);
        return true;
    }
}
