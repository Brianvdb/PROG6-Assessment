using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG6_Assessment.ViewModel;
using DomainModel.Interfaces;
using DomainModel.Model;
using Moq;
using System.Linq;
using System.Collections.Generic;
using PROG6_Assessment;

namespace PROG6_AssessmentTest
{
    [TestClass]
    public class BasisTests
    {
        //data lists
        private List<Brand> brandList;
        private List<Department> departmentList;
        private List<Discount> discountList;
        private List<Product> productList;
        private List<ProductType> productTypeList;
        private List<Recipe> recipeList;

        //mocks
        private Mock<IRepository<Brand>> brandRepo = new Mock<IRepository<Brand>>();
        private Mock<IRepository<Department>> departmentRepo = new Mock<IRepository<Department>>();
        private Mock<IRepository<Discount>> discountRepo = new Mock<IRepository<Discount>>();
        private Mock<IRepository<Product>> productRepo = new Mock<IRepository<Product>>();
        private Mock<IRepository<ProductType>> productTypeRepo = new Mock<IRepository<ProductType>>();
        private Mock<IRepository<Recipe>> recipeRepo = new Mock<IRepository<Recipe>>();

        //standart AppieViewModel
        private AppieViewModel standartViewModel;

        public BasisTests()
        {
            //setup "data"
            brandList = new List<Brand>();
            departmentList = new List<Department>();
            discountList = new List<Discount>();
            productList = new List<Product>();
            productTypeList = new List<ProductType>();
            recipeList = new List<Recipe>();
        }

        [TestMethod]
        public void TestAddBrand()
        {
            //setup
            InitializeTest();
            standartViewModel.CurrentBrand = new BrandVM();

            //execute
            standartViewModel.SaveBrandCommand.Execute("Merk");
            
            //test
            //wordt weergegeven in de view
            Assert.IsTrue(standartViewModel.BrandVMList.Where(
                List => List.Brand.Name == "Merk").FirstOrDefault() != null);
            //word opgeslagen in de database
            brandRepo.Verify(r => r.Add(It.IsAny<Brand>()), Times.Once());
        }

        private AppieViewModel GenerateAppieViewModel(
            Mock<IRepository<Brand>> brandRepo,
            Mock<IRepository<Department>> departmentRepo,
            Mock<IRepository<Discount>> discountRepo,
            Mock<IRepository<Product>> productRepo,
            Mock<IRepository<ProductType>> productTypeRepo,
            Mock<IRepository<Recipe>> recipeRepo
            )
        {
            return new AppieViewModel(
                brandRepo.Object,
                departmentRepo.Object,
                discountRepo.Object,
                productRepo.Object,
                productTypeRepo.Object,
                recipeRepo.Object);
        }

        private void InitializeTest()
        {
            brandRepo.Setup(m => m.GetAll()).Returns(brandList);
            brandRepo.Setup(m => m.Add(new Brand())).Returns(new Brand());

            departmentRepo.Setup(m => m.GetAll()).Returns(departmentList);

            discountRepo.Setup(m => m.GetAll()).Returns(discountList);

            productRepo.Setup(m => m.GetAll()).Returns(productList);

            productTypeRepo.Setup(m => m.GetAll()).Returns(productTypeList);

            recipeRepo.Setup(m => m.GetAll()).Returns(recipeList);

            //setup standart AppieViewModel
            standartViewModel = GenerateAppieViewModel(brandRepo, departmentRepo, discountRepo, productRepo, productTypeRepo, recipeRepo);
        }
    }
}
