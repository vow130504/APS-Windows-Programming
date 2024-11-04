using App.Services.DataAccess;
using App.Services.DataAccess;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class RevenueViewModel : INotifyPropertyChanged
    {
        private readonly MockDao _mockDao = new MockDao();

        private int _orderCount;
        private int _totalRevenue;
        private int _cashAmount;
        private int _transferAmount;
        private string _revenueText;
        private string _yesterdayRevenueText;

        public int OrderCount
        {
            get => _orderCount;
            set
            {
                _orderCount = value;
                OnPropertyChanged();
            }
        }

        public int TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                _totalRevenue = value;
                OnPropertyChanged();
            }
        }

        public int CashAmount
        {
            get => _cashAmount;
            set
            {
                _cashAmount = value;
                OnPropertyChanged();
            }
        }

        public int TransferAmount
        {
            get => _transferAmount;
            set
            {
                _transferAmount = value;
                OnPropertyChanged();
            }
        }

        public string RevenueText
        {
            get => _revenueText;
            set
            {
                _revenueText = value;
                OnPropertyChanged();
            }
        }

        public string YesterdayRevenueText
        {
            get => _yesterdayRevenueText;
            set
            {
                _yesterdayRevenueText = value;
                OnPropertyChanged();
            }
        }

        public List<TopProduct> TopProducts { get; set; } = new List<TopProduct>(5);
        public List<TopCategory> TopCategories { get; set; } = new List<TopCategory>(5);
        public List<TopSeller> TopSellers { get; set; } = new List<TopSeller>(5);

        public async Task LoadRevenueData(DateTime selectedDate)
        {
            var previousDate = selectedDate.AddDays(-1);

            // Lấy dữ liệu từ MockDao
            var revenueData = await _mockDao.GetRevenueData(selectedDate, previousDate);
            var yesterdayRevenueData = await _mockDao.GetYesterdayRevenue(previousDate);
            var topProductsData = await _mockDao.GetTopProducts(selectedDate);
            var topCategoriesData = await _mockDao.GetTopCategories(selectedDate);
            var topSellersData = await _mockDao.GetTopSellers(selectedDate);

            // Gán dữ liệu vào ViewModel
            OrderCount = revenueData.OrderCount;
            TotalRevenue = revenueData.TotalRevenue;
            CashAmount = revenueData.CashAmount;
            TransferAmount = TotalRevenue - CashAmount;

            var yesterdayRevenue = yesterdayRevenueData.TotalRevenue;
            CalculateRevenueText(yesterdayRevenue);

            TopProducts = topProductsData;
            TopCategories = topCategoriesData;
            TopSellers = topSellersData;
        }

        private void CalculateRevenueText(int yesterdayRevenue)
        {
            if (yesterdayRevenue > 0)
            {
                var revenueDifferencePercentage = ((double)(TotalRevenue - yesterdayRevenue) / yesterdayRevenue) * 100;
                var revenueChangeText = revenueDifferencePercentage >= 0
                    ? $"+{revenueDifferencePercentage:N2}%"
                    : $"{revenueDifferencePercentage:N2}%";

                RevenueText = $"{TotalRevenue:N0} VND ({revenueChangeText})";
            }
            else
            {
                RevenueText = $"{TotalRevenue:N0} VND";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TopProduct
    {
        public string Name
        {
            get; set;
        }
        public int Revenue
        {
            get; set;
        }
    }

    public class TopCategory
    {
        public string Name
        {
            get; set;
        }
        public int Revenue
        {
            get; set;
        }
    }
}

public class TopSeller
{
    public string Name
    {
        get; set;
    }
    public int Amount
    {
        get; set;
    } // Có thể thêm thuộc tính Revenue nếu cần
}