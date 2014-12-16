using DomainModel.Database;
using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public class DiscountEntityRepository : EntityRepository<Discount>
    {
        public DiscountEntityRepository(DatabaseContext database)
            : base(database)
        {

        }
    }
}
