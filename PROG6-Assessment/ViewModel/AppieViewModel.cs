using DomainModel.Interfaces;
using DomainModel.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace PROG6_Assessment.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AppieViewModel : ViewModelBase
    {
        private IRepository<Brand> brandRepository;
        private IRepository<Department> departmentRepository;
        private IRepository<Discount> discountRepository;
        private IRepository<Product> productRepository;
        private IRepository<ProductType> productTypeRepository;
        private IRepository<Recipe> recipeRepository;
        public ICommand OpenShoppingList { get; set; }

        public AppieViewModel(IRepository<Brand> brandRepo, IRepository<Department> departmentRepo, IRepository<Discount> discountRepo, IRepository<Product> productRepo, IRepository<ProductType> productTypeRepo, IRepository<Recipe> recipeRepo)
        {
            this.brandRepository = brandRepo;
            this.departmentRepository = departmentRepo;
            this.discountRepository = discountRepo;
            this.productRepository = productRepo;
            this.productTypeRepository = productTypeRepo;
            this.recipeRepository = recipeRepo;

            this.OpenShoppingList = new RelayCommand(OpenShoppingWindow);
        }

        private void OpenShoppingWindow()
        {
            ShoppingList.Instance.Show();
        }
    }
}