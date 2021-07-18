﻿// <auto-generated />
using System;
using AdmissionSystem2.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdmissionSystem2.Migrations
{
    [DbContext(typeof(AdmissionSystemDbContext))]
    [Migration("20210717142207_addFamilyStatus")]
    partial class addFamilyStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("AdmissionSystem2.Entites.Applicant", b =>
                {
                    b.Property<int>("ApplicantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyStatus")
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

                    b.Property<string>("Relegion")
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

            modelBuilder.Entity("AdmissionSystem2.Entites.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Copy")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DocumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.EmergencyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicantId")
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

                    b.ToTable("EmergencyContact");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.FamilyStatus", b =>
                {
                    b.Property<string>("Guardian")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

                    b.Property<string>("GuardianAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageSpoken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guardian");

                    b.HasIndex("ApplicantId")
                        .IsUnique();

                    b.ToTable("FamilyStatues");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.MedicalHistory", b =>
                {
                    b.Property<Guid>("MedicalHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

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

                    b.Property<int>("ApplicantId")
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

                    b.ToTable("ParentInfo");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

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

                    b.Property<int>("ApplicantId")
                        .HasColumnType("int");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiblingName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SibilingId");

                    b.HasIndex("ApplicantId");

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

            modelBuilder.Entity("AdmissionSystem2.Entites.Document", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany("Documents")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AdmissionSystem2.Entites.EmergencyContact", b =>
                {
                    b.HasOne("AdmissionSystem2.Entites.Applicant", "Applicant")
                        .WithMany("EmergencyContact")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
#pragma warning restore 612, 618
        }
    }
}
