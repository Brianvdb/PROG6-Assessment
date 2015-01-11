using DomainModel.Interfaces;
using DomainModel.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PROG6_Assessment.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Linq;

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
        public ICommand EditProductCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }
        public ICommand RemoveProductCommand { get; set; }
        public ICommand OpenDepartmentList { get; set; }
        public ICommand AddDepartmentCommand { get; set; }
        public ICommand EditDepartmentCommand { get; set; }
        public ICommand SaveDepartmentCommand { get; set; }
        public ICommand DeleteDepartmentCommand { get; set; }
        public ICommand OpenRecipeList { get; set; }
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
        public ICommand SaveRecipeCommand { get; set; }
        public ICommand OpenRecipeAddCommand { get; set; }
        public ICommand AddRecipeToShoppingListCommand { get; set; }
        public ICommand RemoveRecipeCommand { get; set; }
        public ICommand SaveReceiptCommand { get; set; }
        public ICommand ManageProductDiscountsCommand { get; set; }
        public ICommand AddDiscountCommand { get; set; }
        public ICommand DeleteDiscountCommand { get; set; }
        public ICommand EditDiscountCommand { get; set; }
        public ICommand SaveDiscountCommand { get; set; }

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

        private RecipeVM _newRecipe;
        public RecipeVM NewRecipe
        {
            get
            {
                return _newRecipe;
            }
            set
            {
                _newRecipe = value;
                RaisePropertyChanged();
            }
        }

        private RecipeVM _currentRecipe;
        public RecipeVM CurrentRecipe
        {
            get
            {
                return _currentRecipe;
            }
            set
            {
                _currentRecipe = value;
                RaisePropertyChanged();
            }
        }

        private DepartmentVM _currentDepartment;
        public DepartmentVM CurrentDepartment
        {
            get
            {
                return _currentDepartment;
            }
            set
            {
                _currentDepartment = value;
                RaisePropertyChanged();
            }
        }

        private DiscountVM _currentDiscount;
        public DiscountVM CurrentDiscount
        {
            get
            {
                return _currentDiscount;
            }
            set
            {
                _currentDiscount = value;
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

        private int _filterProductTypeId;

        public int FilterProductTypeId
        { 
            get
            {
                return _filterProductTypeId;
            }
            set
            {
                _filterProductTypeId = value;
                FilterProductList();
                RaisePropertyChanged();
            }
        }

        private int _filterDepartmentId;

        public int FilterDepartmentId
        {
            get
            {
                return _filterDepartmentId;
            }
            set
            {
                _filterDepartmentId = value;
                FilterProductList();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProductTypeVM> ProductTypeVMList { get; set; }
        public ObservableCollection<ProductVM> ProductVMList { get; set; }
        public ObservableCollection<ProductVM> ProductShoppingVMList { get; set; }
        public ObservableCollection<BrandVM> BrandVMList { get; set; }
        public ObservableCollection<RecipeVM> RecipeVMList { get; set; }
        public ObservableCollection<DepartmentVM> DepartmentVMList { get; set; }
        public ObservableCollection<ProductVM> ProductFilterVMList { get; set; }
        public ObservableCollection<DiscountVM> DiscountVMList { get; set; }

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
            this.EditProductCommand = new RelayCommand(OpenProductEditWindow);
            this.AddProductCommand = new RelayCommand(OpenProductAddWindow);
            this.SaveProductCommand = new RelayCommand(SaveProduct);
            this.RemoveProductCommand = new RelayCommand(RemoveProduct);
            this.OpenRecipeList = new RelayCommand(OpenRecipeWindow);
            this.OpenDepartmentList = new RelayCommand(OpenDepartmentWindow);
            this.AddDepartmentCommand = new RelayCommand(OpenAddDepartmentWindow);
            this.EditDepartmentCommand = new RelayCommand(OpenEditDepartmentWindow);
            this.SaveDepartmentCommand = new RelayCommand<string>(name => SaveDepartment(name));
            this.DeleteDepartmentCommand = new RelayCommand(DeleteDepartment);
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
            this.SaveRecipeCommand = new RelayCommand<string>(name => SaveRecipe(name));
            this.OpenRecipeAddCommand = new RelayCommand(OpenRecipeAddWindow);
            this.AddRecipeToShoppingListCommand = new RelayCommand(AddRecipeToShoppingList);
            this.RemoveRecipeCommand = new RelayCommand(RemoveRecipe);
            this.SaveReceiptCommand = new RelayCommand(SaveReceipt);
            this.ManageProductDiscountsCommand = new RelayCommand(OpenDiscountManagementWindow);
            this.AddDiscountCommand = new RelayCommand(OpenDiscountAddWindow);
            this.EditDiscountCommand = new RelayCommand(OpenDiscountEditWindow);
            this.DeleteDiscountCommand = new RelayCommand(DeleteDiscount);
            this.SaveDiscountCommand = new RelayCommand(SaveDiscount);

            // observable collections
            this.ProductTypeVMList = new ObservableCollection<ProductTypeVM>();
            this.ProductTypeVMList.Add(new ProductTypeVM() { Name = "Geen" });
            this.ProductVMList = new ObservableCollection<ProductVM>();
            this.ProductShoppingVMList = new ObservableCollection<ProductVM>();
            this.BrandVMList = new ObservableCollection<BrandVM>();
            this.BrandVMList.Add(new BrandVM() { Name = "Geen" });
            this.RecipeVMList = new ObservableCollection<RecipeVM>();
            this.DepartmentVMList = new ObservableCollection<DepartmentVM>();
            this.DepartmentVMList.Add(new DepartmentVM() { Name = "Geen" });
            this.ProductFilterVMList = new ObservableCollection<ProductVM>();

            this.productTypeRepository.GetAll().ForEach(p => ProductTypeVMList.Add(new ProductTypeVM(p)));
            this.productRepository.GetAll().ForEach(p => ProductVMList.Add(new ProductVM(p)));
            this.brandRepository.GetAll().ForEach(b => BrandVMList.Add(new BrandVM(b)));
            this.recipeRepository.GetAll().ForEach(r => RecipeVMList.Add(new RecipeVM(r)));
            this.departmentRepository.GetAll().ForEach(d => DepartmentVMList.Add(new DepartmentVM(d)));

            FilterProductList();
        }

        private void FilterProductList()
        {
            int departmentId = FilterDepartmentId;
            int productTypeId = FilterProductTypeId;

            ProductFilterVMList.Clear();
            foreach (ProductVM p in ProductVMList)
            {
                if ((departmentId == 0 || p.Product.DepartmentId == departmentId) && (productTypeId == 0 || p.Product.ProductTypeId == productTypeId))
                {
                    ProductFilterVMList.Add(p);
                }
            }
        }

        private void SaveReceipt()
        {
            if (ProductShoppingVMList.Count == 0)
            {
                string message = "Er staan geen producten in uw boodschappenlijstje.";
                string caption = "Fout";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(message, caption, buttons, icon);
                return;
            }

            ShoppingListUtil.SaveAsPDF(ProductShoppingVMList, ShoppingListPrice);
        }

        private void RemoveRecipe()
        {
            if (CurrentRecipe != null)
            {
                this.recipeRepository.Delete(CurrentRecipe.Recipe);
                RecipeVMList.Remove(CurrentRecipe);
                CurrentRecipe = null;
            }
        }

        private void AddRecipeToShoppingList()
        {

            foreach(ProductVM productVM in CurrentRecipe.ProductVMList) {
                if (!contains(productVM.Product, ProductShoppingVMList))
                {
                    productVM.Amount = 1;
                    ProductShoppingVMList.Add(productVM);
                    RaisePropertyChanged("ShoppingListPrice");
                }
            }
        }

        private bool contains(Product target, ObservableCollection<ProductVM> products)
        {
            foreach(ProductVM p in products) {
                if (p.Id == target.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private void OpenRecipeAddWindow()
        {
            NewRecipe = new RecipeVM(ProductShoppingVMList);
            if (!RecipeAdd.Instance.IsVisible)
            {
                RecipeAdd.Instance.Show();
            }
        }

        private void SaveRecipe(string name)
        {
            if (NewRecipe == null)
            {
                return;
            }

            NewRecipe.Name = name;
            RecipeAdd.Instance.Close();

            this.recipeRepository.Add(NewRecipe.Recipe);

            this.RecipeVMList.Add(NewRecipe);

            if (!RecipeList.Instance.IsVisible)
            {
                RecipeList.Instance.Show();
            }
        }

        private void DeleteBrand()
        {
            if (CurrentBrand != null && !CurrentBrand.IsNew)
            {
                foreach (Product p in this.productRepository.GetAll())
                {
                    if (p.BrandId == CurrentBrand.Id)
                    {
                        string message = "Je kan dit merk niet verwijderen omdat het gekoppeld is aan één of meerdere producten.";
                        string caption = "Fout";
                        MessageBoxButton buttons = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;
                        MessageBox.Show(message, caption, buttons, icon);
                        return;
                    }
                }
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

            try
            {
                OpenBrandWindow();
                BrandEdit.Instance.Close();
            }
            catch { }
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
            if (CurrentListProduct != null && !contains(CurrentListProduct.Product, ProductShoppingVMList))
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

        private void OpenAddDepartmentWindow()
        {
            CurrentDepartment = new DepartmentVM();
            OpenEditDepartmentWindow();
        }

        private void OpenEditDepartmentWindow()
        {////
            Console.WriteLine("halli halo");
            
            if (CurrentDepartment == null)
            {
                return;
            }
            DepartmentList.Instance.Close();
            DepartmentEdit.Instance.Show();
        }

        private void DeleteDepartment()
        {
            if (CurrentDepartment != null && !CurrentDepartment.IsNew)
            {
                foreach (Product p in this.productRepository.GetAll())
                {
                    if (p.Discounts.Where(d => d.Id == CurrentDepartment.Id).FirstOrDefault() != null)
                    {
                        string message = "Je kan deze afdeling niet verwijderen omdat het gekoppeld is aan één of meerdere producten.";
                        string caption = "Fout";
                        MessageBoxButton buttons = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;
                        MessageBox.Show(message, caption, buttons, icon);
                        return;
                    }
                }
                this.departmentRepository.Delete(CurrentDepartment.Department);
                DepartmentVMList.Remove(CurrentDepartment);
                CurrentDepartment = null;
            }
        }

        private void SaveDepartment(string name)
        {
            CurrentDepartment.Name = name;
            if (CurrentDepartment.IsNew)
            {
                this.departmentRepository.Add(CurrentDepartment.Department);
                CurrentDepartment.IsNew = false;
                DepartmentVMList.Add(CurrentDepartment);
            }
            else
            {
                this.departmentRepository.Update();
            }

            try
            {
                OpenDepartmentWindow();
                DepartmentEdit.Instance.Close();
            }
            catch { }
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
                foreach (Product p in this.productRepository.GetAll())
                {
                    if (p.ProductTypeId == CurrentProductType.Id)
                    {
                        string message = "Je kan dit product type niet verwijderen omdat het gekoppeld is aan één of meerdere producten.";
                        string caption = "Fout";
                        MessageBoxButton buttons = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;
                        MessageBox.Show(message, caption, buttons, icon);
                        return;
                    }
                }
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
            CurrentBrand = new BrandVM();
            OpenBrandEditWindow();
        }

        private void OpenBrandEditWindow()
        {
            if (CurrentBrand == null)
            {
                return;
            }
            BrandList.Instance.Close();
            BrandEdit.Instance.Show();
        }

        private void OpenProductAddWindow()
        {
            CurrentListProduct = new ProductVM();
            OpenProductEditWindow();
        }

        private void OpenProductEditWindow()
        {
            if (CurrentListProduct == null)
            {
                return;
            }
            Products.Instance.Close();
            ProductEdit.Instance.Show();
        }

        private void SaveProduct()
        {
            if (CurrentListProduct.IsNew)
            {
                this.productRepository.Add(CurrentListProduct.Product);
                CurrentListProduct.IsNew = false;
                ProductVMList.Add(CurrentListProduct);
            }
            else
            {
                this.productRepository.Update();
            }

            FilterProductList();

            try
            {
                OpenProductWindow();
                ProductEdit.Instance.Close();
            }
            catch { }
        }

        private void RemoveProduct()
        {
            if (CurrentListProduct != null && !CurrentListProduct.IsNew)
            {
                this.productRepository.Delete(CurrentListProduct.Product);
                ProductVMList.Remove(CurrentListProduct);
                FilterProductList();
                CurrentListProduct = null;
            }

            try
            {
                OpenProductWindow();
                ProductEdit.Instance.Close();
            }
            catch { }
        }

        public void OpenDiscountManagementWindow()
        {
            
            if (CurrentListProduct == null)
            {
                return;
            }

            DiscountVMList = new ObservableCollection<DiscountVM>() {};
            CurrentListProduct.Product.Discounts.ForEach(d => DiscountVMList.Add(new DiscountVM(d)));
            Console.WriteLine("Hoeveelheid: " + DiscountVMList.Count);

            DiscountList.Instance.Show();
        }

        private void OpenDiscountAddWindow()
        {
            CurrentDiscount = new DiscountVM();
            OpenDiscountEditWindow();
        }

        private void OpenDiscountEditWindow()
        {
            if (CurrentDiscount == null)
            {
                return;
            }
            DiscountList.Instance.Close();
            DiscountEdit.Instance.Show();
        }

        private void DeleteDiscount()
        {
            if (CurrentDiscount != null && !CurrentDiscount.IsNew)
            {
                this.discountRepository.Delete(CurrentDiscount.Discount);
                DiscountVMList.Remove(CurrentDiscount);
                CurrentDiscount = null;
            }

            try
            {
                ProductEdit.Instance.Close();
            }
            catch { }
        }

        private void SaveDiscount()
        {
            if (CurrentDiscount.IsNew)
            {
                this.discountRepository.Add(CurrentDiscount.Discount);
                CurrentDiscount.IsNew = false;
                CurrentListProduct.Product.Discounts.Add(CurrentDiscount.Discount);
                productRepository.Update();
                DiscountVMList.Add(CurrentDiscount);
            }
            else
            {
                this.discountRepository.Update();
            }

            try
            {
                DiscountEdit.Instance.Close();
            }
            catch { }
        }
    }
}