﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingDiary.Models;

namespace TrainingDiary.Migrations
{
    [DbContext(typeof(TrainingDiaryContext))]
    [Migration("20191130164127_ChangeDateTimeToTimeSpan")]
    partial class ChangeDateTimeToTimeSpan
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TrainingDiary.Models.BaseEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoachId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("EventName");

                    b.Property<string>("PlayerId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("PlayerId");

                    b.ToTable("BaseEvent");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseEvent");
                });

            modelBuilder.Entity("TrainingDiary.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("ReceiveDate");

                    b.Property<string>("ReceiverId");

                    b.Property<DateTime>("SendDate");

                    b.Property<string>("SenderId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Message");
                });

            modelBuilder.Entity("TrainingDiary.Models.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BPG");

                    b.Property<string>("Comment");

                    b.Property<string>("Content");

                    b.Property<float>("DL");

                    b.Property<float>("GL");

                    b.Property<float>("KMStart");

                    b.Property<float>("KR");

                    b.Property<float>("MS");

                    b.Property<float>("MaxSpeed");

                    b.Property<float>("MultipleJump");

                    b.Property<float>("OWB1");

                    b.Property<float>("OWB2");

                    b.Property<float>("RelativeSpeed");

                    b.Property<float>("Rhythm");

                    b.Property<float>("Scamper");

                    b.Property<float>("Skip");

                    b.Property<float>("WB3");

                    b.Property<float>("ZB");

                    b.HasKey("Id");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("TrainingDiary.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("TrainingDiary.Models.Camp", b =>
                {
                    b.HasBaseType("TrainingDiary.Models.BaseEvent");

                    b.Property<string>("Location");

                    b.HasDiscriminator().HasValue("Camp");
                });

            modelBuilder.Entity("TrainingDiary.Models.FullWorkout", b =>
                {
                    b.HasBaseType("TrainingDiary.Models.BaseEvent");

                    b.Property<int?>("CampId");

                    b.Property<int?>("PlanId");

                    b.Property<int?>("RealizationId");

                    b.HasIndex("CampId");

                    b.HasIndex("PlanId");

                    b.HasIndex("RealizationId");

                    b.HasDiscriminator().HasValue("FullWorkout");
                });

            modelBuilder.Entity("TrainingDiary.Models.Start", b =>
                {
                    b.HasBaseType("TrainingDiary.Models.BaseEvent");

                    b.Property<string>("CoachComment");

                    b.Property<float>("Distance");

                    b.Property<string>("Location")
                        .HasColumnName("Start_Location");

                    b.Property<string>("Place");

                    b.Property<string>("PlayerComment");

                    b.Property<TimeSpan>("Result");

                    b.HasDiscriminator().HasValue("Start");
                });

            modelBuilder.Entity("TrainingDiary.Models.Request", b =>
                {
                    b.HasBaseType("TrainingDiary.Models.Message");

                    b.Property<int>("RequestStatus");

                    b.HasDiscriminator().HasValue("Request");
                });

            modelBuilder.Entity("TrainingDiary.Models.Coach", b =>
                {
                    b.HasBaseType("TrainingDiary.Models.User");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("TrainingDiary.Models.Player", b =>
                {
                    b.HasBaseType("TrainingDiary.Models.User");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("CoachId");

                    b.Property<DateTime>("ExaminationDeadline");

                    b.Property<int>("Height");

                    b.Property<bool>("Injured");

                    b.Property<int>("LicenceNo");

                    b.Property<int>("ShoeSize");

                    b.Property<int>("Size");

                    b.Property<int>("StartNo");

                    b.Property<float>("Weight");

                    b.HasIndex("CoachId");

                    b.HasDiscriminator().HasValue("Player");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrainingDiary.Models.BaseEvent", b =>
                {
                    b.HasOne("TrainingDiary.Models.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId");

                    b.HasOne("TrainingDiary.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("TrainingDiary.Models.Message", b =>
                {
                    b.HasOne("TrainingDiary.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId");

                    b.HasOne("TrainingDiary.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("TrainingDiary.Models.FullWorkout", b =>
                {
                    b.HasOne("TrainingDiary.Models.Camp")
                        .WithMany("TrainingList")
                        .HasForeignKey("CampId");

                    b.HasOne("TrainingDiary.Models.Workout", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId");

                    b.HasOne("TrainingDiary.Models.Workout", "Realization")
                        .WithMany()
                        .HasForeignKey("RealizationId");
                });

            modelBuilder.Entity("TrainingDiary.Models.Player", b =>
                {
                    b.HasOne("TrainingDiary.Models.Coach", "Coach")
                        .WithMany("PlayersList")
                        .HasForeignKey("CoachId");
                });
#pragma warning restore 612, 618
        }
    }
}
