﻿namespace INTEX2_06.Models
{
    public interface ILegoRepository
    {
        public IQueryable<Lego> Legos { get; }
        public IQueryable<Customer> Customers { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<LineItem> LineItems { get; }
        Task AddProduct(Lego product);
        Task<int> SaveChangesAsync();
        Task<Lego> GetProductByIdAsync(int product_ID);
        Task DeleteProductAsync(int product_ID);
    }
}
