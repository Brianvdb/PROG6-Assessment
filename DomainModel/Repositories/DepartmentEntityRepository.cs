using DomainModel.Database;
using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public class DepartmentEntityRepository : EntityRepository<Department>
    {
        public DepartmentEntityRepository(DatabaseContext database)
            : base(database)
        {

        }
    }
}
