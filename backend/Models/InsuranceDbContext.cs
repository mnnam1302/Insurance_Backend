using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class InsuranceDbContext: DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Beneficiary> Beneficiaries { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<InsuranceType> InsuranceTypes { get; set; }

        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<Registration> Registrations { get; set; }

    }
}
