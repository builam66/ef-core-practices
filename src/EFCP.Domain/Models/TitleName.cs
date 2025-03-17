namespace EFCP.Domain.Models;

public partial class TitleName
{
    public int TitleId { get; set; }

    public byte Ordinal { get; set; }

    public string? Region { get; set; }

    public string? Language { get; set; }

    public bool IsOriginal { get; set; }

    public string Title { get; set; } = null!;

    public virtual Title TitleNavigation { get; set; } = null!;

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
}
