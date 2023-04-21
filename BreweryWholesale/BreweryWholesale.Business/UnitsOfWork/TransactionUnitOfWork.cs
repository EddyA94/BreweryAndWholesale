using Microsoft.EntityFrameworkCore.Storage;

namespace BreweryWholesale.Infrastructure.UnitsOfWork
{
    public class TransactionUnitOfWork : ITransactionUnitOfWork
    {
        private readonly BrewerWholesaleDBContext _context;
        private IDbContextTransaction? _transaction;

        public TransactionUnitOfWork(BrewerWholesaleDBContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
