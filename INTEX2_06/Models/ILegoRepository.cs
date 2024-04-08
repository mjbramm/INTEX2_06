namespace INTEX2_06.Models
{
    public interface ILegoRepository
    {
        public IQueryable<Lego> Legos { get; }
    }
}
