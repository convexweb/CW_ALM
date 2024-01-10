using CW_ALM.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;

namespace CW_ALM.Infra.Contexts
{
    public partial class CW_ALMContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CW_ALMContext(DbContextOptions<CW_ALMContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<CasoDeTeste> CasosDeTestes { get; set; }
        public virtual DbSet<CasoDeTesteFollowUp> CasosDeTestesFollowUPs { get; set; }
        public virtual DbSet<CasoDeTesteFollowUpImage> CasosDeTestesFollowUPsImages { get; set; }
        public virtual DbSet<CasoDeTeste_Tester> CasosDeTestes_Testers { get; set; }
        public virtual DbSet<Enumeracao> Enumeracoes { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<GrupoUsuario> GruposUsuarios { get; set; }
        public virtual DbSet<Teste> Testes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_100_CI_AI");

            modelBuilder.Entity<CasoDeTeste>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("CasosDeTestes", "Geral");

                entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DataInicio).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DataTermino).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DescricaoENUS).IsUnicode(false);
                entity.Property(e => e.DescricaoESCO).IsUnicode(false);
                entity.Property(e => e.DescricaoESPE).IsUnicode(false);
                entity.Property(e => e.DescricaoPTBR).IsUnicode(false);

                entity.HasOne(d => d.Situacao).WithMany(p => p.LstCasosDeTestes)
                    .HasForeignKey(d => d.SituacaoUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Teste).WithMany(p => p.LstCasosDeTestes)
                    .HasForeignKey(d => d.TesteUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CasoDeTesteFollowUp>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("CasosDeTestesFollowUPs", "Geral");

                entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Feedback).IsUnicode(false);

                entity.HasOne(d => d.CasoDeTeste).WithMany(p => p.LstCasosDeTestesFollowUps)
                    .HasForeignKey(d => d.CasoDeTesteUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Resultado).WithMany(p => p.LstCasosDeTestesFollowUpsResultado)
                    .HasForeignKey(d => d.ResultadoUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Status).WithMany(p => p.LstCasosDeTestesFollowUpsStatus)
                    .HasForeignKey(d => d.StatusUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Tester).WithMany(p => p.LstCasosDeTestesFollowUps)
                    .HasForeignKey(d => d.TesterUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CasoDeTesteFollowUpImage>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("CasosDeTestesFollowUPsImages", "Geral");

                entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.ImgPath)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.CasoDeTesteFollowUp).WithMany(p => p.LstCasosDeTestesFollowUpsImages)
                    .HasForeignKey(d => d.CasoDeTesteFollowUPUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CasoDeTeste_Tester>(entity =>
            {
                entity.HasKey(e => new { e.CasoDeTesteUID, e.TesterUID });

                entity.ToTable("CasosDeTestes_Testers", "Geral");

                entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CasoDeTeste).WithMany(p => p.LstCasosDeTestes_Testers)
                    .HasForeignKey(d => d.CasoDeTesteUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Tester).WithMany(p => p.LstCasosDeTestes_Testers)
                    .HasForeignKey(d => d.TesterUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Enumeracao>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("Enumeracoes", "Geral");

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

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("Grupos", "ACL");

                entity.Property(e => e.Status).HasDefaultValue(true);
                entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GrupoUsuario>(entity =>
            {
                entity.HasKey(e => new { e.GrupoUID, e.UsuarioUID });

                entity.ToTable("GruposUsuarios", "ACL");

                entity.Property(e => e.Criado).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Grupo).WithMany(p => p.LstGruposUsuarios)
                    .HasForeignKey(d => d.GrupoUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Usuario).WithMany(p => p.LstGruposUsuarios)
                    .HasForeignKey(d => d.UsuarioUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Teste>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("Testes", "Geral");

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

                entity.HasOne(d => d.Situacao).WithMany(p => p.LstTestesSituacao)
                    .HasForeignKey(d => d.SituacaoUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Status).WithMany(p => p.LstTestesStatus)
                    .HasForeignKey(d => d.StatusUID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("Usuarios", "ACL");

                entity.Property(e => e.Status).HasDefaultValue(true);
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

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var usuarioUID = string.Empty;
            try
            {
                var usuario = _httpContextAccessor.HttpContext.Items["Usuario"] as Usuario;
                usuarioUID = usuario.UID.ToString();
            } catch
            {                
            }
            
            var currentUsername = !string.IsNullOrEmpty(usuarioUID)
                ? usuarioUID
                : Guid.Empty.ToString();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).Criado = DateTime.Now;
                    ((Entity)entity.Entity).Alterado = null;
                    //((Entity)entity.Entity).CreatedBy = currentUsername;
                }
                if (entity.State == EntityState.Modified)
                {
                    ((Entity)entity.Entity).Alterado = DateTime.Now;
                    //((Entity)entity.Entity).CreatedBy = currentUsername;
                }
            }
        }
    }
}
