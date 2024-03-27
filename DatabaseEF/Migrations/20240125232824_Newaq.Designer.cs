﻿// <auto-generated />
using System;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20240125232824_Newaq")]
    partial class Newaq
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Database.Entities.Account.AccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AccountEntities");
                });

            modelBuilder.Entity("Database.Entities.Player.PlayerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Entidade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Exp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sexo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sprite")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountEntityId");

                    b.ToTable("PlayerEntities");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Bounding")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Bounding")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Bounding")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Penalty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isBanned")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isMuted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Penalty");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MapNum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MapX")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MapY")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillUses")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Stat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Agility")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Endurance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intelligence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WillPower")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Stat");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Vital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurEnergy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurHealth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxEnergy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxHealth")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Vital");
                });

            modelBuilder.Entity("Database.Entities.Player.PlayerEntity", b =>
                {
                    b.HasOne("Database.Entities.Account.AccountEntity", null)
                        .WithMany("Players")
                        .HasForeignKey("AccountEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Bank", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithMany("Bank")
                        .HasForeignKey("PlayerEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Equipment", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithMany("Equipment")
                        .HasForeignKey("PlayerEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Inventory", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithMany("Inventory")
                        .HasForeignKey("PlayerEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Penalty", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithOne("Penalty")
                        .HasForeignKey("Database.Entities.ValueObjects.Player.Penalty", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Position", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithOne("Position")
                        .HasForeignKey("Database.Entities.ValueObjects.Player.Position", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Skill", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithMany("Skill")
                        .HasForeignKey("PlayerEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Stat", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithOne("Stat")
                        .HasForeignKey("Database.Entities.ValueObjects.Player.Stat", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Player.Vital", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithOne("Vital")
                        .HasForeignKey("Database.Entities.ValueObjects.Player.Vital", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.Account.AccountEntity", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Database.Entities.Player.PlayerEntity", b =>
                {
                    b.Navigation("Bank");

                    b.Navigation("Equipment");

                    b.Navigation("Inventory");

                    b.Navigation("Penalty")
                        .IsRequired();

                    b.Navigation("Position")
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Stat")
                        .IsRequired();

                    b.Navigation("Vital")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
