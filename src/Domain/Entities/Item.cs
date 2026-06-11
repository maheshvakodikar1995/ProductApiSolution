using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Item : BaseEntity
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; } = null!;
    }
}
