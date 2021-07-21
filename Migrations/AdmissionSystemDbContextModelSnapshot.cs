﻿// <auto-generated />
using System;
using AdmissionSystem2.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdmissionSystem2.Migrations
{
    [DbContext(typeof(AdmissionSystemDbContext))]
    partial class AdmissionSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdmissionSystem2.Entites.AdmissionDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Section")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId")
                        .IsUnique();

                    b.ToTable("AdmissionDetails");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.AdmissionPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EndDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdmissionPeriod");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Applicant", b =>
                {
                    b.Property<Guid>("ApplicantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Religion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpokenLanguage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicantId");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("AdmissionDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MedicalHistoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdmissionDetailsId");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("MedicalHistoryId");

                    b.HasIndex("PaymentId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Copy")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.EmergencyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("EmergencyContact");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.FamilyStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Guardian")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageSpoken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId")
                        .IsUnique();

                    b.ToTable("FamilyStatues");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Interview", b =>
                {
                    b.Property<int>("InterviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcadmicYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicantEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InterviewDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterviewTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterviewType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterviewerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("ScoreGrade")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterviewId");

                    b.ToTable("Interview");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.InterviewCriteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BreakTime")
                        .HasColumnType("int");

                    b.Property<string>("EndDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InterviewDuration")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfInterviewers")
                        .HasColumnType("int");

                    b.Property<string>("StartDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InterviewCriteria");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.MedicalHistory", b =>
                {
                    b.Property<Guid>("MedicalHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Glass")
                        .HasColumnType("bit");

                    b.Property<bool>("Hearing")
                        .HasColumnType("bit");

                    b.Property<string>("MedicalConditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysiologicalConditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhysiologicalNeed")
                        .HasColumnType("bit");

                    b.HasKey("MedicalHistoryId");

                    b.HasIndex("ApplicantId")
                        .IsUnique();

                    b.ToTable("MedicalHistory");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.ParentInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ParentInfo");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("ApplicantId")
                        .IsUnique();

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Sibling", b =>
                {
                    b.Property<Guid>("SibilingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiblingName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SibilingId");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Sibling");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.TransferredStudent", b =>
                {
                    b.HasBaseType("AdmissionSystem2.Entites.AdmissionDetails");

                    b.Property<string>("Curriculum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastYearScore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolName")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("TransferredStudent");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.BankElahly", b =>
                {
                    b.HasBaseType("AdmissionSystem2.Entites.Payment");

                    b.Property<string>("BankCode")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("BankElahly");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Fawry", b =>
                {
                    b.HasBaseType("AdmissionSystem2.Entites.Payment");

                    b.Property<string>("FawryCode")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Fawry");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.MasterCard", b =>
                {
                    b.HasBaseType("AdmissionSystem2.Entites.Payment");

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.ToTable("MasterCard");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.AdmissionDetails", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithOne("AdmissionDetails")
                        .HasForeignKey("AdmissionSystem2.Entites.AdmissionDetails", "ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Application", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.AdmissionDetails", "AdmissionDetails")
                        .WithMany()
                        .HasForeignKey("AdmissionDetailsId");

                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmissionSystem2.Entites.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId");

                    b.HasOne("AdmissionSystem2.Entites.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.Navigation("AdmissionDetails");

                    b.Navigation("Applicant");

                    b.Navigation("MedicalHistory");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Document", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany("Documents")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmissionSystem2.Entites.Application", null)
                        .WithMany("Documents")
                        .HasForeignKey("ApplicationId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.EmergencyContact", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany("EmergencyContact")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmissionSystem2.Entites.Application", null)
                        .WithMany("EmergencyContact")
                        .HasForeignKey("ApplicationId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.FamilyStatus", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithOne("Family_Status")
                        .HasForeignKey("AdmissionSystem2.Entites.FamilyStatus", "ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.MedicalHistory", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithOne("MedicalHistory")
                        .HasForeignKey("AdmissionSystem2.Entites.MedicalHistory", "ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.ParentInfo", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany("ParentInfo")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmissionSystem2.Entites.Application", null)
                        .WithMany("ParentInfo")
                        .HasForeignKey("ApplicationId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Payment", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithOne("Payment")
                        .HasForeignKey("AdmissionSystem2.Entites.Payment", "ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Sibling", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany("Sibling")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdmissionSystem2.Entites.Application", null)
                        .WithMany("Sibling")
                        .HasForeignKey("ApplicationId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.TransferredStudent", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.AdmissionDetails", null)
                        .WithOne()
                        .HasForeignKey("AdmissionSystem2.Entites.TransferredStudent", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.BankElahly", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Payment", null)
                        .WithOne()
                        .HasForeignKey("AdmissionSystem2.Entites.BankElahly", "PaymentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Fawry", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Payment", null)
                        .WithOne()
                        .HasForeignKey("AdmissionSystem2.Entites.Fawry", "PaymentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.MasterCard", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Payment", null)
                        .WithOne()
                        .HasForeignKey("AdmissionSystem2.Entites.MasterCard", "PaymentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Applicant", b =>
                {
                    b.Navigation("AdmissionDetails");

                    b.Navigation("Documents");

                    b.Navigation("EmergencyContact");

                    b.Navigation("Family_Status");

                    b.Navigation("MedicalHistory");

                    b.Navigation("ParentInfo");

                    b.Navigation("Payment");

                    b.Navigation("Sibling");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Application", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("EmergencyContact");

                    b.Navigation("ParentInfo");

                    b.Navigation("Sibling");
                });
#pragma warning restore 612, 618
        }
    }
}
