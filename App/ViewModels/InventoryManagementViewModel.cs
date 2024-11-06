using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using App.Model;
using System.Linq;
using System;

namespace App.ViewModels
{
    public partial class InventoryManagementViewModel : ObservableRecipient
    {
        public ObservableCollection<Material> AllMaterials
        {
            get; private set;
        }
        public ObservableCollection<Material> FilteredMaterials
        {
            get; set;
        }

        private int currentPage;
        private const int itemsPerPage = 10;

        public int CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    UpdateCurrentPage();
                }
            }
        }

        public InventoryManagementViewModel()
        {
            AllMaterials = new ObservableCollection<Material>(App.GetService<IDao>().GetAllMaterials());
            FilteredMaterials = new ObservableCollection<Material>();
            currentPage = 0;
            UpdateCurrentPage();
        }

        public void UpdateCurrentPage()
        {
            FilteredMaterials.Clear();
            var items = AllMaterials.Skip(CurrentPage * itemsPerPage).Take(itemsPerPage).ToList();
            foreach (var item in items)
            {
                FilteredMaterials.Add(item);
            }
        }


        public int TotalPages()
        {
            return (int)Math.Ceiling((double)AllMaterials.Count / itemsPerPage);
        }

        public void SearchMaterials(string searchText, string selectedCategory, DateTime? startExpirationDate, DateTime? endExpirationDate)
        {
            var filtered = App.GetService<IDao>().GetAllMaterials().Where(m =>
                (string.IsNullOrEmpty(searchText) || m.MaterialName.ToLower().Contains(searchText)) &&
                (selectedCategory == "Tất cả" || string.IsNullOrEmpty(selectedCategory) || m.Category.Equals(selectedCategory)) &&
                (!startExpirationDate.HasValue || m.ExpirationDate >= startExpirationDate.Value) &&
                (!endExpirationDate.HasValue || m.ExpirationDate <= endExpirationDate.Value)).ToList();

            AllMaterials = new ObservableCollection<Material>(filtered);
            currentPage = 0;
            UpdateCurrentPage();
        }
    }
}
