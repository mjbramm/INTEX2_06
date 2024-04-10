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
    }
}
