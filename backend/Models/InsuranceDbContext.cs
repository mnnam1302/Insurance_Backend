using backend.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class InsuranceDbContext: DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(InsuranceDbContext).Assembly);

            // primary key của bảng BenefitDetail
            modelBuilder.Entity<BenefitDetail>()
                .HasKey(bd => new { bd.InsuranceId, bd.BenefitId });

            modelBuilder.Entity<BeneficiaryCount>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("View_BeneficiaryCount");
                    eb.Property(v => v.Label).HasColumnName("Label");
                }
            );
            modelBuilder.Entity<ContractRevenue>(
                cr =>
                {
                    cr.HasNoKey();
                    cr.ToView("View_RevenueContract");
                    cr.Property(v => v.Year).HasColumnName("Year");
                    cr.Property(v => v.Month).HasColumnName("Month");
                    cr.Property(v => v.Revenue).HasColumnName("Revenue");
                }
            );
            modelBuilder.Entity<UserCount>(
                cr =>
                {
                    cr.HasNoKey();
                    cr.ToView("View_CountUser");
                    cr.Property(v => v.Year).HasColumnName("Year");
                    cr.Property(v => v.Month).HasColumnName("Month");
                    cr.Property(v => v.Total).HasColumnName("Total");
                }
            );
            modelBuilder.Entity<SummaryPaymentContract>(
               cr =>
               {
                   cr.HasNoKey();
                   cr.ToView("View_Icome_PaymentContract");
                   cr.Property(v => v.Year).HasColumnName("Year");
                   cr.Property(v => v.Month).HasColumnName("Month");
                   cr.Property(v => v.Amount).HasColumnName("Amount");
               }
            );
            modelBuilder.Entity<SummaryPaymentRequest>(
               cr =>
               {
                   cr.HasNoKey();
                   cr.ToView("View_Payment_PaymentRequest");
                   cr.Property(v => v.Year).HasColumnName("Year");
                   cr.Property(v => v.Month).HasColumnName("Month");
                   cr.Property(v => v.Amount).HasColumnName("Amount");
               }
            );
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
        public DbSet<ContractPaymentHistory> ContractPaymentHistories { get; set; }


        public DbSet<BeneficiaryCount> BeneficiaryCounts { get; set; }
        public DbSet<ContractRevenue> ContractRevenues { get; set; }
        public DbSet<UserCount> UserCounts { get; set; }
        public DbSet<SummaryPaymentContract> SummaryPaymentContracts { get; set; }
        public DbSet<SummaryPaymentRequest> SummaryPaymentRequests { get; set; }
    }
}
