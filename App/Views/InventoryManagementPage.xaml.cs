﻿using App.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using App.Model;
using System;
using System.Linq;
using ClosedXML.Excel; // Thêm thư viện này vào file
using System.IO;

namespace App.Views
{
    public sealed partial class InventoryManagementPage : Page
    {
        public InventoryManagementViewModel ViewModel
        {
            get;
        }

        public InventoryManagementPage()
        {
            this.InitializeComponent();
            ViewModel = new InventoryManagementViewModel();
            InventoryListView.ItemsSource = ViewModel.FilteredMaterials;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            DateTime? startExpirationDate = StartExpirationDatePicker.SelectedDate?.Date;
            DateTime? endExpirationDate = EndExpirationDatePicker.SelectedDate?.Date;

            ViewModel.SearchMaterials(searchText, startExpirationDate, endExpirationDate);
        }


        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            AddEditDialog.Title = "Thêm Nguyên Liệu";
            ImportDatePicker.Visibility = Visibility.Visible;
            await AddEditDialog.ShowAsync();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMaterial = (Material)InventoryListView.SelectedItem;
            if (selectedMaterial != null)
            {
                MaterialCodeTextBox.Text = selectedMaterial.MaterialCode;
                MaterialNameTextBox.Text = selectedMaterial.MaterialName;
                QuantityTextBox.Text = selectedMaterial.Quantity.ToString();
                CategoryTextBox.Text = selectedMaterial.Category;
                UnitTextBox.Text = selectedMaterial.Unit;
                UnitPriceTextBox.Text = selectedMaterial.UnitPrice.ToString();
                ImportDatePicker.Visibility = Visibility.Collapsed;
                ExpirationDatePicker.Date = new DateTimeOffset(selectedMaterial.ExpirationDate);

                AddEditDialog.Title = "Sửa Nguyên Liệu";
                await AddEditDialog.ShowAsync();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMaterial = (Material)InventoryListView.SelectedItem;
            if (selectedMaterial != null)
            {
                App.GetService<IDao>().DeleteMaterial(selectedMaterial.MaterialCode);
                ViewModel.AllMaterials.Remove(selectedMaterial);
                ViewModel.UpdateCurrentPage();
            }
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for handling detail view
            var selectedMaterial = (Material)InventoryListView.SelectedItem;
            if (selectedMaterial != null)
            {
                // Implement logic to display detailed information about the selected material
                ContentDialog detailDialog = new ContentDialog
                {
                    Title = "Chi tiết Nguyên Liệu",
                    Content = $"Mã: {selectedMaterial.MaterialCode}\n" +
                              $"Tên: {selectedMaterial.MaterialName}\n" +
                              $"Số lượng: {selectedMaterial.Quantity}\n" +
                              $"Phân loại: {selectedMaterial.Category}\n" +
                              $"Đơn vị: {selectedMaterial.Unit}\n" +
                              $"Đơn giá: {selectedMaterial.UnitPrice}\n" +
                              $"Ngày nhập: {selectedMaterial.FormattedImportDate}\n" +
                              $"Hạn sử dụng: {selectedMaterial.FormattedExpirationDate}",
                    CloseButtonText = "Đóng"
                };

                detailDialog.ShowAsync();
            }
        }

        private void AddEditDialogPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var newMaterial = new Material
            {
                MaterialCode = MaterialCodeTextBox.Text,
                MaterialName = MaterialNameTextBox.Text,
                Quantity = int.Parse(QuantityTextBox.Text),
                Category = CategoryTextBox.Text,
                Unit = UnitTextBox.Text,
                UnitPrice = double.Parse(UnitPriceTextBox.Text),
                ExpirationDate = ExpirationDatePicker.Date.DateTime
            };

