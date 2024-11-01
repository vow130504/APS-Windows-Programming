using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerinatManagement.Model;
public class Product :INotifyPropertyChanged
{
    public string Name
    {
        get; set;
    }
    public int Price
    {
        get; set;
    }
    public string Image
    {
        get; set;
    }
    public string Size
    {
        get; set;
    }
    public event PropertyChangedEventHandler PropertyChanged;
}

