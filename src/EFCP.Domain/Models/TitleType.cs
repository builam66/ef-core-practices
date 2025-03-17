namespace EFCP.Domain.Models;

public partial class TitleType
{
    public byte TitleTypeId { get; set; }

    public string TitleType1 { get; set; } = null!;

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
