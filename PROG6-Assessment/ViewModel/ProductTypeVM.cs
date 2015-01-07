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
    public class ProductTypeVM : INotifyPropertyChanged
    {
        private ProductType _productType;

        public bool IsNew { get; set; }

        public int Id
        {
            get
            {
                return _productType.Id;
            }
        }

        public string Name
        {
            get
            {
                return _productType.Name;
            }
            set
            {
                _productType.Name = value;
                OnPropertyChanged();
            }
        }

        public ProductType ProductType
        {
            get
            {
                return _productType;
            }
        }



        public ProductTypeVM(ProductType type)
        {
            _productType = type;
            IsNew = false;
        }

        public ProductTypeVM()
        {
            _productType = new ProductType();
            IsNew = true;
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
