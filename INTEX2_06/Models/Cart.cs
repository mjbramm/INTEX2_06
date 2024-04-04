namespace INTEX2_06.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Book boo, int quantity) 
        {
            CartLine? line = Lines
                .Where(x => x.Book.BookId == boo.BookId)
                .FirstOrDefault();

            //Has this item already been added to our cart?
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = boo,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Book boo) => Lines.RemoveAll(x => x.Book.BookId == boo.BookId);

        public virtual void Clear() => Lines.Clear(); 

        public decimal CalculateTotal() => Lines.Sum(x => 25 * x.Quantity);
    
        public class CartLine 
        {
            public int CartLineId { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }

        }
    
    }
}
