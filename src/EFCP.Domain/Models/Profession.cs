namespace EFCP.Domain.Models;

public partial class Profession
{
    public short ProfessionId { get; set; }

    public string Profession1 { get; set; } = null!;

    public virtual ICollection<PrimaryProfession> PrimaryProfessions { get; set; } = new List<PrimaryProfession>();

    public virtual ICollection<TitlePrincipal> TitlePrincipals { get; set; } = new List<TitlePrincipal>();
}
