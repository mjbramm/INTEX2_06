using System.Linq;

namespace INTEX2_06.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Lego leg, int quantity) 
        {
            CartLine? line = Lines
                .Where(x => x.Lego.product_ID == leg.product_ID)
                .FirstOrDefault();

            //Has this item already been added to our cart?
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Lego = leg,
                    Quantity = quantity,
                    Price = (float)leg.price
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Lego leg) => Lines.RemoveAll(x => x.Lego.product_ID == leg.product_ID);

        public virtual void Clear() => Lines.Clear(); 

        public float CalculateTotal() => Lines.Sum(x => x.Price * x.Quantity);

        public virtual void InitializeCart()
        {
            // Clear the cart when initializing for a new user
            Clear();
        }

        public class CartLine 
        {
            public int CartLineId { get; set; }
            public Lego Lego { get; set; }
            public int Quantity { get; set; }
            public double Subtotal { get; set; }
            public float Price { get; set; }

        }
    
    }
}
