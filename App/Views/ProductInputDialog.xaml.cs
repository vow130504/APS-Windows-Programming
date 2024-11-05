﻿using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace App.Views;

public sealed partial class ProductInputDialog : ContentDialog
{
    public ProductInputDialog()
    {
        this.InitializeComponent();
    }

    public int Quantity
    {
        get
        {
            int.TryParse(quantityTextBox.Text, out int quantity);
            return quantity;
        }
    }

    public string Notes
    {
        get => notesTextBox.Text;
    }

    public string SelectedSize
    {
        get
        {
            var selectedItem = sizeComboBox.SelectedItem as ComboBoxItem;
            return selectedItem?.Content.ToString();
        }
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        // Kiểm tra số lượng có hợp lệ không
        if (!int.TryParse(quantityTextBox.Text, out int quantity) || quantity <= 0)
        {
            args.Cancel = true; // Ngăn không cho đóng dialog

            // Hiển thị thông báo lỗi
            var errorDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = "Vui lòng nhập số lượng hợp lệ (số nguyên dương).",
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot // Đảm bảo hiển thị đúng
            };
            _ = errorDialog.ShowAsync();
        }
    }

    private void QuantityTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
    {
        // Chỉ cho phép nhập số
        args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
    }
}
