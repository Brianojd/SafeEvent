using Microsoft.EntityFrameworkCore;
using SafeEvent.Models;

namespace SafeEvent.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Cliente)
                .WithMany()
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.StaffValidador)
                .WithMany()
                .HasForeignKey(t => t.StaffValidadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
