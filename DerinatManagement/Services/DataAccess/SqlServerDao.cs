using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DerinatManagement.Model;
using DerinatManagement;

namespace DemoListBinding1610;

public class SqlServerDao : IDao
{
    private readonly string _connectionString = """
        Server=localhost;
        Database=Cofus_Management;
        User ID=db;
        Password=1234;
        TrustServerCertificate=True;
    """;

    // Lấy thông tin Category theo loại
    public Category GetCategory(string category)
    {
        Category categoryResult = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sql = @"
                SELECT tb.ID AS CategoryID, tb.CATEGORY, b.ID AS BeverageID, b.BEVERAGE_NAME, b.SIZE, b.PRICE
                FROM TYPE_BEVERAGE tb
                JOIN BEVERAGE b ON tb.ID = b.CATEGORY_ID
                WHERE tb.CATEGORY = @Category";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Category", category);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (categoryResult == null)
                    {
                        // Khởi tạo categoryResult lần đầu tiên
                        categoryResult = new Category
                        {
                            Products = new FullObservableCollection<Product>()
                        };
                    }

                    // Thêm từng sản phẩm vào danh sách Products
                    var product = new Product
                    {
                        Name = (string)reader["BEVERAGE_NAME"],
                        Price = (decimal)reader["PRICE"],
                        Size = (string)reader["SIZE"]
                    };
                    categoryResult.Products.Add(product);
                }
            }
        }
        return categoryResult;
    }

    // Lấy danh sách loại thức uống
    public FullObservableCollection<TypeBeverage> GetListTypeBeverage()
    {
        var result = new FullObservableCollection<TypeBeverage>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sql = "SELECT * FROM TYPE_BEVERAGE";
            using (var command = new SqlCommand(sql, connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var typeBeverage = new TypeBeverage
                    {
                        TypeName = (string)reader["CATEGORY"]
                    };
                    result.Add(typeBeverage);
                }
            }
        }
        return result;
    }

    // Lấy danh sách hóa đơn chờ xử lý
    public FullObservableCollection<Invoice> GetPendingOrders()
    {
        var result = new FullObservableCollection<Invoice>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sql = "SELECT * FROM ORDERS WHERE COMPLETED_TIME IS NULL";
            using (var command = new SqlCommand(sql, connection))
            {
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
            }
        }
        return result;
    }
}
