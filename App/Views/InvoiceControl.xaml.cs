using App.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using PropertyChanged;
using App.ViewModels;

namespace App.Views;

[AddINotifyPropertyChangedInterface]
public sealed partial class InvoiceControl : UserControl
{
    public InvoiceControlViewModel ViewModel
    {
        get;
    }
    public InvoiceControl()
    {
        this.InitializeComponent();

        ViewModel = new InvoiceControlViewModel();
    }

    private void CheckoutButton_Click(object sender, RoutedEventArgs e)
    {
        // Xử lý logic thanh toán ở đây
    }
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        // Kiểm tra xem nút đã được bấm có thuộc một sản phẩm nào
        if (sender is Button deleteButton && deleteButton.DataContext is InvoiceItem itemToDelete)
        {
            ViewModel.Invoice.RemoveItem(itemToDelete);
        }
    }
}
