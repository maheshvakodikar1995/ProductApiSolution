using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string ProductName { get; set; } = string.Empty;

        public ICollection<Item> Items { get; set; }
            = new List<Item>();
    }
}
