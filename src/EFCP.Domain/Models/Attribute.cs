namespace EFCP.Domain.Models;

public partial class Attribute
{
    public int AttributeId { get; set; }

    public string Class { get; set; } = null!;

    public string Attribute1 { get; set; } = null!;

    public virtual ICollection<TitleName> TitleNames { get; set; } = new List<TitleName>();
}
