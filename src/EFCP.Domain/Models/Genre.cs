namespace EFCP.Domain.Models;

public partial class Genre
{
    public short GenreId { get; set; }

    public string Genre1 { get; set; } = null!;

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
