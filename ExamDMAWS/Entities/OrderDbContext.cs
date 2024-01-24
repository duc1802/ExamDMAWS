using ExamDMAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamDMAWS.Entities
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public virtual DbSet<OrderModel> OrderTbl { get; set; }
    }
}
