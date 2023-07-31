﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RXApp;

#nullable disable

namespace RXApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230731161142_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RXApp.Model.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Article")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Copies")
                        .HasColumnType("int");

                    b.Property<int>("CreateDate")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("RXApp.Model.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("ReaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("IssueId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("RXApp.Model.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("RXApp.Model.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BornDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("RXApp.Model.History", b =>
                {
                    b.HasOne("RXApp.Model.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RXApp.Model.Issue", "Issue")
                        .WithMany("Histories")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RXApp.Model.Reader", "Reader")
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Issue");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("RXApp.Model.Issue", b =>
                {
                    b.HasOne("RXApp.Model.Book", "Book")
                        .WithMany("Issues")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RXApp.Model.Reader", "Reader")
                        .WithMany("Issues")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("RXApp.Model.Book", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("RXApp.Model.Issue", b =>
                {
                    b.Navigation("Histories");
                });

            modelBuilder.Entity("RXApp.Model.Reader", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
