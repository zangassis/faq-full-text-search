using NpgsqlTypes;

namespace FaqFullTextSearch.Models;

public class Faq
{
    public long Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public NpgsqlTsVector SearchVector { get; set; } = null!;
}