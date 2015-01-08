using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PROG6_Assessment.ViewModel
{
    public class RecipeVM : INotifyPropertyChanged
    {
        private Recipe _recipe;
        public int Id
        {
            get
            {
                return _recipe.Id;
            }
        }

        public string Name
        {
            get
            {
                return _recipe.Name;
            }
            set
            {
                _recipe.Name = value;
                OnPropertyChanged();
            }
        }

        public Recipe Recipe
        {
            get
            {
                return _recipe;
            }
        }

        public ObservableCollection<ProductVM> ProductVMList { get; set; }


        public RecipeVM(Recipe recipe)
        {
            _recipe = recipe;
            ProductVMList = new ObservableCollection<ProductVM>();
            foreach(Product p in recipe.Products) {
                ProductVMList.Add(new ProductVM(p));
            }
        }

        public RecipeVM(ObservableCollection<ProductVM> productList)
        {
            _recipe = new Recipe();
            ProductVMList = new ObservableCollection<ProductVM>();
            foreach (ProductVM p in productList)
            {
                ProductVMList.Add(new ProductVM(p.Product));
            }
            _recipe.Products = new List<Product>();
            ProductVMList.ToList().ForEach(p => _recipe.Products.Add(p.Product));
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
