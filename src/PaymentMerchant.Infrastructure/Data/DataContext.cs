using Microsoft.EntityFrameworkCore;
using PaymentMerchant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Infrastructure.Data
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }

        public  DbSet<CreditCard> CreditCards { get; set; }
        public  DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
    }
}
