using System;
using System.Collections.Generic;
using CredWiseCustomer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CredWiseCustomer.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DecisionAppLog> DecisionAppLogs { get; set; }

    public virtual DbSet<Fdapplication> Fdapplications { get; set; }

    public virtual DbSet<Fdtransaction> Fdtransactions { get; set; }

    public virtual DbSet<Fdtype> Fdtypes { get; set; }

    public virtual DbSet<GoldLoanApplication> GoldLoanApplications { get; set; }

    public virtual DbSet<GoldLoanDetail> GoldLoanDetails { get; set; }

    public virtual DbSet<HomeLoanApplication> HomeLoanApplications { get; set; }

    public virtual DbSet<HomeLoanDetail> HomeLoanDetails { get; set; }

    public virtual DbSet<LoanApplication> LoanApplications { get; set; }

    public virtual DbSet<LoanBankStatement> LoanBankStatements { get; set; }

    public virtual DbSet<LoanEnquiry> LoanEnquiries { get; set; }

    public virtual DbSet<LoanProduct> LoanProducts { get; set; }

    public virtual DbSet<LoanProductDocument> LoanProductDocuments { get; set; }

    public virtual DbSet<LoanRepaymentSchedule> LoanRepaymentSchedules { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<PersonalLoanDetail> PersonalLoanDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DecisionAppLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Decision__5E5486489C70BCD7");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProcessedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.DecisionAppLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DecisionA__LoanA__73BA3083");
        });

        modelBuilder.Entity<Fdapplication>(entity =>
        {
            entity.HasKey(e => e.FdapplicationId).HasName("PK__FDApplic__5A2486C02B7DEBEE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Fdtype).WithMany(p => p.Fdapplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FDApplica__FDTyp__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.Fdapplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FDApplica__UserI__75A278F5");
        });

        modelBuilder.Entity<Fdtransaction>(entity =>
        {
            entity.HasKey(e => e.FdtransactionId).HasName("PK__FDTransa__76CF381F29B42098");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Fdapplication).WithMany(p => p.Fdtransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FDTransac__FDApp__76969D2E");
        });

        modelBuilder.Entity<Fdtype>(entity =>
        {
            entity.HasKey(e => e.FdtypeId).HasName("PK__FDTypes__FFD0291B62AAF462");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<GoldLoanApplication>(entity =>
        {
            entity.HasKey(e => e.GoldLoanAppId).HasName("PK__GoldLoan__BD7F004360CC04F0");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.GoldLoanApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GoldLoanA__LoanA__29221CFB");
        });

        modelBuilder.Entity<GoldLoanDetail>(entity =>
        {
            entity.HasKey(e => e.LoanProductId).HasName("PK__GoldLoan__0D22CCC2A327CF7A");

            entity.Property(e => e.LoanProductId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.LoanProduct).WithOne(p => p.GoldLoanDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GoldLoanD__LoanP__778AC167");
        });

        modelBuilder.Entity<HomeLoanApplication>(entity =>
        {
            entity.HasKey(e => e.HomeLoanAppId).HasName("PK__HomeLoan__08F02377F50FBFA8");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.HomeLoanApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HomeLoanA__LoanA__2BFE89A6");
        });

        modelBuilder.Entity<HomeLoanDetail>(entity =>
        {
            entity.HasKey(e => e.LoanProductId).HasName("PK__HomeLoan__0D22CCC2A9C75627");

            entity.Property(e => e.LoanProductId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.LoanProduct).WithOne(p => p.HomeLoanDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HomeLoanD__LoanP__787EE5A0");
        });

        modelBuilder.Entity<LoanApplication>(entity =>
        {
            entity.HasKey(e => e.LoanApplicationId).HasName("PK__LoanAppl__F60027BD05BF4480");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.LoanProduct).WithMany(p => p.LoanApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanAppli__LoanP__797309D9");

            entity.HasOne(d => d.User).WithMany(p => p.LoanApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanAppli__UserI__7A672E12");
        });

        modelBuilder.Entity<LoanBankStatement>(entity =>
        {
            entity.HasKey(e => e.BankStatementId).HasName("PK__LoanBank__D4AD9FA4F00051AD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("Pending");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.LoanBankStatements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanBankS__LoanA__7B5B524B");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.LoanBankStatements).HasConstraintName("FK__LoanBankS__Verif__7C4F7684");
        });

        modelBuilder.Entity<LoanEnquiry>(entity =>
        {
            entity.HasKey(e => e.EnquiryId).HasName("PK__LoanEnqu__0A019B7D033EE504");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<LoanProduct>(entity =>
        {
            entity.HasKey(e => e.LoanProductId).HasName("PK__LoanProd__0D22CCC23CDA784F");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<LoanProductDocument>(entity =>
        {
            entity.HasKey(e => e.LoanProductDocumentId).HasName("PK__LoanProd__D85B694FDA7AAE04");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.LoanProduct).WithMany(p => p.LoanProductDocuments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanProdu__LoanP__7D439ABD");
        });

        modelBuilder.Entity<LoanRepaymentSchedule>(entity =>
        {
            entity.HasKey(e => e.RepaymentId).HasName("PK__LoanRepa__10AD21F2EB8DBA1E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("Pending");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.LoanRepaymentSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanRepay__LoanA__7E37BEF6");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logs__3214EC0779BC155B");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__PaymentT__55433A6B7E10ACF8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.PaymentTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentTr__LoanA__7F2BE32F");

            entity.HasOne(d => d.Repayment).WithMany(p => p.PaymentTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentTr__Repay__00200768");
        });

        modelBuilder.Entity<PersonalLoanDetail>(entity =>
        {
            entity.HasKey(e => e.LoanProductId).HasName("PK__Personal__0D22CCC2C179AB46");

            entity.Property(e => e.LoanProductId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.LoanProduct).WithOne(p => p.PersonalLoanDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonalL__LoanP__01142BA1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C04831C15");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
