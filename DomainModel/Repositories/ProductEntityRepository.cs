using DomainModel.Database;
using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public class ProductEntityRepository : EntityRepository<Product>
    {
        public ProductEntityRepository(DatabaseContext database)
            : base(database)
        {

        }
    }
}
