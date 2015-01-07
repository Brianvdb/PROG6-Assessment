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
    public class BrandVM : INotifyPropertyChanged
    {
        private Brand _brand;

        public bool IsNew { get; set; }

        public int Id
        {
            get
            {
                return _brand.Id;
            }
        }

        public string Name
        {
            get
            {
                return _brand.Name;
            }
            set
            {
                _brand.Name = value;
                OnPropertyChanged();
            }
        }

        public Brand Brand
        {
            get
            {
                return _brand;
            }
        }

        public BrandVM()
        {
            _brand = new Brand();
            IsNew = true;
        }

        public BrandVM(Brand brand)
        {
            _brand = brand;
            IsNew = false;
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
