using DomainModel.Interfaces;
using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.Model
{
    public class DummyProductRepository : IRepository<Product>
    {
        private List<Product> products;

        public DummyProductRepository()
        {
            products = new List<Product>()
            {
                new Product() {
                    Brand = new Brand() { Name = "Campina" },
                    Department = new Department { Name="" },
                }
            };
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product Add(Product t)
        {
            return t;
        }

        public Product Delete(Product t)
        {
            return t;
        }

        public void Update()
        {

        }
    }
}
