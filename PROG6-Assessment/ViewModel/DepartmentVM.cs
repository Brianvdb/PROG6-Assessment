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
    public class DepartmentVM : INotifyPropertyChanged
    {
        private Department _department;

        public bool IsNew { get; set; }

        public int Id
        {
            get
            {
                return _department.Id;
            }
        }

        public string Name
        {
            get
            {
                return _department.Name;
            }
            set
            {
                _department.Name = value;
                OnPropertyChanged();
            }
        }

        public Department Department
        {
            get
            {
                return _department;
            }
        }

        public DepartmentVM()
        {
            _department = new Department();
            IsNew = true;
        }

        public DepartmentVM(Department department)
        {
            _department = department;
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
