namespace INTEX2_06.Models
{
    public interface IBookRepository
    {
        public IQueryable<Book> Books { get; }
    }
}
