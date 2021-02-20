using PaymentMerchant.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<ICollection<Transaction>> GetAllTransactions();
    }
}

