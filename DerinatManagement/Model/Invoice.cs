using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PropertyChanged;

namespace DerinatManagement.Model;

[AddINotifyPropertyChangedInterface]
public class Invoice:INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public int InvoiceNumber
    {
        get;set;
    }
    public DateTime CreatedTime
    {
        get;set;
    }
    public int TableNumber
    {
        get; set;
    }
    public FullObservableCollection<InvoiceItem> InvoiceItems
    {
        get; set;
    }

    public Invoice()
    {
        

        // Đăng ký sự kiện CollectionChanged để cập nhật tổng số lượng và tổng giá khi danh sách thay đổi
       // InvoiceItems.CollectionChanged += (s, e) => OnInvoiceItemsChanged(e);
    }

    public void OnInvoiceItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Khi có các mục mới được thêm vào, đăng ký sự kiện PropertyChanged cho chúng
        if (e.NewItems != null)
        {
            foreach (InvoiceItem item in e.NewItems)
            {
                item.PropertyChanged += (s, e) => UpdateTotals();
            }
        }

        // Khi có các mục cũ bị xóa, hủy đăng ký sự kiện PropertyChanged
        if (e.OldItems != null)
        {
            foreach (InvoiceItem item in e.OldItems)
            {
                item.PropertyChanged -= (s, e) => UpdateTotals();
            }
        }

        // Cập nhật tổng số lượng và tổng tiền
        UpdateTotals();
    }
    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // Check if the changed property is "Quantity" or "Total"
        if (e.PropertyName == nameof(InvoiceItem.Quantity) || e.PropertyName == nameof(InvoiceItem.Total))
        {
            // Update totals when the Quantity or Total property changes
            UpdateTotals();
        }
    }

    public void UpdateTotals()
    {
        OnPropertyChanged(nameof(TotalQuantity));
        OnPropertyChanged(nameof(TotalPrice));
    }

    // Phương thức để tính tổng số lượng
    public int TotalQuantity => InvoiceItems.Sum(item => item.Quantity);

    // Phương thức để tính tổng giá
    public decimal TotalPrice => InvoiceItems.Sum(item => item.Total);

    // Phương thức để thêm sản phẩm vào hóa đơn
    public void AddItem(InvoiceItem item)
    {
        // Đăng ký sự kiện PropertyChanged để cập nhật khi sản phẩm được thay đổi
        item.PropertyChanged += (s, e) => UpdateTotals();
        InvoiceItems.Add(item);
    }

    // Phương thức để xóa sản phẩm khỏi hóa đơn
    public void RemoveItem(InvoiceItem item)
    {
        // Hủy đăng ký sự kiện PropertyChanged trước khi xóa
        item.PropertyChanged -= (s, e) => UpdateTotals();
        InvoiceItems.Remove(item);
    }

    // Phương thức để đánh dấu hóa đơn đã thanh toán
    public void MarkAsPaid()
    {
        // Implementation for marking the invoice as paid
    }

    //// Tạo số hóa đơn duy nhất
    //private static string GenerateUniqueInvoiceNumber()
    //{
    //    return Guid.NewGuid().ToString();
    //}
    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
