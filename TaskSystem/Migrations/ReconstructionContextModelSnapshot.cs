﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskSystem.Core;

#nullable disable

namespace TaskSystem.Migrations
{
    [DbContext(typeof(ReconstructionContext))]
    partial class ReconstructionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8_general_ci")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb3");

            modelBuilder.Entity("TaskSystem.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("TaskSystem.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int?>("PerformerId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CustomerId" }, "Task_Customer_FK");

                    b.HasIndex(new[] { "PerformerId" }, "Task_Performer_FK");

                    b.HasIndex(new[] { "StatusId" }, "Task_Status_FK");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("TaskSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("TaskSystem.Models.Task", b =>
                {
                    b.HasOne("TaskSystem.Models.User", "Customer")
                        .WithMany("TaskCustomers")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("Task_Customer_FK");

                    b.HasOne("TaskSystem.Models.User", "Performer")
                        .WithMany("TaskPerformers")
                        .HasForeignKey("PerformerId")
                        .HasConstraintName("Task_Performer_FK");

                    b.HasOne("TaskSystem.Models.Status", "Status")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusId")
                        .IsRequired()
                        .HasConstraintName("Task_Status_FK");

                    b.Navigation("Customer");

                    b.Navigation("Performer");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TaskSystem.Models.Status", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskSystem.Models.User", b =>
                {
                    b.Navigation("TaskCustomers");

                    b.Navigation("TaskPerformers");
                });
#pragma warning restore 612, 618
        }
    }
}