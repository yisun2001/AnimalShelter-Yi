using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public int ClientNumber { get; set; }
        public Client Client { get; set; }
        
        public ICollection<CartItem> CartItems { get; set; }
    }
}
