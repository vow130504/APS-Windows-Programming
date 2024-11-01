using System;
using System.Collections.Generic;
using Npgsql;
using DerinatManagement.Model;
using DerinatManagement;

namespace DemoListBinding1610;

public class SqlServerDao : IDao
{
    private readonly string _connectionString = """
            Host=localhost;
            Database=mystore;
            Username=postgres;
            Password=1234;
        """;

    // Lấy thông tin Category theo loại
    public Category GetCategory(string category)
    {
        
        var categoryResult = new Category
        {
            Products = new FullObservableCollection<Product>()
        };

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        var sql = @"
                SELECT tb.""ID"" AS CategoryID, tb.""CATEGORY"", b.""ID"" AS BeverageID, b.""BEVERAGE_NAME"", b.""SIZE"", b.""PRICE"", b.""IMAGE_PATH""
                FROM ""TYPE_BEVERAGE"" tb
                JOIN ""BEVERAGE"" b ON tb.""ID"" = b.""CATEGORY_ID""
                WHERE tb.""CATEGORY"" = @Category";

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Category", category);
        command.Parameters["@Category"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            // Thêm từng sản phẩm vào danh sách Products
            var product = new Product
            {
                Name = (string)reader["BEVERAGE_NAME"],
                Price = (int)reader["PRICE"],
                Size = (string)reader["SIZE"],
                Image = (string)reader["IMAGE_PATH"]
            };
            categoryResult.Products.Add(product);
        }
        int count = categoryResult.Products.Count;

        return count > 0 ? categoryResult : new Category { Products = new FullObservableCollection<Product>()
        {
            new Product { Name = "Không có sản phẩm nào", Price = 0, Size = "M", Image="Assets/soup_paris.jpg"}
        } };
    }

    // Lấy danh sách loại thức uống
    public FullObservableCollection<TypeBeverage> GetListTypeBeverage()
    {
        var result = new FullObservableCollection<TypeBeverage>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        var sql = "SELECT * FROM \"TYPE_BEVERAGE\"";
        using var command = new NpgsqlCommand(sql, connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var name = (string)reader["CATEGORY"];
            var typeBeverage = new TypeBeverage(name);
            result.Add(typeBeverage);
        }

        return result;
    }

    // Lấy danh sách hóa đơn chờ xử lý
    public FullObservableCollection<Invoice> GetPendingOrders()
    {
        var result = new FullObservableCollection<Invoice>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        var sql = "SELECT * FROM ORDERS WHERE COMPLETED_TIME IS NULL";
        using var command = new NpgsqlCommand(sql, connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var invoice = new Invoice
            {
                InvoiceNumber = (int)reader["ORDER_ID"],
                TableNumber = (int)reader["RESERVED_TABLE_ID"],
                CreatedTime = (DateTime)reader["ORDER_TIME"],
            };
            result.Add(invoice);
        }

        return result;
    }
}
