namespace EFCP.Domain.Models;

public partial class Principal
{
    public int PrincipalId { get; set; }

    public string PrimaryName { get; set; } = null!;

    public DateOnly? BirthYear { get; set; }

    public DateOnly? DeathYear { get; set; }

    public virtual ICollection<PrimaryProfession> PrimaryProfessions { get; set; } = new List<PrimaryProfession>();

    public virtual ICollection<TitlePrincipal> TitlePrincipals { get; set; } = new List<TitlePrincipal>();
}
