using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.ViewModel
{
    public class ProductVM : INotifyPropertyChanged
    {
        private Product _product;
        private int _amount;

        public bool IsNew { get; set; }

        public int Id
        {
            get
            {
                return _product.Id;
            }
        }

        public Brand Brand
        {
            get
            {
                return _product.Brand;
            }
            set
            {
                _product.Brand = value;
                OnPropertyChanged();
            }
        }

        public Department Department
        {
            get
            {
                return _product.Department;
            }
            set
            {
                _product.Department = value;
                OnPropertyChanged();
            }
        }

        public ProductType ProductType
        {
            get
            {
                return _product.ProductType;
            }
            set
            {
                _product.ProductType = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get
            {
                return _product.ProductName;
            }
            set
            {
                _product.ProductName = value;
                OnPropertyChanged();
            }
        }

        public double Price
        {
            get
            {
                return _product.Price;
            }
            set
            {
                _product.Price = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalPrice");
                OnPropertyChanged("TotalPriceWithDiscount");
            }
        }

        public double TotalPrice
        {
            get
            {
                return _amount * _product.Price;
            }
        }

        public double TotalPriceWithDiscount
        {
            get
            {
                double discount = DiscountPercentage;
                if (discount == -1)
                {
                    return TotalPrice;
                }
                return Math.Round((TotalPrice * (100-discount)) / 100D, 2);
            }
        }

        public double DiscountPercentage
        {
            get
            {
                foreach (Discount discount in _product.Discounts)
                {
                    if (discount.StartDate <= DateTime.Today && (discount.EndDate == null || discount.EndDate > DateTime.Today))
                    {
                        return discount.DiscountPercentage;
                    }
                }
                return -1;
            }
        
        }

        public string CurrentDiscount
        { 
            get
            {
                foreach(Discount discount in _product.Discounts) {
                    if (discount.StartDate <= DateTime.Today && (discount.EndDate == null || discount.EndDate > DateTime.Today))
                    {
                        return discount.DiscountPercentage + "%";
                    }
                }
                return "Geen";
            }
        }


        public Product Product { get { return _product; } }



        public ProductVM()
        {
            _product = new Product();
            _product.Discounts = new List<Discount>();
            Amount = 1;
            IsNew = true;
        }

        public ProductVM(Product product)
        {
            _product = product;
            Amount = 1;
            IsNew = false;
        }

        public void DiscountChanged()
        {
            OnPropertyChanged("CurrentDiscount");
            OnPropertyChanged("DiscountPercentage");
            OnPropertyChanged("TotalPriceWithDiscount");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
