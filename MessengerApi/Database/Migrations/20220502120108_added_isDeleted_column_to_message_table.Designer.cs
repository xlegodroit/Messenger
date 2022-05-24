﻿// <auto-generated />
using System;
using MessengerApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MessengerApi.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220502120108_added_isDeleted_column_to_message_table")]
    partial class added_isDeleted_column_to_message_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MessengerApi.Database.Models.Blockade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("IdBlocked")
                        .HasColumnType("int");

                    b.Property<int>("IdBlocker")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Blockade");

                    b.HasIndex("IdBlocked");

                    b.HasIndex("IdBlocker");

                    b.ToTable("Blockade", (string)null);
                });

            modelBuilder.Entity("MessengerApi.Database.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(32)");

                    b.HasKey("Id")
                        .HasName("PK_Chat");

                    b.ToTable("Chat", (string)null);
                });

            modelBuilder.Entity("MessengerApi.Database.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdChat")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id")
                        .HasName("PK_Message");

                    b.HasIndex("IdChat");

                    b.HasIndex("IdUser");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("MessengerApi.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EmailVerificationToken")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("User_pk");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MessengerApi.Database.Models.UserChat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdChat")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_User_Chat");

                    b.HasIndex("IdChat");

                    b.HasIndex("IdUser");

                    b.ToTable("User_Chat", (string)null);
                });

            modelBuilder.Entity("MessengerApi.Database.Models.Blockade", b =>
                {
                    b.HasOne("MessengerApi.Database.Models.User", "IdBlockedNavigation")
                        .WithMany("ReceivedBlockades")
                        .HasForeignKey("IdBlocked")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MessengerApi.Database.Models.User", "IdBlockerNavigation")
                        .WithMany("CreatedBlockades")
                        .HasForeignKey("IdBlocker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdBlockedNavigation");

                    b.Navigation("IdBlockerNavigation");
                });

            modelBuilder.Entity("MessengerApi.Database.Models.Message", b =>
                {
                    b.HasOne("MessengerApi.Database.Models.Chat", "IdChatNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("IdChat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MessengerApi.Database.Models.User", "IdUserNavigation")
                        .WithMany("SentMessages")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdChatNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("MessengerApi.Database.Models.UserChat", b =>
                {
                    b.HasOne("MessengerApi.Database.Models.Chat", "IdChatNavigation")
                        .WithMany("UserChats")
                        .HasForeignKey("IdChat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MessengerApi.Database.Models.User", "IdUserNavigation")
                        .WithMany("UserChats")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdChatNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("MessengerApi.Database.Models.Chat", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserChats");
                });

            modelBuilder.Entity("MessengerApi.Database.Models.User", b =>
                {
                    b.Navigation("CreatedBlockades");

                    b.Navigation("ReceivedBlockades");

                    b.Navigation("SentMessages");

                    b.Navigation("UserChats");
                });
#pragma warning restore 612, 618
        }
    }
}