using App.Services;
using App.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic; // Thêm namespace này để sử dụng List<T>

namespace App.Views;

public sealed partial class RevenuePage : Page
{
    public RevenueViewModel ViewModel
    {
        get; set;
    }
    private MockDao mockDao; // Thêm trường mockDao

    public RevenuePage()
    {
        this.InitializeComponent();
        ViewModel = new RevenueViewModel();
        this.DataContext = ViewModel;

        mockDao = new MockDao(); // Khởi tạo mockDao

        // Gọi hàm LoadRevenueData để tải dữ liệu khi trang được khởi tạo
        LoadData();
    }

    private async void LoadData()
    {
        // Sử dụng ngày hiện tại để tải dữ liệu doanh thu
        await ViewModel.LoadRevenueData(DateTime.Today);
        await LoadTopSellers(DateTime.Today); // Gọi LoadTopSellers ở đây
        await LoadTopProducts(DateTime.Today); // Gọi LoadTopProducts ở đây
        await LoadTopCategories(DateTime.Today); // Gọi LoadTopCategories ở đây
    }

    private async Task LoadTopProducts(DateTime selectedDate)
    {
        List<TopProduct> topProducts = await mockDao.GetTopProducts(selectedDate); // Sử dụng mockDao để gọi phương thức

        if (topProducts.Count >= 5)
        {
            topProduct1.Text = topProducts[0].Name;
            productRevenue1.Text = topProducts[0].Revenue.ToString("N0");

            topProduct2.Text = topProducts[1].Name;
            productRevenue2.Text = topProducts[1].Revenue.ToString("N0");

            topProduct3.Text = topProducts[2].Name;
            productRevenue3.Text = topProducts[2].Revenue.ToString("N0");

            topProduct4.Text = topProducts[3].Name;
            productRevenue4.Text = topProducts[3].Revenue.ToString("N0");

            topProduct5.Text = topProducts[4].Name;
            productRevenue5.Text = topProducts[4].Revenue.ToString("N0");
        }
    }

    private async Task LoadTopCategories(DateTime selectedDate)
    {
        List<TopCategory> topCategories = await mockDao.GetTopCategories(selectedDate); // Sử dụng mockDao để gọi phương thức

        if (topCategories.Count >= 5)
        {
            topBeverage1.Text = topCategories[0].Name;
            beverageRevenue1.Text = topCategories[0].Revenue.ToString("N0");

            topBeverage2.Text = topCategories[1].Name;
            beverageRevenue2.Text = topCategories[1].Revenue.ToString("N0");

            topBeverage3.Text = topCategories[2].Name;
            beverageRevenue3.Text = topCategories[2].Revenue.ToString("N0");

            topBeverage4.Text = topCategories[3].Name;
            beverageRevenue4.Text = topCategories[3].Revenue.ToString("N0");

            topBeverage5.Text = topCategories[4].Name;
            beverageRevenue5.Text = topCategories[4].Revenue.ToString("N0");
        }
    }

    private async Task LoadTopSellers(DateTime selectedDate)
    {
        List<TopSeller> TopSellers = await mockDao.GetTopSellers(selectedDate); // Sử dụng mockDao để gọi phương thức

        if (TopSellers.Count >= 5)
        {
            topAmountBeverage1.Text = TopSellers[0].Name;
            beverageAmount1.Text = TopSellers[0].Amount.ToString();

            topAmountBeverage2.Text = TopSellers[1].Name;
            beverageAmount2.Text = TopSellers[1].Amount.ToString();

            topAmountBeverage3.Text = TopSellers[2].Name;
            beverageAmount3.Text = TopSellers[2].Amount.ToString();

            topAmountBeverage4.Text = TopSellers[3].Name;
            beverageAmount4.Text = TopSellers[3].Amount.ToString();

            topAmountBeverage5.Text = TopSellers[4].Name;
            beverageAmount5.Text = TopSellers[4].Amount.ToString();
        }
    }
}