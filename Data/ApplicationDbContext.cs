using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Contract_Administrator.Models;

namespace Contract_Administrator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Contract_Administrator.Models.Contract> Contract { get; set; } = default!;
        public DbSet<Contract_Administrator.Models.Client> Client { get; set; } = default!;
        public DbSet<Contract_Administrator.Models.Adviser> Adviser { get; set; } = default!;
        public DbSet<Contract_Administrator.Models.ContractAdviser> ContractAdvisers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // tady prehod to c a ca
            base.OnModelCreating(builder);
            builder.Entity<ContractAdviser>()
                .HasKey(ca => new { ca.ContractId, ca.AdviserId });

            builder.Entity<ContractAdviser>()
                .HasOne(ca => ca.Contract)
                .WithMany(c => c.ContractAdviser)
                .HasForeignKey(ca => ca.ContractId);

            builder.Entity<ContractAdviser>()
                .HasOne(ca => ca.Adviser)
                .WithMany(a => a.ContractAdviser)
                .HasForeignKey(ca => ca.AdviserId);
        }
    }

}
