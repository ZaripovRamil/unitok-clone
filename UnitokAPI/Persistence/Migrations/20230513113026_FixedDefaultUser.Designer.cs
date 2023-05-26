﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230513113026_FixedDefaultUser")]
    partial class FixedDefaultUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.ActivityLogs.AuctionFinishLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuctionId")
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("SellerId")
                        .HasColumnType("text");

                    b.Property<string>("TokenId")
                        .HasColumnType("text");

                    b.Property<string>("WinnerId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("SellerId");

                    b.HasIndex("TokenId");

                    b.HasIndex("WinnerId");

                    b.ToTable("AuctionFinishLogs");
                });

            modelBuilder.Entity("Domain.ActivityLogs.AuctionParticipationLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuctionId")
                        .HasColumnType("text");

                    b.Property<string>("BidderId")
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<bool>("HasWon")
                        .HasColumnType("boolean");

                    b.Property<string>("TokenId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("BidderId");

                    b.HasIndex("TokenId");

                    b.ToTable("AuctionParticipationLogs");
                });

            modelBuilder.Entity("Domain.ActivityLogs.TokenCreationLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TokenId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TokenId");

                    b.ToTable("TokenCreationLogs");
                });

            modelBuilder.Entity("Domain.Entities.Auction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("BestPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Finish")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("StartPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("TokenId")
                        .HasColumnType("text");

                    b.Property<string>("WinnerId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TokenId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("Domain.Entities.Bid", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuctionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BidTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BidderId")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("BidderId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("Domain.Entities.Token", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfilePicUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Seed")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Wallet")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "defaultuser",
                            AccessFailedCount = 0,
                            Balance = 0m,
                            ConcurrencyStamp = "bcec8ad6-b1c6-47f2-8c46-d459b2e68367",
                            Email = "",
                            EmailConfirmed = false,
                            FirstName = "Ramil",
                            LastName = "Zaripov",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            ProfilePicUrl = "img/avatars/avatar.jpg",
                            Role = 0,
                            SecurityStamp = "f5204ca7-7a5c-4ca1-a937-bef028f1cfd7",
                            TwoFactorEnabled = false,
                            UserName = "rzaripov"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.ActivityLogs.AuctionFinishLog", b =>
                {
                    b.HasOne("Domain.Entities.Auction", "Auction")
                        .WithMany()
                        .HasForeignKey("AuctionId");

                    b.HasOne("Domain.Entities.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.HasOne("Domain.Entities.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.HasOne("Domain.Entities.User", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("Auction");

                    b.Navigation("Seller");

                    b.Navigation("Token");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("Domain.ActivityLogs.AuctionParticipationLog", b =>
                {
                    b.HasOne("Domain.Entities.Auction", "Auction")
                        .WithMany()
                        .HasForeignKey("AuctionId");

                    b.HasOne("Domain.Entities.User", "Bidder")
                        .WithMany()
                        .HasForeignKey("BidderId");

                    b.HasOne("Domain.Entities.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.Navigation("Auction");

                    b.Navigation("Bidder");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Domain.ActivityLogs.TokenCreationLog", b =>
                {
                    b.HasOne("Domain.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Domain.Entities.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.Navigation("Author");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Domain.Entities.Auction", b =>
                {
                    b.HasOne("Domain.Entities.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.HasOne("Domain.Entities.User", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("Token");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("Domain.Entities.Bid", b =>
                {
                    b.HasOne("Domain.Entities.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Bidder")
                        .WithMany()
                        .HasForeignKey("BidderId");

                    b.Navigation("Auction");

                    b.Navigation("Bidder");
                });

            modelBuilder.Entity("Domain.Entities.Token", b =>
                {
                    b.HasOne("Domain.Entities.User", "Author")
                        .WithMany("CreatedTokens")
                        .HasForeignKey("AuthorId");

                    b.HasOne("Domain.Entities.User", "Owner")
                        .WithMany("OwnedTokens")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Author");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Auction", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("CreatedTokens");

                    b.Navigation("OwnedTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
