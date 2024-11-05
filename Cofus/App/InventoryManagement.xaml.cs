using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

// Namespace and class declarations
namespace App
{
    public sealed partial class InventoryManagement : Page
    {
        // DAO instance
        private ICoffeeShopDAO _dao;
        private ObservableCollection<Material> AllMaterials;
        public ObservableCollection<Material> FilteredMaterials { get; set; }
        private int currentPage;
        private const int itemsPerPage = 10;

        public InventoryManagement()
        {
            this.InitializeComponent();
            _dao = new MockCoffeeShopDAO();

            // Lấy tất cả dữ liệu từ DAO
            AllMaterials = new ObservableCollection<Material>(_dao.GetAllMaterials());
            FilteredMaterials = new ObservableCollection<Material>();

            currentPage = 0;
            UpdateCurrentPage();

            InventoryListView.ItemsSource = FilteredMaterials;
        }

        private void UpdateCurrentPage()
        {
            FilteredMaterials.Clear();
            var items = AllMaterials.Skip(currentPage * itemsPerPage).Take(itemsPerPage).ToList();
            foreach (var item in items)
            {
                FilteredMaterials.Add(item);
            }

            // Cập nhật thông tin trang
            PageInfoTextBlock.Text = $"Trang {currentPage + 1} / {TotalPages()}";
        }

        private int TotalPages()
        {
            return (int)Math.Ceiling((double)AllMaterials.Count / itemsPerPage);
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                UpdateCurrentPage();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < TotalPages() - 1)
            {
                currentPage++;
                UpdateCurrentPage();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin tìm kiếm
            string searchText = SearchBox.Text.ToLower();
            string selectedCategory = (CategoryFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            DateTime? startExpirationDate = StartExpirationDatePicker.SelectedDate?.Date;
            DateTime? endExpirationDate = EndExpirationDatePicker.SelectedDate?.Date;

            // Lọc sản phẩm dựa trên hạn sử dụng và các tiêu chí khác
            var filtered = _dao.GetAllMaterials().Where(m =>
                (string.IsNullOrEmpty(searchText) || m.MaterialName.ToLower().Contains(searchText)) &&
                (selectedCategory == "Tất cả" || string.IsNullOrEmpty(selectedCategory) || m.Category.Equals(selectedCategory)) &&
                (!startExpirationDate.HasValue || m.ExpirationDate >= startExpirationDate.Value) &&
                (!endExpirationDate.HasValue || m.ExpirationDate <= endExpirationDate.Value)).ToList();

            // Cập nhật danh sách sản phẩm sau khi lọc
            AllMaterials = new ObservableCollection<Material>(filtered);
            currentPage = 0; // Reset to the first page
            UpdateCurrentPage();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại thêm sản phẩm
            ClearInputFields();
            AddEditDialog.Title = "Thêm Nguyên Liệu";
            ImportDatePicker.Visibility = Visibility.Visible; // Hiển thị trường ngày nhập khi thêm
            await AddEditDialog.ShowAsync();  // Await the dialog display
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMaterial = (Material)InventoryListView.SelectedItem;
            if (selectedMaterial != null)
            {
                // Hiển thị thông tin hiện tại của sản phẩm
                MaterialCodeTextBox.Text = selectedMaterial.MaterialCode;
                MaterialNameTextBox.Text = selectedMaterial.MaterialName;
                QuantityTextBox.Text = selectedMaterial.Quantity.ToString();
                CategoryTextBox.Text = selectedMaterial.Category;
                UnitTextBox.Text = selectedMaterial.Unit;
                UnitPriceTextBox.Text = selectedMaterial.UnitPrice.ToString();
                ImportDatePicker.Visibility = Visibility.Collapsed; // Ẩn trường ngày nhập khi sửa
                ExpirationDatePicker.Date = new DateTimeOffset(selectedMaterial.ExpirationDate);

                AddEditDialog.Title = "Sửa Nguyên Liệu";
                await AddEditDialog.ShowAsync();  // Await the dialog display
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMaterial = (Material)InventoryListView.SelectedItem;
            if (selectedMaterial != null)
            {
                _dao.DeleteMaterial(selectedMaterial.MaterialCode);
                AllMaterials.Remove(selectedMaterial);
                UpdateCurrentPage();
            }
        }

        private void AddEditDialogPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Lấy thông tin từ hộp thoại
            var newMaterial = new Material
            {
                MaterialCode = MaterialCodeTextBox.Text,
                MaterialName = MaterialNameTextBox.Text,
                Quantity = int.Parse(QuantityTextBox.Text),
                Category = CategoryTextBox.Text,
                Unit = UnitTextBox.Text,
                UnitPrice = double.Parse(UnitPriceTextBox.Text),
                ExpirationDate = ExpirationDatePicker.Date.DateTime // Luôn cập nhật ngày hết hạn
            };

            if (AddEditDialog.Title == "Thêm Nguyên Liệu")
            {
                newMaterial.ImportDate = DateTime.Now; // Gán ngày hiện tại cho ngày nhập
                _dao.AddMaterial(newMaterial);
                AllMaterials.Add(newMaterial);
            }
            else if (AddEditDialog.Title == "Sửa Nguyên Liệu")
            {
                var existingMaterial = AllMaterials.FirstOrDefault(m => m.MaterialCode == newMaterial.MaterialCode);
                if (existingMaterial != null)
                {
                    existingMaterial.MaterialName = newMaterial.MaterialName;
                    existingMaterial.Quantity = newMaterial.Quantity;
                    existingMaterial.Category = newMaterial.Category;
                    existingMaterial.Unit = newMaterial.Unit;
                    existingMaterial.UnitPrice = newMaterial.UnitPrice;
                    existingMaterial.ExpirationDate = newMaterial.ExpirationDate;
                    // Không cập nhật ImportDate
                }
            }

            UpdateCurrentPage(); // Cập nhật lại danh sách
        }

        private void ClearInputFields()
        {
            MaterialCodeTextBox.Text = "";
            MaterialNameTextBox.Text = "";
            QuantityTextBox.Text = "";
            CategoryTextBox.Text = "";
            UnitTextBox.Text = "";
            UnitPriceTextBox.Text = "";
            ImportDatePicker.Date = DateTimeOffset.Now; // Gán ngày hiện tại cho ngày nhập khi thêm
            ExpirationDatePicker.Date = DateTimeOffset.Now.AddMonths(1);
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for viewing details of a selected material
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic for exporting data to Excel
        }

        // Xử lý sự kiện khi chọn một dòng trong ListView
        private void InventoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Logic to handle selection change if needed
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
