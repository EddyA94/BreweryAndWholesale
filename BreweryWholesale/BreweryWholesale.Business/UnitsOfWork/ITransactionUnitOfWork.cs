namespace BreweryWholesale.Infrastructure.UnitsOfWork
{
    public interface ITransactionUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
