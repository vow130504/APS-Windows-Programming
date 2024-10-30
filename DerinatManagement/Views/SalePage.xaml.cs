using System;
using System.Collections.Generic;
using System.IO;
using DerinatManagement.Model;
using DerinatManagement.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.Devices.Enumeration;
using DerinatManagement.Views;
using Microsoft.UI.Xaml.Media;


namespace DerinatManagement.Views;

public sealed partial class SalePage : Page
{
    public SalePageViewModel ViewModel
    {
        get;
    }
    public SalePage()
    {
        this.InitializeComponent();
        ViewModel = new SalePageViewModel();
    }

    public void TabView_Loaded(object sender, RoutedEventArgs e)
    {
        (sender as TabView).TabItems.Add(CreateNewTab(1));
    }

    public void TabView_AddButtonClick(TabView sender, object args)
    {
        TabViewItem newInvoice = CreateNewTab(sender.TabItems.Count);
        sender.TabItems.Add(newInvoice);
        sender.SelectedItem = newInvoice;
    }

    public void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        sender.TabItems.Remove(args.Tab);
    }

    public TabViewItem CreateNewTab(int index)
    {
        // Tạo Tab mới cho hóa đơn
        TabViewItem newTab = new TabViewItem
        {
            Header = $"Hóa đơn {index}",
            Content = new InvoiceControl(),
            Background = (SolidColorBrush)Application.Current.Resources["TabViewItemHeaderBackground"],
            Foreground = (SolidColorBrush)Application.Current.Resources["TabViewItemHeaderForeground"]
        };
        return newTab;
    }

    private void ViewCategory_Click(object sender, RoutedEventArgs e)
    {
        ContentCenter.Children.Clear();
        CategoryControl categoryView = new CategoryControl(this);
        ContentCenter.Children.Add(categoryView);
    }
    private void PendingOrder_Click(object sender, RoutedEventArgs e)
    {
        ContentCenter.Children.Clear();
        PendingControl pendingControl = new PendingControl();
        ContentCenter.Children.Add(pendingControl);
    }
}
