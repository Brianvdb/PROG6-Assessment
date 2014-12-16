using DomainModel.Interfaces;
using DomainModel.Model;
using GalaSoft.MvvmLight;

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

        public AppieViewModel(IRepository<Brand> brandRepo, IRepository<Department> departmentRepo, IRepository<Discount> discountRepo, IRepository<Product> productRepo)
        {
            this.brandRepository = brandRepo;
            this.departmentRepository = departmentRepo;
            this.discountRepository = discountRepo;
            this.productRepository = productRepo;
        }
    }
}