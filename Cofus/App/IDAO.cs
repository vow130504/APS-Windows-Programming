using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;
using Windows.System;

public interface ICoffeeShopDAO
{
    List<Material> GetAllMaterials();
    Material GetMaterialByCode(string code);
    bool AddMaterial(Material material);
    bool UpdateMaterial(Material material);
    bool DeleteMaterial(string code);



    // Quản lý người dùng
    List<App.User> GetAllUsers();
    App.User GetUserByUsername(string username);
    bool AddUser(App.User user);
}
