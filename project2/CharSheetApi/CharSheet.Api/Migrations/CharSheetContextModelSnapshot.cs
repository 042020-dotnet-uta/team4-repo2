﻿// <auto-generated />
using System;
using CharSheet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharSheet.Api.Migrations
{
    [DbContext(typeof(CharSheetContext))]
    partial class CharSheetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CharSheet.Domain.FormInput", b =>
                {
                    b.Property<Guid>("FormInputId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormInputGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormInputId");

                    b.HasIndex("FormInputGroupId");

                    b.ToTable("FormInputs");
                });

            modelBuilder.Entity("CharSheet.Domain.FormInputGroup", b =>
                {
                    b.Property<Guid>("FormInputGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SheetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FormInputGroupId");

                    b.HasIndex("FormTemplateId");

                    b.HasIndex("SheetId");

                    b.ToTable("FormInputGroups");
                });

            modelBuilder.Entity("CharSheet.Domain.FormPosition", b =>
                {
                    b.Property<Guid>("FormPostionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("FormPostionId");

                    b.ToTable("FormPositions");
                });

            modelBuilder.Entity("CharSheet.Domain.FormTemplate", b =>
                {
                    b.Property<Guid>("FormTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormPositionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormTemplateId");

                    b.HasIndex("FormPositionId");

                    b.HasIndex("TemplateId");

                    b.ToTable("FormTemplates");
                });

            modelBuilder.Entity("CharSheet.Domain.Login", b =>
                {
                    b.Property<Guid>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Hashed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IterationCount")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("CharSheet.Domain.Sheet", b =>
                {
                    b.Property<Guid>("SheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SheetId");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("CharSheet.Domain.Template", b =>
                {
                    b.Property<Guid>("TemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TemplateId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("CharSheet.Domain.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("LoginId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CharSheet.FormLabel", b =>
                {
                    b.Property<Guid>("FormLabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormLabelId");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("FormLabels");
                });

            modelBuilder.Entity("CharSheet.Domain.FormInput", b =>
                {
                    b.HasOne("CharSheet.Domain.FormInputGroup", null)
                        .WithMany("FormInputs")
                        .HasForeignKey("FormInputGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharSheet.Domain.FormInputGroup", b =>
                {
                    b.HasOne("CharSheet.Domain.FormTemplate", "FormTemplate")
                        .WithMany()
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CharSheet.Domain.Sheet", null)
                        .WithMany("FormInputGroups")
                        .HasForeignKey("SheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharSheet.Domain.FormTemplate", b =>
                {
                    b.HasOne("CharSheet.Domain.FormPosition", "FormPosition")
                        .WithMany()
                        .HasForeignKey("FormPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CharSheet.Domain.Template", null)
                        .WithMany("FormTemplates")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharSheet.Domain.User", b =>
                {
                    b.HasOne("CharSheet.Domain.Login", "Login")
                        .WithMany()
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharSheet.FormLabel", b =>
                {
                    b.HasOne("CharSheet.Domain.FormTemplate", null)
                        .WithMany("FormLabels")
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
