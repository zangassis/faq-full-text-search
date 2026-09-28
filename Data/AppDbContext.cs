using FaqFullTextSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace FaqFullTextSearch.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Faq> Faqs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("faqs");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Question)
                    .HasColumnName("question");

                entity.Property(e => e.Answer)
                    .HasColumnName("answer");

                entity.Property(e => e.Category)
                    .HasColumnName("category");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(e => e.SearchVector)
                      .HasColumnName("search_vector")
                      .HasColumnType("tsvector");

                entity.HasIndex(e => e.SearchVector)
                    .HasDatabaseName("idx_faqs_search_vector")
                    .HasMethod("GIN");
            });
        }
    }
}