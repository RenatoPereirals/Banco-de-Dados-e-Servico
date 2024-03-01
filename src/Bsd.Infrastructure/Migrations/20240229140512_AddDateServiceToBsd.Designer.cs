﻿// <auto-generated />
using System;
using Bsd.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bsd.Infrastructure.Migrations
{
    [DbContext(typeof(BsdDbContext))]
    [Migration("20240229140512_AddDateServiceToBsd")]
    partial class AddDateServiceToBsd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Bsd.Domain.Entities.BsdEntity", b =>
                {
                    b.Property<int>("BsdNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateService")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayType")
                        .HasColumnType("INTEGER");

                    b.HasKey("BsdNumber");

                    b.ToTable("BsdEntities");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Registration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateService")
                        .HasColumnType("TEXT");

                    b.Property<int>("Digit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ServiceType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Registration");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.EmployeeBsdEntity", b =>
                {
                    b.Property<int>("BsdEntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeRegistration")
                        .HasColumnType("INTEGER");

                    b.HasKey("BsdEntityNumber", "EmployeeRegistration");

                    b.HasIndex("EmployeeRegistration");

                    b.ToTable("EmployeesBsdEntities");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.Rubric", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("EmployeeBsdEntityBsdEntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EmployeeBsdEntityEmployeeRegistration")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("HoursPerDay")
                        .HasColumnType("TEXT");

                    b.Property<int>("ServiceType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Code");

                    b.HasIndex("EmployeeBsdEntityBsdEntityNumber", "EmployeeBsdEntityEmployeeRegistration");

                    b.ToTable("Rubrics");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.EmployeeBsdEntity", b =>
                {
                    b.HasOne("Bsd.Domain.Entities.BsdEntity", "BsdEntity")
                        .WithMany("EmployeeBsdEntities")
                        .HasForeignKey("BsdEntityNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bsd.Domain.Entities.Employee", "Employee")
                        .WithMany("EmployeeBsdEntities")
                        .HasForeignKey("EmployeeRegistration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BsdEntity");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.Rubric", b =>
                {
                    b.HasOne("Bsd.Domain.Entities.EmployeeBsdEntity", null)
                        .WithMany("Rubrics")
                        .HasForeignKey("EmployeeBsdEntityBsdEntityNumber", "EmployeeBsdEntityEmployeeRegistration");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.BsdEntity", b =>
                {
                    b.Navigation("EmployeeBsdEntities");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.Employee", b =>
                {
                    b.Navigation("EmployeeBsdEntities");
                });

            modelBuilder.Entity("Bsd.Domain.Entities.EmployeeBsdEntity", b =>
                {
                    b.Navigation("Rubrics");
                });
#pragma warning restore 612, 618
        }
    }
}
