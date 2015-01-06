/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PROG6_Assessment"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using DomainModel.Database;
using DomainModel.Interfaces;
using DomainModel.Model;
using DomainModel.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace PROG6_Assessment.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<DatabaseContext>();
            SimpleIoc.Default.Register<IRepository<Brand>, BrandEntityRepository>();
            SimpleIoc.Default.Register<IRepository<Department>, DepartmentEntityRepository>();
            SimpleIoc.Default.Register<IRepository<Discount>, DiscountEntityRepository>();
            SimpleIoc.Default.Register<IRepository<Product>, ProductEntityRepository>();
            SimpleIoc.Default.Register<IRepository<ProductType>, ProductTypeEntityRepository>();
            SimpleIoc.Default.Register<IRepository<Recipe>, RecipeEntityRepository>();
            SimpleIoc.Default.Register<AppieViewModel>();
        }

        public AppieViewModel Appie
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AppieViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}