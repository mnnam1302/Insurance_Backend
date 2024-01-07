using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class InsuranceDbContext: DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<VerificationPassword> VerificationPasswords { get; set; }

        public DbSet<Beneficiary> Beneficiaries { get; set; }

        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<BenefitType> BenefitTypes { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<BenefitDetail> BenefitDetails { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<PaymentRequest> PaymentRequests { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(InsuranceDbContext).Assembly);

            // primary key của bảng BenefitDetail
            modelBuilder.Entity<BenefitDetail>()
                .HasKey(bd => new { bd.InsuranceId, bd.BenefitId });
        }

    }
}
