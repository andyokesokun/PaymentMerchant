using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Interfaces;

namespace PaymentMerchant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService  transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ICollection<TransactionResponse>> GetAll()
        {
            return await _transactionService.GetAllTransactions();
        }
    }
}