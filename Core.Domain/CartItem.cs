using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public Animal Animal { get; set; }
        public int AnimalId { get; set; }
    }
}
