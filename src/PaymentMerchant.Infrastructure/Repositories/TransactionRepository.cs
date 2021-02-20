using Microsoft.EntityFrameworkCore;
using PaymentMerchant.Core.Entities;
using PaymentMerchant.Core.Extensions;
using PaymentMerchant.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<ICollection<Transaction>> GetAllTransactions()
        {
            return await _dataContext.Transactions
                                            .Include(s => s.CreditCard)
                                            .Include(s => s.PaymentStatus)
                                            .ToListAsync();

          
        }
    }
}
