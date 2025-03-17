namespace EFCP.Domain.Models;

public partial class TitlePrincipal
{
    public int TitleId { get; set; }

    public short Ordinal { get; set; }

    public int PrincipalId { get; set; }

    public short ProfessionId { get; set; }

    public byte? KnownForOrdinal { get; set; }

    public virtual Principal Principal { get; set; } = null!;

    public virtual Profession Profession { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}
