using FaqFullTextSearch.Data;
using FaqFullTextSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace FaqFullTextSearch.Services;

public class FaqService
{
    private readonly AppDbContext _context;

    public FaqService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Faq>> FindFaqsByContains(string search)
    {
        var faqs = await _context.Faqs
            .Where(f =>
                f.Question.Contains(search) ||
                f.Answer.Contains(search))
            .ToListAsync();

        return faqs;
    }

    public async Task<List<FaqSearchResult>> FindFaqsByFts(string search)
    {
        var faqs = await _context.Faqs
            .Where(f => f.SearchVector.Matches(EF.Functions.PlainToTsQuery("english", search)))
            .Select(f => new FaqSearchResult
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer,
                Category = f.Category,
                Rank = f.SearchVector.Rank(EF.Functions.PlainToTsQuery("english", search))
            })
            .OrderByDescending(f => f.Rank)
            .Take(50)
            .ToListAsync();

        return faqs;
    }
}
