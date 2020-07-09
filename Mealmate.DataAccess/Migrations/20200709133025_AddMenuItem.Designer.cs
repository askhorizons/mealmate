﻿// <auto-generated />
using System;
using Mealmate.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mealmate.DataAccess.Migrations
{
    [DbContext(typeof(MealmateDbContext))]
    [Migration("20200709133025_AddMenuItem")]
    partial class AddMenuItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(350)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("VARCHAR(25)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.HasKey("Id");

                    b.ToTable("User","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserToken", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginProvider", "UserId", "Name");

                    b.HasIndex("UserId");

                    b.ToTable("UserToken","Identity");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("NVARCHAR(1000)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INT");

                    b.HasKey("BranchId")
                        .HasName("PK_Branch");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Branch","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("INT");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.HasKey("LocationId")
                        .HasName("PK_Location");

                    b.HasIndex("BranchId");

                    b.ToTable("Location","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("INT");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<TimeSpan>("ServiceTime")
                        .HasColumnType("TIME(7)");

                    b.HasKey("MenuId")
                        .HasName("PK_Menu");

                    b.HasIndex("BranchId");

                    b.ToTable("Menu","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuId")
                        .HasColumnType("INT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MenuItemId")
                        .HasName("PK_MenuItem");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItem","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.QRCode", b =>
                {
                    b.Property<int>("QRCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Code")
                        .IsRequired()
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("TableId")
                        .HasColumnType("INT");

                    b.HasKey("QRCodeId")
                        .HasName("PK_QRCode");

                    b.HasIndex("TableId");

                    b.ToTable("QRCode","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("RestaurantId")
                        .HasName("PK_Restaurant");

                    b.HasIndex("OwnerId");

                    b.ToTable("Restaurant","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIMEOFFSET")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("LocationId")
                        .HasColumnType("INT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)");

                    b.HasKey("TableId")
                        .HasName("PK_Table");

                    b.HasIndex("LocationId");

                    b.ToTable("Table","Mealmate");
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.RoleClaim", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Identity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserClaim", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Identity.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserLogin", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Identity.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mealmate.DataAccess.Entities.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Identity.UserToken", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Identity.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Branch", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Mealmate.Restaurant", "Restaurant")
                        .WithMany("Branches")
                        .HasForeignKey("RestaurantId")
                        .HasConstraintName("FK_Branch_Restaurant")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Location", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Mealmate.Branch", "Branch")
                        .WithMany("Locations")
                        .HasForeignKey("BranchId")
                        .HasConstraintName("FK_Location_Branch")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Menu", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Mealmate.Branch", "Branch")
                        .WithMany("Menus")
                        .HasForeignKey("BranchId")
                        .HasConstraintName("FK_Menu_Branch")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.MenuItem", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Mealmate.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("FK_MenuItem_Menu")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.QRCode", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Mealmate.Table", "Table")
                        .WithMany("QRCodes")
                        .HasForeignKey("TableId")
                        .HasConstraintName("FK_QRCode_Table")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Restaurant", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Identity.User", "Owner")
                        .WithMany("Restaurants")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK_Restaurant_User")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Mealmate.DataAccess.Entities.Mealmate.Table", b =>
                {
                    b.HasOne("Mealmate.DataAccess.Entities.Mealmate.Location", "Location")
                        .WithMany("Tables")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK_Table_Location")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}