using Microsoft.EntityFrameworkCore;
using PaymentMerchant.Core.Entities;
using PaymentMerchant.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Repositories
{
    public class CreditCardRepository : BaseRepository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<CreditCard> FindByCardNumber(string cardNumber)
        {
            return await _dataContext.CreditCards.Where(s => s.CreditCardNumber.Equals(cardNumber))
                                                 .FirstOrDefaultAsync();
        }
    }
}
