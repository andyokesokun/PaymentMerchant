using PaymentMerchant.Core.Entities;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Repositories
{
    public interface ICreditCardRepository : IRepository<CreditCard>
    {
       Task<CreditCard> FindByCardNumber(string cardNumber);
    }
}
