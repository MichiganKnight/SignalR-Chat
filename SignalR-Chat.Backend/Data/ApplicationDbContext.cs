using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Backend.Entities;

namespace SignalR_Chat.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConversationMember>().HasKey(x => new
            {
                x.ConversationId,
                x.UserId
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}