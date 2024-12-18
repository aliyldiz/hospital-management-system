using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories;

public class ApplicationDbContext : IdentityDbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<HospitalInfo> HospitalInfos { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<Lab> Labs { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<MedicineReport> MedicineReports { get; set; }
    public DbSet<PatientReport> PatientReports { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }
    public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<TestPrice> TestPrices { get; set; }
    public DbSet<Timing> Timings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.Property(e => e.Advance).HasColumnType("decimal(18,2)");
            entity.Property(e => e.MedicineCharge).HasColumnType("decimal(18,2)");
            entity.Property(e => e.OperationCharge).HasColumnType("decimal(18,2)");
            entity.Property(e => e.RoomCharge).HasColumnType("decimal(18,2)");
            entity.Property(e => e.TotalBill).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.Property(e => e.Cost).HasColumnType("decimal(18,2)");
        });
        
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasOne<ApplicationUser>(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete

            entity.HasOne<ApplicationUser>(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete
        });
        
        modelBuilder.Entity<PatientReport>(entity =>
        {
            entity.HasOne<ApplicationUser>(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete

            entity.HasOne<ApplicationUser>(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.Property(e => e.BonusSalary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Compensation).HasColumnType("decimal(18,2)");
            entity.Property(e => e.HourlySalary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.NetSalary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
        });
        
        modelBuilder.Entity<TestPrice>()
            .HasOne(tp => tp.Lab)
            .WithMany()
            .HasForeignKey(tp => tp.LabId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TestPrice>()
            .HasOne(tp => tp.Bill)
            .WithMany()
            .HasForeignKey(tp => tp.BillId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TestPrice>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        base.OnModelCreating(modelBuilder);
    }
} 