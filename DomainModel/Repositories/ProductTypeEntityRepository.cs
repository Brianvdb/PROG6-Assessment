using DomainModel.Database;
using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public class ProductTypeEntityRepository : EntityRepository<ProductType>
    {
        public ProductTypeEntityRepository(DatabaseContext database)
            : base(database)
        {

        }
    }
}
