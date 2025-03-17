namespace EFCP.Domain.Models;

public partial class Episode
{
    public int ParentId { get; set; }

    public int EpisodeId { get; set; }

    public short? Season { get; set; }

    public int? Episode1 { get; set; }

    public virtual Title EpisodeNavigation { get; set; } = null!;

    public virtual Title Parent { get; set; } = null!;
}
