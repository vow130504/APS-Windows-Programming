using DerinatManagement.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using PropertyChanged;
using DerinatManagement.ViewModels;

namespace DerinatManagement.Views;

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
}
