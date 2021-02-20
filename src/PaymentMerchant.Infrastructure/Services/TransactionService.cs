using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Extensions;
using PaymentMerchant.Core.Interfaces;
using PaymentMerchant.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaymentMerchant.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<ICollection<TransactionResponse>> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllTransactions();
            return transactions.Select(s => s.MapTransactionResponse()).ToList();
        }
    }

}
