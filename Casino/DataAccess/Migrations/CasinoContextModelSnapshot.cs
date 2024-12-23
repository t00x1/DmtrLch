﻿// <auto-generated />
using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(CasinoContext))]
    partial class CasinoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Balance", b =>
                {
                    b.Property<string>("IdOfUser")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_Of_User");

                    b.Property<int>("Balance1")
                        .HasColumnType("int")
                        .HasColumnName("Balance");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfUser")
                        .HasName("PK__Balance__93F2FC91C637AF8A");

                    b.ToTable("Balance", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Config", b =>
                {
                    b.Property<string>("IdOfUser")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_user");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool>("DarkTheme")
                        .HasColumnType("bit")
                        .HasColumnName("Dark_theme");

                    b.Property<bool>("Notifications")
                        .HasColumnType("bit");

                    b.Property<bool>("SaveHistory")
                        .HasColumnType("bit")
                        .HasColumnName("Save_History");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfUser")
                        .HasName("PK__Config__23D22D0A03C2C6DD");

                    b.ToTable("Config", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Cupon", b =>
                {
                    b.Property<string>("IdOfCupon")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_cupon");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("ExpireAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(dateadd(hour,(1),getdate()))");

                    b.Property<bool>("Reusable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("IdOfCupon")
                        .HasName("PK__Cupons__B703844AC53B3A36");

                    b.ToTable("Cupons");
                });

            modelBuilder.Entity("Domain.Models.CuponsUsed", b =>
                {
                    b.Property<string>("IdOfCupon")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_cupon");

                    b.Property<string>("IdOfUser")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_user");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfCupon", "IdOfUser")
                        .HasName("PK__Cupons_U__053EA69A1C16D58C");

                    b.HasIndex("IdOfUser");

                    b.ToTable("Cupons_Used", (string)null);
                });

            modelBuilder.Entity("Domain.Models.EmailConfirmation", b =>
                {
                    b.Property<string>("IdReq")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_req");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Deleted_at");

                    b.Property<DateTime>("Expire")
                        .HasColumnType("datetime");

                    b.Property<string>("IdOfUser")
                        .IsRequired()
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_user");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdReq")
                        .HasName("PK__EmailCon__182A6452D3D54077");

                    b.HasIndex("IdOfUser");

                    b.ToTable("EmailConfirmation", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Game", b =>
                {
                    b.Property<string>("IdOfGame")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_Game");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("TypeOfGame")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Type_Of_Game");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfGame")
                        .HasName("PK__Game__F187176F416BA7CC");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Image", b =>
                {
                    b.Property<string>("IdOfImage")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_of_image");

                    b.Property<string>("Directory")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdOfImage")
                        .HasName("PK__Images__9347264A4B05F199");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Models.Participation", b =>
                {
                    b.Property<string>("IdOfUser")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_Of_User");

                    b.Property<string>("IdOfRoom")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_Of_Room");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfUser", "IdOfRoom")
                        .HasName("PK__Particip__CB4A5550C652ACCB");

                    b.HasIndex("IdOfRoom");

                    b.ToTable("Participation", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Property<string>("IdOfRoom")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_Of_Room");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("IdOfGame")
                        .IsRequired()
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_Of_Game");

                    b.Property<string>("NameOfRoom")
                        .IsRequired()
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Name_Of_Room");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfRoom")
                        .HasName("PK__Room__8B8A9C144CAB0E88");

                    b.HasIndex("IdOfGame");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Session", b =>
                {
                    b.Property<string>("IdOfSession")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_session");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("IdOfUser")
                        .IsRequired()
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_user");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfSession")
                        .HasName("PK__Sessions__7599B219643FFE3C");

                    b.HasIndex("IdOfUser");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Domain.Models.Statistic", b =>
                {
                    b.Property<string>("IdOfUser")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("Id_Of_User");

                    b.Property<int>("Bet")
                        .HasColumnType("int");

                    b.Property<int>("BetsWon")
                        .HasColumnType("int")
                        .HasColumnName("Bets_Won");

                    b.Property<int>("ChipsWon")
                        .HasColumnType("int")
                        .HasColumnName("Chips_Won");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("IdOfUser")
                        .HasName("PK__Statisti__93F2FC91D05D0875");

                    b.ToTable("Statistic", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<string>("IdOfUser")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_user");

                    b.Property<bool?>("Admin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Avatar")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)");

                    b.Property<string>("Bio")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("ConfirmedEmail")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Location")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("Male")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdOfUser")
                        .HasName("PK__Users__23D22D0A380B4478");

                    b.HasIndex("Avatar");

                    b.HasIndex(new[] { "UserName" }, "UQ__Users__C9F2845639396034")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.WonBet", b =>
                {
                    b.Property<string>("IdOfGame")
                        .HasMaxLength(54)
                        .HasColumnType("nvarchar(54)")
                        .HasColumnName("ID_of_Game");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("WonWeb")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdOfGame")
                        .HasName("PK__WonBet__F187176FFF10317D");

                    b.ToTable("WonBet", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Balance", b =>
                {
                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithOne("Balance")
                        .HasForeignKey("Domain.Models.Balance", "IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__Balance__Id_Of_U__6D2D2E85");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.Config", b =>
                {
                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithOne("Config")
                        .HasForeignKey("Domain.Models.Config", "IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__Config__ID_of_us__5A1A5A11");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.CuponsUsed", b =>
                {
                    b.HasOne("Domain.Models.Cupon", "IdOfCuponNavigation")
                        .WithMany("CuponsUseds")
                        .HasForeignKey("IdOfCupon")
                        .IsRequired()
                        .HasConstraintName("FK__Cupons_Us__ID_of__4AD81681");

                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithMany("CuponsUseds")
                        .HasForeignKey("IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__Cupons_Us__ID_of__4BCC3ABA");

                    b.Navigation("IdOfCuponNavigation");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.EmailConfirmation", b =>
                {
                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithMany("EmailConfirmations")
                        .HasForeignKey("IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__EmailConf__ID_of__5090EFD7");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.Participation", b =>
                {
                    b.HasOne("Domain.Models.Room", "IdOfRoomNavigation")
                        .WithMany("Participations")
                        .HasForeignKey("IdOfRoom")
                        .IsRequired()
                        .HasConstraintName("FK__Participa__Id_Of__7A8729A3");

                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithMany("Participations")
                        .HasForeignKey("IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__Participa__Id_Of__7993056A");

                    b.Navigation("IdOfRoomNavigation");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.HasOne("Domain.Models.Game", "IdOfGameNavigation")
                        .WithMany("Rooms")
                        .HasForeignKey("IdOfGame")
                        .IsRequired()
                        .HasConstraintName("FK__Room__Id_Of_Game__6774552F");

                    b.Navigation("IdOfGameNavigation");
                });

            modelBuilder.Entity("Domain.Models.Session", b =>
                {
                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithMany("Sessions")
                        .HasForeignKey("IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__Sessions__ID_of___5555A4F4");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.Statistic", b =>
                {
                    b.HasOne("Domain.Models.User", "IdOfUserNavigation")
                        .WithOne("Statistic")
                        .HasForeignKey("Domain.Models.Statistic", "IdOfUser")
                        .IsRequired()
                        .HasConstraintName("FK__Statistic__Id_Of__74CE504D");

                    b.Navigation("IdOfUserNavigation");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasOne("Domain.Models.Image", "AvatarNavigation")
                        .WithMany("Users")
                        .HasForeignKey("Avatar")
                        .HasConstraintName("FK__Users__Avatar__46136164");

                    b.Navigation("AvatarNavigation");
                });

            modelBuilder.Entity("Domain.Models.WonBet", b =>
                {
                    b.HasOne("Domain.Models.Game", "IdOfGameNavigation")
                        .WithOne("WonBet")
                        .HasForeignKey("Domain.Models.WonBet", "IdOfGame")
                        .IsRequired()
                        .HasConstraintName("FK__WonBet__ID_of_Ga__62AFA012");

                    b.Navigation("IdOfGameNavigation");
                });

            modelBuilder.Entity("Domain.Models.Cupon", b =>
                {
                    b.Navigation("CuponsUseds");
                });

            modelBuilder.Entity("Domain.Models.Game", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("WonBet");
                });

            modelBuilder.Entity("Domain.Models.Image", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Navigation("Participations");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Balance");

                    b.Navigation("Config");

                    b.Navigation("CuponsUseds");

                    b.Navigation("EmailConfirmations");

                    b.Navigation("Participations");

                    b.Navigation("Sessions");

                    b.Navigation("Statistic");
                });
#pragma warning restore 612, 618
        }
    }
}
