using EFCP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Attribute = EFCP.Domain.Models.Attribute;

namespace EFCP.Application.Abstractions
{
    public interface IImdbDbContext
    {
        DbSet<Attribute> Attributes { get; set; }

        DbSet<Episode> Episodes { get; set; }

        DbSet<Genre> Genres { get; set; }

        DbSet<PrimaryProfession> PrimaryProfessions { get; set; }

        DbSet<Principal> Principals { get; set; }

        DbSet<Profession> Professions { get; set; }

        DbSet<Title> Titles { get; set; }

        DbSet<TitleCharacter> TitleCharacters { get; set; }

        DbSet<TitleName> TitleNames { get; set; }

        DbSet<TitlePrincipal> TitlePrincipals { get; set; }

        DbSet<TitleType> TitleTypes { get; set; }
    }
}
