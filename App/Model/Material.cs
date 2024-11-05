using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model;


// Class representing a material item
public class Material
{
    public string MaterialCode
    {
        get; set;
    }
    public string MaterialName
    {
        get; set;
    }
    public int Quantity
    {
        get; set;
    }
    public string Category
    {
        get; set;
    }
    public string Unit
    {
        get; set;
    }
    public double UnitPrice
    {
        get; set;
    }
    public DateTime ImportDate
    {
        get; set;
    }
    public DateTime ExpirationDate
    {
        get; set;
    } // New property for Expiration Date

    // Properties to format dates as strings
    public string FormattedImportDate => ImportDate.ToString("dd/MM/yyyy");
    public string FormattedExpirationDate => ExpirationDate.ToString("dd/MM/yyyy"); // New formatted date
}
