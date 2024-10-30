using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerinatManagement.Model;

public class Category :INotifyPropertyChanged
{
    public FullObservableCollection<Product> Products
    {
        get; set;
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
