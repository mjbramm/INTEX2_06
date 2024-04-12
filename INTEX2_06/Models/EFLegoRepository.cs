using Microsoft.EntityFrameworkCore;

namespace INTEX2_06.Models
{
    public class EFLegoRepository : ILegoRepository
    {
        private LegostoreContext _context;

        private AppIdentityDbContext _identitycontext;
        public EFLegoRepository(LegostoreContext context, AppIdentityDbContext identityContext)
        {
            _context = context;
            _identitycontext = identityContext;
        }
        public IQueryable<Lego> Legos => _context.Legos;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<AppUser> AppUser => _identitycontext.Users;
        public IQueryable<LineItem> LineItems => _context.LineItems;

        public async Task AddProduct(Lego product)
        {
            _context.Legos.Add(product); // Add the product to the Legos DbSet in your DbContext
            await _context.SaveChangesAsync();
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
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

        public async Task UpdateProductAsync(int product_ID)
        {
            var product = await _context.Legos.FindAsync(product_ID);

            if (product != null)
            {
                _context.Legos.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderAsync(int transaction_ID)
        {
            var order = await _context.Orders.FindAsync(transaction_ID);

            if (order != null)
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
