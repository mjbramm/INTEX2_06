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
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Lego leg) => Lines.RemoveAll(x => x.Lego.product_ID == leg.product_ID);

        public virtual void Clear() => Lines.Clear(); 

        public decimal CalculateTotal() => Lines.Sum(x => 25 * x.Quantity);
    
        public class CartLine 
        {
            public int CartLineId { get; set; }
            public Lego Lego { get; set; }
            public int Quantity { get; set; }
        }
    
    }
}
