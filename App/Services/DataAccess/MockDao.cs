using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.ViewModels;
using App.ViewModels;

namespace App.Services.DataAccess
{
    public class MockDao
    {
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
