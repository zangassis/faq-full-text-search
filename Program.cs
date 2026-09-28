using FaqFullTextSearch.Data;
using FaqFullTextSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<FaqService>();

var app = builder.Build();

app.MapGet("/api/faqs/contains", async (string search, [FromServices] FaqService faqService) =>
{
    var faqs = await faqService.FindFaqsByContains(search);

    return Results.Ok(faqs);
});

app.MapGet("/api/faqs/search", async (string search, [FromServices] FaqService faqService) =>
{
    var faqs = await faqService.FindFaqsByFts(search);

    return Results.Ok(faqs);
});

app.Run();