            if (AddEditDialog.Title == "Thêm Nguyên Liệu")
            {
                newMaterial.ImportDate = DateTime.Now;
                App.GetService<IDao>().AddMaterial(newMaterial);
                ViewModel.AllMaterials.Add(newMaterial);
            }
            else if (AddEditDialog.Title == "Sửa Nguyên Liệu")
            {
                var existingMaterial = ViewModel.AllMaterials.FirstOrDefault(m => m.MaterialCode == newMaterial.MaterialCode);
                if (existingMaterial != null)
                {
                    existingMaterial.MaterialName = newMaterial.MaterialName;
                    existingMaterial.Quantity = newMaterial.Quantity;
                    existingMaterial.Category = newMaterial.Category;
                    existingMaterial.Unit = newMaterial.Unit;
                    existingMaterial.UnitPrice = newMaterial.UnitPrice;
                    existingMaterial.ExpirationDate = newMaterial.ExpirationDate;
                }
            }

            ViewModel.UpdateCurrentPage();
        }

        private void ClearInputFields()
        {
            MaterialCodeTextBox.Text = string.Empty;
            MaterialNameTextBox.Text = string.Empty;
            QuantityTextBox.Text = string.Empty;
            CategoryTextBox.Text = string.Empty;
            UnitTextBox.Text = string.Empty;
            UnitPriceTextBox.Text = string.Empty;
            ImportDatePicker.Date = DateTimeOffset.Now;
            ExpirationDatePicker.Date = DateTimeOffset.Now.AddMonths(1);
        }
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentPage > 0)
            {
                ViewModel.CurrentPage--;
                ViewModel.UpdateCurrentPage();
                PageInfoTextBlock.Text = $"Trang {ViewModel.CurrentPage + 1} ";
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentPage < ViewModel.TotalPages() - 1)
            {
                ViewModel.CurrentPage++;
                ViewModel.UpdateCurrentPage();
                PageInfoTextBlock.Text = $"Trang {ViewModel.CurrentPage + 1} ";
            }
        }

        private async void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Bắt đầu xuất file Excel...");

                await Task.Run(() =>
                {
                    try
                    {
                        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InventoryData.xlsx");

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Inventory");

                            // Tạo tiêu đề cột
                            worksheet.Cell(1, 1).Value = "Mã nguyên liệu";
                            worksheet.Cell(1, 2).Value = "Tên nguyên liệu";
                            worksheet.Cell(1, 3).Value = "Số lượng";
                            worksheet.Cell(1, 4).Value = "Phân loại";
                            worksheet.Cell(1, 5).Value = "Đơn vị tính";
                            worksheet.Cell(1, 6).Value = "Đơn giá";
                            worksheet.Cell(1, 7).Value = "Ngày nhập";
                            worksheet.Cell(1, 8).Value = "Hạn sử dụng";

                            int row = 2;
                            foreach (var material in ViewModel.FilteredMaterials)
                            {
                                worksheet.Cell(row, 1).Value = material.MaterialCode;
                                worksheet.Cell(row, 2).Value = material.MaterialName;
                                worksheet.Cell(row, 3).Value = material.Quantity;
                                worksheet.Cell(row, 4).Value = material.Category;
                                worksheet.Cell(row, 5).Value = material.Unit;
                                worksheet.Cell(row, 6).Value = material.UnitPrice;
                                worksheet.Cell(row, 7).Value = material.ImportDate.ToString("dd/MM/yyyy");
                                worksheet.Cell(row, 8).Value = material.ExpirationDate.ToString("dd/MM/yyyy");
                                row++;
                            }

                            var range = worksheet.RangeUsed();
                            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            range.Style.Font.Bold = false;

                            workbook.SaveAs(filePath);
                        }

                        System.Diagnostics.Debug.WriteLine("Xuất file Excel thành công.");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi khi ghi file: {ex.Message}");
                        throw; // Ném lại ngoại lệ để xử lý bên ngoài
                    }
                });

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Xuất Excel thành công",
                        Content = $"File Excel đã được lưu tại {Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}",
                        CloseButtonText = "Đóng"
                    };
                    await dialog.ShowAsync();
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi không mong muốn: {ex.Message}");
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Lỗi xuất file Excel",
                        Content = $"Đã xảy ra lỗi khi xuất file Excel: {ex.Message}",
                        CloseButtonText = "Đóng"
                    };
                    await errorDialog.ShowAsync();
                });
            }
        }



        private void InventoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Logic to handle selection change
        }
    }
}
