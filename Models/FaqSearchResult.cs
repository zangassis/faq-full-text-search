namespace FaqFullTextSearch.Models;

public class FaqSearchResult
{
    public long Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Category { get; set; }
    public float Rank { get; set; }
}