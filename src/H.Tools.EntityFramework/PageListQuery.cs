namespace H.Tools.EntityFramework;

public class PageListQuery
{
    public int Limit { get; set; } = 20;
    public int Page { get; set; } = 1;
    public bool All { get; set; } = false;
}
