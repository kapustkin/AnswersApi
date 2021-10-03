using AnswersApi.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersApi.Context
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<AnswerAttachment> AnswerAttachments { get; set; }
        public DbSet<AnswerEvent> AnswerEvents { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}