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
    [Migration("20240120044419_addothers")]
    partial class addothers
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
                        .HasMaxLength(20)
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

                    b.Property<int>("PositionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sexo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sprite")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VitalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountEntityId");

                    b.HasIndex("PositionId");

                    b.HasIndex("StatId");

                    b.HasIndex("VitalId");

                    b.ToTable("PlayerEntities");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Equipment", b =>
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

            modelBuilder.Entity("Database.Entities.ValueObjects.Position", b =>
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

            modelBuilder.Entity("Database.Entities.ValueObjects.Stat", b =>
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

            modelBuilder.Entity("Database.Entities.ValueObjects.Vital", b =>
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

                    b.HasOne("Database.Entities.ValueObjects.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.ValueObjects.Stat", "Stat")
                        .WithMany()
                        .HasForeignKey("StatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.ValueObjects.Vital", "Vital")
                        .WithMany()
                        .HasForeignKey("VitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Stat");

                    b.Navigation("Vital");
                });

            modelBuilder.Entity("Database.Entities.ValueObjects.Equipment", b =>
                {
                    b.HasOne("Database.Entities.Player.PlayerEntity", null)
                        .WithMany("Equipment")
                        .HasForeignKey("PlayerEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Database.Entities.Account.AccountEntity", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Database.Entities.Player.PlayerEntity", b =>
                {
                    b.Navigation("Equipment");
                });
#pragma warning restore 612, 618
        }
    }
}
