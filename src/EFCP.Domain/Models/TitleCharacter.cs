namespace EFCP.Domain.Models;

public partial class TitleCharacter
{
    public int TitleId { get; set; }

    public int PrincipalId { get; set; }

    public string Character { get; set; } = null!;

    public virtual Principal Principal { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}
