using EFCP.Application.Abstractions;
using EFCP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Attribute = EFCP.Domain.Models.Attribute;

namespace EFCP.Infrastructure;

public partial class ImdbDbContext : DbContext, IImdbDbContext
{
    public ImdbDbContext()
    {
    }

    public ImdbDbContext(DbContextOptions<ImdbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<PrimaryProfession> PrimaryProfessions { get; set; }

    public virtual DbSet<Principal> Principals { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<TitleCharacter> TitleCharacters { get; set; }

    public virtual DbSet<TitleName> TitleNames { get; set; }

    public virtual DbSet<TitlePrincipal> TitlePrincipals { get; set; }

    public virtual DbSet<TitleType> TitleTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasIndex(e => new { e.Class, e.Attribute1 }, "UQ_Attributes").IsUnique();

            entity.Property(e => e.AttributeId)
                .ValueGeneratedNever()
                .HasColumnName("attributeId");
            entity.Property(e => e.Attribute1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("attribute");
            entity.Property(e => e.Class)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("class");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.Property(e => e.EpisodeId)
                .ValueGeneratedNever()
                .HasColumnName("episodeId");
            entity.Property(e => e.Episode1).HasColumnName("episode");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Season).HasColumnName("season");

            entity.HasOne(d => d.EpisodeNavigation).WithOne(p => p.EpisodeEpisodeNavigation)
                .HasForeignKey<Episode>(d => d.EpisodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleCharacters_Episode");

            entity.HasOne(d => d.Parent).WithMany(p => p.EpisodeParents)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleCharacters_Parent");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("genreId");
            entity.Property(e => e.Genre1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genre");
        });

        modelBuilder.Entity<PrimaryProfession>(entity =>
        {
            entity.HasKey(e => new { e.PrincipalId, e.ProfessionId }).HasName("PK_PrimaryProfession");

            entity.Property(e => e.PrincipalId).HasColumnName("principalId");
            entity.Property(e => e.ProfessionId).HasColumnName("professionId");
            entity.Property(e => e.Ordinal).HasColumnName("ordinal");

            entity.HasOne(d => d.Principal).WithMany(p => p.PrimaryProfessions)
                .HasForeignKey(d => d.PrincipalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrimaryProfession_Principal");

            entity.HasOne(d => d.Profession).WithMany(p => p.PrimaryProfessions)
                .HasForeignKey(d => d.ProfessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrimaryProfession_Profession");
        });

        modelBuilder.Entity<Principal>(entity =>
        {
            entity.Property(e => e.PrincipalId)
                .ValueGeneratedNever()
                .HasColumnName("principalId");
            entity.Property(e => e.BirthYear).HasColumnName("birthYear");
            entity.Property(e => e.DeathYear).HasColumnName("deathYear");
            entity.Property(e => e.PrimaryName)
                .HasMaxLength(120)
                .HasColumnName("primaryName");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.Property(e => e.ProfessionId)
                .ValueGeneratedNever()
                .HasColumnName("professionId");
            entity.Property(e => e.Profession1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("profession");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.TitleId)
                .ValueGeneratedNever()
                .HasColumnName("titleId");
            entity.Property(e => e.AverageRating)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("averageRating");
            entity.Property(e => e.EndYear).HasColumnName("endYear");
            entity.Property(e => e.IsAdult).HasColumnName("isAdult");
            entity.Property(e => e.Runtime)
                .HasPrecision(0)
                .HasColumnName("runtime");
            entity.Property(e => e.StartYear).HasColumnName("startYear");
            entity.Property(e => e.TitleTypeId).HasColumnName("titleTypeId");
            entity.Property(e => e.VoteCount).HasColumnName("voteCount");

            entity.HasOne(d => d.TitleType).WithMany(p => p.Titles)
                .HasForeignKey(d => d.TitleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Titles_TitleType");

            entity.HasMany(d => d.Genres).WithMany(p => p.Titles)
                .UsingEntity<Dictionary<string, object>>(
                    "TitleGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TitleGenres_Genre"),
                    l => l.HasOne<Title>().WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TitleGenres_Title"),
                    j =>
                    {
                        j.HasKey("TitleId", "GenreId");
                        j.ToTable("TitleGenres");
                        j.IndexerProperty<int>("TitleId").HasColumnName("titleId");
                        j.IndexerProperty<short>("GenreId").HasColumnName("genreId");
                    });
        });

        modelBuilder.Entity<TitleCharacter>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => new { e.TitleId, e.PrincipalId }, "IX_TitleCharacters").IsClustered();

            entity.Property(e => e.Character)
                .HasMaxLength(500)
                .HasColumnName("character");
            entity.Property(e => e.PrincipalId).HasColumnName("principalId");
            entity.Property(e => e.TitleId).HasColumnName("titleId");

            entity.HasOne(d => d.Principal).WithMany()
                .HasForeignKey(d => d.PrincipalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleCharacters_Principal");

            entity.HasOne(d => d.Title).WithMany()
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleCharacters_Title");
        });

        modelBuilder.Entity<TitleName>(entity =>
        {
            entity.HasKey(e => new { e.TitleId, e.Ordinal });

            entity.HasIndex(e => e.TitleId, "IX_TitleNames_Original")
                .IsUnique()
                .HasFilter("([isOriginal]=(1))");

            entity.Property(e => e.TitleId).HasColumnName("titleId");
            entity.Property(e => e.Ordinal).HasColumnName("ordinal");
            entity.Property(e => e.IsOriginal).HasColumnName("isOriginal");
            entity.Property(e => e.Language)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("language");
            entity.Property(e => e.Region)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("region");
            entity.Property(e => e.Title)
                .HasMaxLength(1000)
                .HasColumnName("title");

            entity.HasOne(d => d.TitleNavigation).WithOne(p => p.TitleName)
                .HasForeignKey<TitleName>(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleNames_Title");

            entity.HasMany(d => d.Attributes).WithMany(p => p.TitleNames)
                .UsingEntity<Dictionary<string, object>>(
                    "TitleNameAttribute",
                    r => r.HasOne<Attribute>().WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TitleNameAttributes_Attribute"),
                    l => l.HasOne<TitleName>().WithMany()
                        .HasForeignKey("TitleId", "Ordinal")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TitleNameAttributes_TitleName"),
                    j =>
                    {
                        j.HasKey("TitleId", "Ordinal", "AttributeId");
                        j.ToTable("TitleNameAttributes");
                        j.IndexerProperty<int>("TitleId").HasColumnName("titleId");
                        j.IndexerProperty<byte>("Ordinal").HasColumnName("ordinal");
                        j.IndexerProperty<int>("AttributeId").HasColumnName("attributeId");
                    });
        });

        modelBuilder.Entity<TitlePrincipal>(entity =>
        {
            entity.HasKey(e => new { e.TitleId, e.Ordinal });

            entity.Property(e => e.TitleId).HasColumnName("titleId");
            entity.Property(e => e.Ordinal).HasColumnName("ordinal");
            entity.Property(e => e.KnownForOrdinal).HasColumnName("knownForOrdinal");
            entity.Property(e => e.PrincipalId).HasColumnName("principalId");
            entity.Property(e => e.ProfessionId).HasColumnName("professionId");

            entity.HasOne(d => d.Principal).WithMany(p => p.TitlePrincipals)
                .HasForeignKey(d => d.PrincipalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitlePrincipals_Principal");

            entity.HasOne(d => d.Profession).WithMany(p => p.TitlePrincipals)
                .HasForeignKey(d => d.ProfessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitlePrincipals_Profession");

            entity.HasOne(d => d.Title).WithMany(p => p.TitlePrincipals)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitlePrincipals_Title");
        });

        modelBuilder.Entity<TitleType>(entity =>
        {
            entity.Property(e => e.TitleTypeId).HasColumnName("titleTypeId");
            entity.Property(e => e.TitleType1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titleType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
