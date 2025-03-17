namespace EFCP.Domain.Models;

public partial class PrimaryProfession
{
    public int PrincipalId { get; set; }

    public short ProfessionId { get; set; }

    public byte Ordinal { get; set; }

    public virtual Principal Principal { get; set; } = null!;

    public virtual Profession Profession { get; set; } = null!;
}
