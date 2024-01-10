using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetoAuxiliar.Models;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CasosDeTestes> CasosDeTestes { get; set; }

    public virtual DbSet<CasosDeTestesFollowUPs> CasosDeTestesFollowUPs { get; set; }

    public virtual DbSet<CasosDeTestesFollowUPsImages> CasosDeTestesFollowUPsImages { get; set; }

    public virtual DbSet<CasosDeTestes_Testers> CasosDeTestes_Testers { get; set; }

    public virtual DbSet<Enumeracoes> Enumeracoes { get; set; }

    public virtual DbSet<Grupos> Grupos { get; set; }

    public virtual DbSet<GruposUsuarios> GruposUsuarios { get; set; }

    public virtual DbSet<Testes> Testes { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AI");

        modelBuilder.Entity<CasosDeTestes>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__CasosDeT__C5B196023174DFCA");

            entity.ToTable("CasosDeTestes", "Geral");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DataInicio).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DataTermino).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DescricaoENUS).IsUnicode(false);
            entity.Property(e => e.DescricaoESCO).IsUnicode(false);
            entity.Property(e => e.DescricaoESPE).IsUnicode(false);
            entity.Property(e => e.DescricaoPTBR).IsUnicode(false);

            entity.HasOne(d => d.SituacaoU).WithMany(p => p.CasosDeTestes)
                .HasForeignKey(d => d.SituacaoUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__Situa__403A8C7D");

            entity.HasOne(d => d.TesteU).WithMany(p => p.CasosDeTestes)
                .HasForeignKey(d => d.TesteUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__Teste__412EB0B6");
        });

        modelBuilder.Entity<CasosDeTestesFollowUPs>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__CasosDeT__C5B1960257427118");

            entity.ToTable("CasosDeTestesFollowUPs", "Geral");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Feedback).IsUnicode(false);

            entity.HasOne(d => d.CasoDeTesteU).WithMany(p => p.CasosDeTestesFollowUPs)
                .HasForeignKey(d => d.CasoDeTesteUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__CasoD__440B1D61");

            entity.HasOne(d => d.ResultadoU).WithMany(p => p.CasosDeTestesFollowUPsResultadoU)
                .HasForeignKey(d => d.ResultadoUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__Resul__44FF419A");

            entity.HasOne(d => d.StatusU).WithMany(p => p.CasosDeTestesFollowUPsStatusU)
                .HasForeignKey(d => d.StatusUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__Statu__45F365D3");

            entity.HasOne(d => d.TesterU).WithMany(p => p.CasosDeTestesFollowUPs)
                .HasForeignKey(d => d.TesterUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__Teste__46E78A0C");
        });

        modelBuilder.Entity<CasosDeTestesFollowUPsImages>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__CasosDeT__C5B1960281AE6DD3");

            entity.ToTable("CasosDeTestesFollowUPsImages", "Geral");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.CasoDeTesteFollowUPU).WithMany(p => p.CasosDeTestesFollowUPsImages)
                .HasForeignKey(d => d.CasoDeTesteFollowUPUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__CasoD__47DBAE45");
        });

        modelBuilder.Entity<CasosDeTestes_Testers>(entity =>
        {
            entity.HasKey(e => new { e.CasoDeTesteUID, e.TesterUID }).HasName("PK__CasosDeT__614E72F9F825AA2C");

            entity.ToTable("CasosDeTestes_Testers", "Geral");

            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CasoDeTesteU).WithMany(p => p.CasosDeTestes_Testers)
                .HasForeignKey(d => d.CasoDeTesteUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__CasoD__4222D4EF");

            entity.HasOne(d => d.TesterU).WithMany(p => p.CasosDeTestes_Testers)
                .HasForeignKey(d => d.TesterUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CasosDeTe__Teste__4316F928");
        });

        modelBuilder.Entity<Enumeracoes>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__Enumerac__C5B1960237251533");

            entity.ToTable("Enumeracoes", "Geral");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DBField)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DBValue)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomeENUS)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomeESCO)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomeESPE)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomePTBR)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grupos>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__Grupos__C5B19602A54D9F2F");

            entity.ToTable("Grupos", "ACL");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GruposUsuarios>(entity =>
        {
            entity.HasKey(e => new { e.GrupoUID, e.UsuarioUID }).HasName("PK__GruposUs__34A00C46334A6264");

            entity.ToTable("GruposUsuarios", "ACL");

            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.GrupoU).WithMany(p => p.GruposUsuarios)
                .HasForeignKey(d => d.GrupoUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GruposUsu__Grupo__3E52440B");

            entity.HasOne(d => d.UsuarioU).WithMany(p => p.GruposUsuarios)
                .HasForeignKey(d => d.UsuarioUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GruposUsu__Usuar__3F466844");
        });

        modelBuilder.Entity<Testes>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__Testes__C5B1960214028EF7");

            entity.ToTable("Testes", "Geral");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DataInicio).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DataTermino).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NomeENUS)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomeESCO)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomeESPE)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NomePTBR)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.URLSistema)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.SituacaoU).WithMany(p => p.TestesSituacaoU)
                .HasForeignKey(d => d.SituacaoUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Testes__Situacao__48CFD27E");

            entity.HasOne(d => d.StatusU).WithMany(p => p.TestesStatusU)
                .HasForeignKey(d => d.StatusUID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Testes__StatusUI__49C3F6B7");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UID).HasName("PK__Usuarios__C5B19602F1A0312D");

            entity.ToTable("Usuarios", "ACL");

            entity.Property(e => e.UID).ValueGeneratedNever();
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioAD)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
