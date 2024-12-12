using ChatWebApiSignalR.DTO;
using Microsoft.EntityFrameworkCore;

namespace ChatWebApiSignalR.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
