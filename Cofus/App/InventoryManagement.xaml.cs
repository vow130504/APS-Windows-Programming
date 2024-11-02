using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

using System.Collections.ObjectModel;


// Namespace and class declarations
namespace App
{
    public sealed partial class InventoryManagement : Page
    {
        // ObservableCollection for data binding to ListView
        public ObservableCollection<Material> Materials { get; set; }
        public ObservableCollection<Material> FilteredMaterials { get; set; }

        public InventoryManagement()
        {
            this.InitializeComponent();
            Materials = new ObservableCollection<Material>
{
                new Material { MaterialCode = "C001", MaterialName = "Cafe Arabica", Quantity = 100, Category = "Cafe", Unit = "Kg", UnitPrice = 150000, ImportDate = new DateTime(2024, 1, 15), ExpirationDate = new DateTime(2024, 3, 1) },
                new Material { MaterialCode = "S001", MaterialName = "Sữa tươi", Quantity = 50, Category = "Sữa", Unit = "Lít", UnitPrice = 20000, ImportDate = new DateTime(2024, 1, 20),ExpirationDate = new DateTime(2024, 2, 28) },
                new Material { MaterialCode = "S002", MaterialName = "Sữa đặc", Quantity = 50, Category = "Sữa", Unit = "Lon", UnitPrice = 20000, ImportDate = new DateTime(2024, 1, 22), ExpirationDate = new DateTime(2024, 4, 5) }
            };


            FilteredMaterials = new ObservableCollection<Material>(Materials);
            // Binding
            InventoryListView.ItemsSource = FilteredMaterials;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin tìm kiếm
            string searchText = SearchBox.Text.ToLower();
            string selectedCategory = (CategoryFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            DateTime? startExpirationDate = StartExpirationDatePicker.SelectedDate?.Date;
            DateTime? endExpirationDate = EndExpirationDatePicker.SelectedDate?.Date;

            // Lọc sản phẩm dựa trên hạn sử dụng và các tiêu chí khác
            var filtered = Materials.Where(m =>
                (string.IsNullOrEmpty(searchText) || m.MaterialName.ToLower().Contains(searchText)) &&
                (selectedCategory == "Tất cả" || string.IsNullOrEmpty(selectedCategory) || m.Category == selectedCategory) &&
                (!startExpirationDate.HasValue || m.ExpirationDate >= startExpirationDate.Value) &&
                (!endExpirationDate.HasValue || m.ExpirationDate <= endExpirationDate.Value)).ToList();

            // Cập nhật danh sách sản phẩm sau khi lọc
            FilteredMaterials.Clear();
            foreach (var material in filtered)
            {
                FilteredMaterials.Add(material);
            }
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for adding a new material
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for editing a selected material
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for deleting a selected material
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for viewing details of a selected material
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for exporting data to Excel
        }
    }

    // Class representing a material item
    public class Material
    {
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public DateTime ImportDate { get; set; }

        public DateTime ExpirationDate { get; set; } // New property for Expiration Date

        // Properties to format dates as strings
        public string FormattedImportDate => ImportDate.ToString("dd/MM/yyyy");

        public string FormattedExpirationDate => ExpirationDate.ToString("dd/MM/yyyy"); // New formatted date
    }

}

