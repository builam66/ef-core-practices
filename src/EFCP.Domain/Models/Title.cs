namespace EFCP.Domain.Models;

public partial class Title
{
    public int TitleId { get; set; }

    public byte TitleTypeId { get; set; }

    public bool IsAdult { get; set; }

    public DateOnly? StartYear { get; set; }

    public DateOnly? EndYear { get; set; }

    public TimeOnly? Runtime { get; set; }

    public int? VoteCount { get; set; }

    public decimal? AverageRating { get; set; }

    public virtual Episode? EpisodeEpisodeNavigation { get; set; }

    public virtual ICollection<Episode> EpisodeParents { get; set; } = new List<Episode>();

    public virtual TitleName? TitleName { get; set; }

    public virtual ICollection<TitlePrincipal> TitlePrincipals { get; set; } = new List<TitlePrincipal>();

    public virtual TitleType TitleType { get; set; } = null!;

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
