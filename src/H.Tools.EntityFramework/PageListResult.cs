namespace H.Tools.EntityFramework;

public class PageListResult
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; }
}

public class PageListResult<T> : PageListResult
{
    public IReadOnlyList<T> List { get; set; } = null!;
}
