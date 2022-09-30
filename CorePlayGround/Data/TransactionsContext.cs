using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CorePlayGround.Models;

namespace CorePlayGround.Data
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext (DbContextOptions<TransactionsContext> options)
            : base(options)
        {
        }

        public DbSet<CorePlayGround.Models.Transactions> Transactions { get; set; } = default!;
    }
}
