using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionSystem2.Entites
{
    public class AdmissionSystemDbContext : DbContext
    {
        public AdmissionSystemDbContext(DbContextOptions<AdmissionSystemDbContext> options) : base(options)
        {
            Database.Migrate();

        }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<ParentInfo> ParentInfo { get; set; }
        public DbSet<EmergencyContact> EmergencyContact { get; set; }
        public DbSet<AdmissionDetails> AdmissionDetails { get; set; }
        public DbSet<TransferredStudent> TransferredStudents { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Sibling> Sibling { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Fawry> Fawry { get; set; }
        public DbSet<MasterCard> MasterCard { get; set; }
        public DbSet<BankElahly> BankElahly { get; set; }
        public DbSet<AdmissionPeriod> AdmissionPeriod { get; set; }
        public DbSet<InterviewCriteria> InterviewCriteria { get; set; }
        public DbSet<Interview> Interview { get; set; }
        public DbSet<FamilyStatus> FamilyStatus { get; set; }
        public DbSet<AdministratorOfficer> AdministratorOfficer { get; set; }
        public DbSet<DocumentCriteria> DocumentCriteria { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>().ToTable("Applicant");
            modelBuilder.Entity<ParentInfo>().ToTable("ParentInfo");
            modelBuilder.Entity<EmergencyContact>().ToTable("EmergencyContact");
            modelBuilder.Entity<AdmissionDetails>().ToTable("AdmissionDetails");
            modelBuilder.Entity<TransferredStudent>().ToTable("TransferredStudent");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<MedicalHistory>().ToTable("MedicalHistory");
            modelBuilder.Entity<Sibling>().ToTable("Sibling");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<Fawry>().ToTable("Fawry");
            modelBuilder.Entity<MasterCard>().ToTable("MasterCard");
            modelBuilder.Entity<BankElahly>().ToTable("BankElahly");
            modelBuilder.Entity<AdmissionPeriod>().ToTable("AdmissionPeriod");
            modelBuilder.Entity<InterviewCriteria>().ToTable("InterviewCriteria");
            modelBuilder.Entity<Interview>().ToTable("Interview");
            modelBuilder.Entity<FamilyStatus>().ToTable("FamilyStatus");
            modelBuilder.Entity<AdministratorOfficer>().ToTable("AdministratorOfficer");
            modelBuilder.Entity<DocumentCriteria>().ToTable("DocumentCriteria");


        }



    }
}
