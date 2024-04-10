namespace INTEX2_06.Models
{
    public class EFLegoRepository : ILegoRepository
    {
        private LegostoreContext _context;
        public EFLegoRepository(LegostoreContext temp) {
            _context = temp;
        }
        public IQueryable<Lego> Legos => _context.Legos;
        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<LineItem> LineItems => _context.LineItems;

        public async Task AddProduct(Lego product)
        {
            _context.Legos.Add(product); // Add the product to the Legos DbSet in your DbContext
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); // Save changes to the database asynchronously
        }

        public async Task<Lego> GetProductByIdAsync(int product_ID)
        {
            return await _context.Legos.FindAsync(product_ID);
        }

        public async Task DeleteProductAsync(int product_ID)
        {
            var product = await _context.Legos.FindAsync(product_ID);

            if (product != null)
            {
                _context.Legos.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
