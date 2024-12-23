using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{


    public partial class CasinoContext : DbContext
    {
        public CasinoContext()
        {
        }

        public CasinoContext(DbContextOptions<CasinoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Balance> Balances { get; set; }

        public virtual DbSet<Config> Configs { get; set; }

        public virtual DbSet<Cupon> Cupons { get; set; }

        public virtual DbSet<CuponsUsed> CuponsUseds { get; set; }

        public virtual DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Participation> Participations { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<Statistic> Statistics { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<WonBet> WonBets { get; set; }

    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //         => optionsBuilder.UseSqlServer("Server=DESKTOP-T6LU3PA;Database=Casino;Integrated Security=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.IdOfUser).HasName("PK__Balance__93F2FC91C637AF8A");

                entity.ToTable("Balance");

                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("Id_Of_User");
                entity.Property(e => e.Balance1).HasColumnName("Balance");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfUserNavigation).WithOne(p => p.Balance)
                    .HasForeignKey<Balance>(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Balance__Id_Of_U__6D2D2E85");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasKey(e => e.IdOfUser).HasName("PK__Config__23D22D0A03C2C6DD");

                entity.ToTable("Config");

                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_user");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.DarkTheme).HasColumnName("Dark_theme");
                entity.Property(e => e.SaveHistory).HasColumnName("Save_History");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfUserNavigation).WithOne(p => p.Config)
                    .HasForeignKey<Config>(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Config__ID_of_us__5A1A5A11");
            });

            modelBuilder.Entity<Cupon>(entity =>
            {
                entity.HasKey(e => e.IdOfCupon).HasName("PK__Cupons__B703844AC53B3A36");

                entity.Property(e => e.IdOfCupon)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_cupon");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.ExpireAt)
                    .HasDefaultValueSql("(dateadd(hour,(1),getdate()))")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");
            });

            modelBuilder.Entity<CuponsUsed>(entity =>
            {
                entity.HasKey(e => new { e.IdOfCupon, e.IdOfUser }).HasName("PK__Cupons_U__053EA69A1C16D58C");

                entity.ToTable("Cupons_Used");

                entity.Property(e => e.IdOfCupon)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_cupon");
                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_user");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfCuponNavigation).WithMany(p => p.CuponsUseds)
                    .HasForeignKey(d => d.IdOfCupon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cupons_Us__ID_of__4AD81681");

                entity.HasOne(d => d.IdOfUserNavigation).WithMany(p => p.CuponsUseds)
                    .HasForeignKey(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cupons_Us__ID_of__4BCC3ABA");
            });

            modelBuilder.Entity<EmailConfirmation>(entity =>
            {
                entity.HasKey(e => e.IdReq).HasName("PK__EmailCon__182A6452D3D54077");

                entity.ToTable("EmailConfirmation");

                entity.Property(e => e.IdReq)
                    .HasMaxLength(54)
                    .HasColumnName("ID_req");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");
                entity.Property(e => e.Expire).HasColumnType("datetime");
                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_user");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfUserNavigation).WithMany(p => p.EmailConfirmations)
                    .HasForeignKey(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EmailConf__ID_of__5090EFD7");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.IdOfGame).HasName("PK__Game__F187176F416BA7CC");

                entity.ToTable("Game");

                entity.Property(e => e.IdOfGame)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_Game");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.TypeOfGame)
                    .HasMaxLength(54)
                    .HasColumnName("Type_Of_Game");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.IdOfImage).HasName("PK__Images__9347264A4B05F199");

                entity.Property(e => e.IdOfImage)
                    .HasMaxLength(54)
                    .HasColumnName("Id_of_image");
                entity.Property(e => e.Directory)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participation>(entity =>
            {
                entity.HasKey(e => new { e.IdOfUser, e.IdOfRoom }).HasName("PK__Particip__CB4A5550C652ACCB");

                entity.ToTable("Participation");

                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("Id_Of_User");
                entity.Property(e => e.IdOfRoom)
                    .HasMaxLength(54)
                    .HasColumnName("Id_Of_Room");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfRoomNavigation).WithMany(p => p.Participations)
                    .HasForeignKey(d => d.IdOfRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Participa__Id_Of__7A8729A3");

                entity.HasOne(d => d.IdOfUserNavigation).WithMany(p => p.Participations)
                    .HasForeignKey(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Participa__Id_Of__7993056A");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdOfRoom).HasName("PK__Room__8B8A9C144CAB0E88");

                entity.ToTable("Room");

                entity.Property(e => e.IdOfRoom)
                    .HasMaxLength(54)
                    .HasColumnName("Id_Of_Room");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.IdOfGame)
                    .HasMaxLength(54)
                    .HasColumnName("Id_Of_Game");
                entity.Property(e => e.NameOfRoom)
                    .HasMaxLength(54)
                    .HasColumnName("Name_Of_Room");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfGameNavigation).WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.IdOfGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__Id_Of_Game__6774552F");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.IdOfSession).HasName("PK__Sessions__7599B219643FFE3C");

                entity.Property(e => e.IdOfSession)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_session");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_user");
                entity.Property(e => e.Token).HasColumnType("text");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfUserNavigation).WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sessions__ID_of___5555A4F4");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.HasKey(e => e.IdOfUser).HasName("PK__Statisti__93F2FC91D05D0875");

                entity.ToTable("Statistic");

                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("Id_Of_User");
                entity.Property(e => e.BetsWon).HasColumnName("Bets_Won");
                entity.Property(e => e.ChipsWon).HasColumnName("Chips_Won");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.IdOfUserNavigation).WithOne(p => p.Statistic)
                    .HasForeignKey<Statistic>(d => d.IdOfUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Statistic__Id_Of__74CE504D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdOfUser).HasName("PK__Users__23D22D0A380B4478");

                entity.HasIndex(e => e.UserName, "UQ__Users__C9F2845639396034").IsUnique();

                entity.Property(e => e.IdOfUser)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_user");
                entity.Property(e => e.Admin).HasDefaultValue(false);
                entity.Property(e => e.Avatar).HasMaxLength(54);
                entity.Property(e => e.Bio)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.BirthDate).HasColumnType("datetime");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");
                entity.Property(e => e.Email).HasMaxLength(320);
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");
                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AvatarNavigation).WithMany(p => p.Users)
                    .HasForeignKey(d => d.Avatar)
                    .HasConstraintName("FK__Users__Avatar__46136164");
            });

            modelBuilder.Entity<WonBet>(entity =>
            {
                entity.HasKey(e => e.IdOfGame).HasName("PK__WonBet__F187176FFF10317D");

                entity.ToTable("WonBet");

                entity.Property(e => e.IdOfGame)
                    .HasMaxLength(54)
                    .HasColumnName("ID_of_Game");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_at");
                entity.Property(e => e.WonWeb).HasMaxLength(50);

                entity.HasOne(d => d.IdOfGameNavigation).WithOne(p => p.WonBet)
                    .HasForeignKey<WonBet>(d => d.IdOfGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WonBet__ID_of_Ga__62AFA012");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
