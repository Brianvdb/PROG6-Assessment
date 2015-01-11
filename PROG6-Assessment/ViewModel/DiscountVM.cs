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
    public class DiscountVM : INotifyPropertyChanged
    {
        private Discount _discount;

        public bool IsNew { get; set; }

        public int Id
        {
            get
            {
                return _discount.Id;
            }
        }

        public string CouponCode
        {
            get
            {
                return _discount.CouponCode;
            }
            set
            {
                _discount.CouponCode= value;
                OnPropertyChanged();
            }
        }

        public double DiscountPercentage
        {
            get
            {
                return _discount.DiscountPercentage;
            }
            set
            {
                _discount.DiscountPercentage = value;
                OnPropertyChanged();
            }
        }

        public DateTime DiscountStartDate
        {
            get
            {
                return _discount.StartDate;
            }
            set
            {
                _discount.StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DiscountEndDate
        {
            get
            {
                return _discount.EndDate;
            }
            set
            {
                _discount.EndDate = Convert.ToDateTime(value);
                OnPropertyChanged();
            }
        }

        public Discount Discount
        {
            get
            {
                return _discount;
            }
        }

        public DiscountVM()
        {
            _discount = new Discount();
            IsNew = true;
        }

        public DiscountVM(Discount discount)
        {
            _discount = discount;
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
        /*
        private string transformToDatePicker(string normalDate)
        {
            String dd = normalDate.Substring(8,2);
            String MM = normalDate.Substring(5,2);
            String yyyy = normalDate.Substring(0,4);
            return yyyy + "-" + dd + "-" + MM;
        }*/
    }
}
