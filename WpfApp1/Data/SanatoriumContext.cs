using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;

namespace WpfApp1.Data;


public partial class SanatoriumContext : DbContext
{
    public SanatoriumContext()
    {
    }

    public SanatoriumContext(DbContextOptions<SanatoriumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Бронирование> Бронированиеs { get; set; }

    public virtual DbSet<ДневникСмены> ДневникСменыs { get; set; }

    public virtual DbSet<Документы> Документыs { get; set; }

    public virtual DbSet<Люди> Людиs { get; set; }

    public virtual DbSet<Программы> Программыs { get; set; }

    public virtual DbSet<Ребенок> Ребенокs { get; set; }

    public virtual DbSet<Смены> Сменыs { get; set; }

    public virtual DbSet<Учреждение> Учреждениеs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=dbsrv\\vip2025;Initial Catalog='Курсовой проект Иванова ЛВ Детсикй санаторий';Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Бронирование>(entity =>
        {
            entity.ToTable("Бронирование");

            entity.HasIndex(e => e.FkОператор, "IX_Бронирование_Оператор");

            entity.HasIndex(e => e.FkРебенка, "IX_Бронирование_Ребенок");

            entity.HasIndex(e => e.FkСмена, "IX_Бронирование_Смена");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkОператор).HasColumnName("FK_оператор");
            entity.Property(e => e.FkРебенка).HasColumnName("FK_ребенка");
            entity.Property(e => e.FkСмена).HasColumnName("FK_смена");
            entity.Property(e => e.ДатаОформления).HasColumnName("дата_оформления");
            entity.Property(e => e.Статус)
                .HasMaxLength(50)
                .HasColumnName("статус");

            entity.HasOne(d => d.FkОператорNavigation).WithMany(p => p.Бронированиеs)
                .HasForeignKey(d => d.FkОператор)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Бронирование_Люди_Оператор");

            entity.HasOne(d => d.FkРебенкаNavigation).WithMany(p => p.Бронированиеs)
                .HasForeignKey(d => d.FkРебенка)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Бронирование_Ребенок");

            entity.HasOne(d => d.FkСменаNavigation).WithMany(p => p.Бронированиеs)
                .HasForeignKey(d => d.FkСмена)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Бронирование_Смены");
        });

        modelBuilder.Entity<ДневникСмены>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Дневник_смены");

            entity.ToTable("Дневник смены");

            entity.HasIndex(e => e.FkБронирование, "IX_ДневникСмены_Бронирование");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkБронирование).HasColumnName("FK_бронирование");
            entity.Property(e => e.ДатаСоздания)
                .HasColumnType("datetime")
                .HasColumnName("дата_создания");
            entity.Property(e => e.Описание).HasColumnName("описание");

            entity.HasOne(d => d.FkБронированиеNavigation).WithMany(p => p.ДневникСменыs)
                .HasForeignKey(d => d.FkБронирование)
                .HasConstraintName("FK_ДневникСмены_Бронирование");
        });

        modelBuilder.Entity<Документы>(entity =>
        {
            entity.ToTable("Документы");

            entity.HasIndex(e => e.FkБронирование, "IX_Документы_Бронирование");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkБронирование).HasColumnName("FK_бронирование");
            entity.Property(e => e.СсылкаНаФайлы)
                .HasMaxLength(500)
                .HasColumnName("ссылка_на_файлы");
            entity.Property(e => e.Статус)
                .HasMaxLength(50)
                .HasColumnName("статус");
            entity.Property(e => e.Тип)
                .HasMaxLength(50)
                .HasColumnName("тип");

            entity.HasOne(d => d.FkБронированиеNavigation).WithMany(p => p.Документыs)
                .HasForeignKey(d => d.FkБронирование)
                .HasConstraintName("FK_Документы_Бронирование");
        });

        modelBuilder.Entity<Люди>(entity =>
        {
            entity.ToTable("Люди");

            entity.HasIndex(e => e.Логин, "UK_Люди_логин").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Логин)
                .HasMaxLength(50)
                .HasColumnName("логин");
            entity.Property(e => e.Пароль)
                .HasMaxLength(100)
                .HasColumnName("пароль");
            entity.Property(e => e.Паспорт)
                .HasMaxLength(20)
                .HasColumnName("паспорт");
            entity.Property(e => e.Почта)
                .HasMaxLength(100)
                .HasColumnName("почта");
            entity.Property(e => e.Роль)
                .HasMaxLength(20)
                .HasColumnName("роль");
            entity.Property(e => e.Телефон)
                .HasMaxLength(20)
                .HasColumnName("телефон");
            entity.Property(e => e.Фио)
                .HasMaxLength(100)
                .HasColumnName("ФИО");
        });

        modelBuilder.Entity<Программы>(entity =>
        {
            entity.ToTable("Программы");

            entity.HasIndex(e => e.FkУчреждение, "IX_Программы_Учреждение");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkУчреждение).HasColumnName("FK_учреждение");
            entity.Property(e => e.ВозрастМакс).HasColumnName("возраст_макс");
            entity.Property(e => e.ВозрастМин).HasColumnName("возраст_мин");
            entity.Property(e => e.Название)
                .HasMaxLength(100)
                .HasColumnName("название");

            entity.HasOne(d => d.FkУчреждениеNavigation).WithMany(p => p.Программыs)
                .HasForeignKey(d => d.FkУчреждение)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Программы_Учреждение");
        });

        modelBuilder.Entity<Ребенок>(entity =>
        {
            entity.ToTable("Ребенок");

            entity.HasIndex(e => e.FkРодитель, "IX_Ребенок_Родитель");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkРодитель).HasColumnName("FK_родитель");
            entity.Property(e => e.ДатаРождения).HasColumnName("дата_рождения");
            entity.Property(e => e.МедДокументы).HasColumnName("мед_документы");
            entity.Property(e => e.Фио)
                .HasMaxLength(100)
                .HasColumnName("ФИО");

            entity.HasOne(d => d.FkРодительNavigation).WithMany(p => p.Ребенокs)
                .HasForeignKey(d => d.FkРодитель)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ребенок_Люди");
        });

        modelBuilder.Entity<Смены>(entity =>
        {
            entity.ToTable("Смены");

            entity.HasIndex(e => e.FkПрограмма, "IX_Смены_Программа");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FkПрограмма).HasColumnName("FK_программа");
            entity.Property(e => e.Вместимость).HasColumnName("вместимость");
            entity.Property(e => e.ДатаНачала).HasColumnName("дата_начала");
            entity.Property(e => e.ДатаОкончания).HasColumnName("дата_окончания");
            entity.Property(e => e.Стоимость)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("стоимость");

            entity.HasOne(d => d.FkПрограммаNavigation).WithMany(p => p.Сменыs)
                .HasForeignKey(d => d.FkПрограмма)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Смены_Программы");
        });

        modelBuilder.Entity<Учреждение>(entity =>
        {
            entity.ToTable("Учреждение");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Адрес)
                .HasMaxLength(300)
                .HasColumnName("адрес");
            entity.Property(e => e.Название)
                .HasMaxLength(200)
                .HasColumnName("название");
            entity.Property(e => e.Регион)
                .HasMaxLength(100)
                .HasColumnName("регион");
            entity.Property(e => e.Рейтинг)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("рейтинг");
            entity.Property(e => e.Тип)
                .HasMaxLength(50)
                .HasColumnName("тип");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
