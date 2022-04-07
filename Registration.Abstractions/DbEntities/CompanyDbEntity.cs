using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Abstractions.DbEntities
{
    public class CompanyDbEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
