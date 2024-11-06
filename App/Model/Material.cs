﻿using System;
using System.ComponentModel;

namespace App.Model
{
    public class Material : INotifyPropertyChanged
    {
        private string materialCode;
        private string materialName;
        private int quantity;
        private string category;
        private string unit;
        private double unitPrice;
        private DateTime importDate;
        private DateTime expirationDate;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string MaterialCode
        {
            get => materialCode;
            set
            {
                if (materialCode != value)
                {
                    materialCode = value;
                    OnPropertyChanged(nameof(MaterialCode));
                }
            }
        }

        public string MaterialName
        {
            get => materialName;
            set
            {
                if (materialName != value)
                {
                    materialName = value;
                    OnPropertyChanged(nameof(MaterialName));
                }
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public string Category
        {
            get => category;
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public string Unit
        {
            get => unit;
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged(nameof(Unit));
                }
            }
        }

        public double UnitPrice
        {
            get => unitPrice;
            set
            {
                if (unitPrice != value)
                {
                    unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                }
            }
        }

        public DateTime ImportDate
        {
            get => importDate;
            set
            {
                if (importDate != value)
                {
                    importDate = value;
                    OnPropertyChanged(nameof(ImportDate));
                }
            }
        }

        public DateTime ExpirationDate
        {
            get => expirationDate;
            set
            {
                if (expirationDate != value)
                {
                    expirationDate = value;
                    OnPropertyChanged(nameof(ExpirationDate));
                }
            }
        }

        public string FormattedImportDate => ImportDate.ToString("dd/MM/yyyy");
        public string FormattedExpirationDate => ExpirationDate.ToString("dd/MM/yyyy");
    }
}
