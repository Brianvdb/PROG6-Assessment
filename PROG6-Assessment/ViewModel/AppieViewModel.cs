using DomainModel.Interfaces;
using DomainModel.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
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
        // repositories
        private IRepository<Brand> brandRepository;
        private IRepository<Department> departmentRepository;
        private IRepository<Discount> discountRepository;
        private IRepository<Product> productRepository;
        private IRepository<ProductType> productTypeRepository;
        private IRepository<Recipe> recipeRepository;

        // commands
        public ICommand OpenShoppingList { get; set; }
        public ICommand OpenBrandList { get; set; }
        public ICommand OpenProductList { get; set; }
        public ICommand OpenDepartmentList { get; set; }
        public ICommand OpenRecipeList { get; set; }
        public ICommand OpenDiscountList { get; set; }
        public ICommand OpenProductTypeList { get; set; }
        public ICommand OpenProductTypeEdit { get; set; }
        public ICommand OpenProductTypeEditAdd { get; set; }
        public ICommand SaveProductTypeCommand { get; set; }
        public ICommand DeleteProductTypeCommand { get; set; }

        // view models
        private ProductTypeVM _productType;
        public ProductTypeVM CurrentProductType
        {
            get
            {
                return _productType;
            }
            set
            {
                _productType = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProductTypeVM> ProductTypeVMList { get; set; }

        public AppieViewModel(IRepository<Brand> brandRepo, IRepository<Department> departmentRepo, IRepository<Discount> discountRepo, IRepository<Product> productRepo, IRepository<ProductType> productTypeRepo, IRepository<Recipe> recipeRepo)
        {
            //setup sql model data
            this.brandRepository = brandRepo;
            this.departmentRepository = departmentRepo;
            this.discountRepository = discountRepo;
            this.productRepository = productRepo;
            this.productTypeRepository = productTypeRepo;
            this.recipeRepository = recipeRepo;

            //bind actions to buttons
            this.OpenShoppingList = new RelayCommand(OpenShoppingWindow);
            this.OpenBrandList = new RelayCommand(OpenBrandWindow);
            this.OpenProductList = new RelayCommand(OpenProductWindow);
            this.OpenRecipeList = new RelayCommand(OpenRecipeWindow);
            this.OpenDepartmentList = new RelayCommand(OpenDepartmentWindow);
            this.OpenDiscountList = new RelayCommand(OpenDiscountWindow);
            this.OpenProductTypeList = new RelayCommand(OpenProductTypeWindow);
            this.OpenProductTypeEdit = new RelayCommand(OpenProductTypeEditWindow);
            this.OpenProductTypeEditAdd = new RelayCommand(OpenProductTypeListAddWindow);
            this.SaveProductTypeCommand = new RelayCommand<string>(name => SaveProductType(name));
            this.DeleteProductTypeCommand = new RelayCommand(DeleteProductType);

            // observable collections
            this.ProductTypeVMList = new ObservableCollection<ProductTypeVM>();

            this.productTypeRepository.GetAll().ForEach(p => ProductTypeVMList.Add(new ProductTypeVM(p)));

            System.Diagnostics.Trace.WriteLine("PRODUCT TYPES: " + ProductTypeVMList.Count);
        }

        private void OpenShoppingWindow()
        {
            ShoppingList.Instance.Show();
        }

        private void OpenBrandWindow()
        {
            BrandList.Instance.Show();
        }

        private void OpenProductWindow()
        {
            Products.Instance.Show();
        }

        private void OpenRecipeWindow()
        {
            RecipeList.Instance.Show();
        }

        private void OpenDepartmentWindow()
        {
            DepartmentList.Instance.Show();
        }

        private void OpenDiscountWindow()
        {
            DiscountList.Instance.Show();
        }

        private void OpenProductTypeWindow()
        {
            ProductTypeList.Instance.Show();
        }

        private void OpenProductTypeListAddWindow()
        {
            CurrentProductType = new ProductTypeVM();
            OpenProductTypeEditWindow();
        }

        private void SaveProductType(string name)
        {
            CurrentProductType.Name = name;
            if (CurrentProductType.IsNew)
            {
                this.productTypeRepository.Add(CurrentProductType.ProductType);
                ProductTypeVMList.Add(CurrentProductType);
            }
            else
            {
                this.productTypeRepository.Update(); 
            }
            ProductTypeEdit.Instance.Close();
            OpenProductTypeWindow();
        }

        private void DeleteProductType()
        {
            if (!CurrentProductType.IsNew)
            {
                this.productTypeRepository.Delete(CurrentProductType.ProductType);
                ProductTypeVMList.Remove(CurrentProductType);
                CurrentProductType = null;
            }
        }

        private void OpenProductTypeEditWindow()
        {
            if (CurrentProductType == null)
            {
                return;
            }
            ProductTypeList.Instance.Close();
            ProductTypeEdit.Instance.Show();
        }
    }
}