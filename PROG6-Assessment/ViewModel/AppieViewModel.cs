using DomainModel.Interfaces;
using DomainModel.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public ICommand AddToShoppingListCommand { get; set; }
        public ICommand AddMoreToShoppingListCommand { get; set; }
        public ICommand AddLessToShoppingListCommand { get; set; }
        public ICommand ClearShoppingListCommand { get; set; }
        public ICommand SaveBrandCommand { get; set; }
        public ICommand OpenBrandEdit { get; set; }
        public ICommand OpenBrandEditAdd { get; set; }
        public ICommand DeleteBrandCommand { get; set; }

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

        private ProductVM _currentListProduct;
        public ProductVM CurrentListProduct
        {
            get
            {
                return _currentListProduct;
            }
            set
            {
                _currentListProduct = value;
                RaisePropertyChanged();
            }
        }

        private ProductVM _currentShopListProduct;
        public ProductVM CurrentShopListProduct
        {
            get
            {
                return _currentShopListProduct;
            }
            set
            {
                _currentShopListProduct = value;
                RaisePropertyChanged();
            }
        }

        private BrandVM _brand;
        public BrandVM CurrentBrand
        {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
                RaisePropertyChanged();
            }
        }

        public double ShoppingListPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (ProductVM product in ProductShoppingVMList)
                {
                    totalPrice += product.TotalPriceWithDiscount;
                }
                return totalPrice;
            }
        }

        public ObservableCollection<ProductTypeVM> ProductTypeVMList { get; set; }
        public ObservableCollection<ProductVM> ProductVMList { get; set; }
        public ObservableCollection<ProductVM> ProductShoppingVMList { get; set; }
        public ObservableCollection<BrandVM> BrandVMList { get; set; }

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
            this.AddToShoppingListCommand = new RelayCommand(AddProductToShoppingList);
            this.AddMoreToShoppingListCommand = new RelayCommand(AddMoreToShoppingList);
            this.AddLessToShoppingListCommand = new RelayCommand(AddLessToShoppingList);
            this.ClearShoppingListCommand = new RelayCommand(ClearShoppingList);
            this.SaveBrandCommand = new RelayCommand<string>(name => SaveBrand(name));
            this.OpenBrandEdit = new RelayCommand(OpenBrandEditWindow);
            this.OpenBrandEditAdd = new RelayCommand(OpenBrandAddWindow);
            this.DeleteBrandCommand = new RelayCommand(DeleteBrand);


            // observable collections
            this.ProductTypeVMList = new ObservableCollection<ProductTypeVM>();
            this.ProductVMList = new ObservableCollection<ProductVM>();
            this.ProductShoppingVMList = new ObservableCollection<ProductVM>();
            this.BrandVMList = new ObservableCollection<BrandVM>();

            this.productTypeRepository.GetAll().ForEach(p => ProductTypeVMList.Add(new ProductTypeVM(p)));
            this.productRepository.GetAll().ForEach(p => ProductVMList.Add(new ProductVM(p)));
            this.brandRepository.GetAll().ForEach(b => BrandVMList.Add(new BrandVM(b)));

            //Trace.WriteLine("PRODUCT TYPES: " + ProductTypeVMList.Count);

            /*
             * DUMMY DATA PRODUCT (ja was te lui voor dummy repository)
             * 
            Brand brand = new Brand()
            {
                Name = "Unilever"
            };

            Department dep = new Department() {
                Name = "Kaas Afdeling"
            };

            ProductType type = new ProductType() {
                Name = "Kaas"
            };

            this.brandRepository.Add(brand);
            this.departmentRepository.Add(dep);
            this.productTypeRepository.Add(type);
            
            Product product = new Product()
            {
                ProductName = "Lekkere kaas",
                Brand = brand,
                Department = dep,
                Price = 80,
                ProductType = type
                  
                  
            };

            this.productRepository.Add(product);*/
        }

        private void DeleteBrand()
        {
            if (CurrentBrand != null && !CurrentBrand.IsNew)
            {
                this.brandRepository.Delete(CurrentBrand.Brand);
                BrandVMList.Remove(CurrentBrand);
                CurrentBrand = null;
            }
        }

        private void SaveBrand(string name)
        {
            CurrentBrand.Name = name;
            if (CurrentBrand.IsNew)
            {
                this.brandRepository.Add(CurrentBrand.Brand);
                CurrentBrand.IsNew = false;
                BrandVMList.Add(CurrentBrand);
            }
            else
            {
                this.brandRepository.Update();
            }
            BrandEdit.Instance.Close();
            OpenBrandWindow();
        }

        private void ClearShoppingList()
        {
            CurrentShopListProduct = null;
            ProductShoppingVMList.Clear();
            RaisePropertyChanged("ShoppingListPrice");
        }

        private void AddMoreToShoppingList()
        {
            if (CurrentShopListProduct != null && ProductShoppingVMList.Contains(CurrentShopListProduct))
            {
                CurrentShopListProduct.Amount += 1;
                RaisePropertyChanged("ShoppingListPrice");
            } 
        }

        private void AddLessToShoppingList()
        {
            if (CurrentShopListProduct != null && ProductShoppingVMList.Contains(CurrentShopListProduct))
            {
                CurrentShopListProduct.Amount -= 1;
                if (CurrentShopListProduct.Amount == 0)
                {
                    ProductShoppingVMList.Remove(CurrentShopListProduct);
                    CurrentShopListProduct = null;
                }
                RaisePropertyChanged("ShoppingListPrice");
            }
        }

        private void AddProductToShoppingList()
        {
            if (CurrentListProduct != null && !ProductShoppingVMList.Contains(CurrentListProduct))
            {
                ProductShoppingVMList.Add(CurrentListProduct);
                RaisePropertyChanged("ShoppingListPrice");
            }
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
                CurrentProductType.IsNew = false;
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
            if (CurrentProductType != null && !CurrentProductType.IsNew)
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

        private void OpenBrandAddWindow()
        {
            Trace.WriteLine("ADD");
            CurrentBrand = new BrandVM();
            OpenBrandEditWindow();
        }

        private void OpenBrandEditWindow()
        {
            Trace.WriteLine("EDIT");
            if (CurrentBrand == null)
            {
                return;
            }
            BrandList.Instance.Close();
            BrandEdit.Instance.Show();
        }
    }
}