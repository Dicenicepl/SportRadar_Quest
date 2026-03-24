using Microsoft.EntityFrameworkCore;
using Quest.Models.Cards;
using Quest.Models.Goals;
using Quest.Models.Matches;
using Quest.Models.PeriodScores;
using Quest.Models.Results;
using Quest.Models.Sports;
using Quest.Models.Stadiums;
using Quest.Models.Teams;

namespace Quest;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<SportEntity> Sports { get; set; }
  public DbSet<TeamEntity> Teams { get; set; }
  public DbSet<StadiumEntity> Stadiums { get; set; }
  public DbSet<MatchEntity> Matches { get; set; }
  public DbSet<ResultEntity> Results { get; set; }
  public DbSet<GoalEntity> Goals { get; set; }
  public DbSet<CardEntity> Cards { get; set; }
  public DbSet<PeriodScoreEntity> PeriodScores { get; set; }

  //Created by AI using my Diagram
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // ── Match → Sport (many-to-one) ──
    modelBuilder.Entity<MatchEntity>()
        .HasOne(m => m.Sport)
        .WithMany(s => s.Matches)
        .HasForeignKey(m => m._sport_id)
        .OnDelete(DeleteBehavior.Restrict);

    // ── Match → HomeTeam (many-to-one) ──
    modelBuilder.Entity<MatchEntity>()
        .HasOne(m => m.HomeTeam)
        .WithMany(t => t.HomeMatches)
        .HasForeignKey(m => m._home_team_id)
        .OnDelete(DeleteBehavior.Restrict);

    // ── Match → AwayTeam (many-to-one) ──
    modelBuilder.Entity<MatchEntity>()
        .HasOne(m => m.AwayTeam)
        .WithMany(t => t.AwayMatches)
        .HasForeignKey(m => m._away_team_id)
        .OnDelete(DeleteBehavior.Restrict);

    // ── Match → Stadium (many-to-one) ──
    modelBuilder.Entity<MatchEntity>()
        .HasOne(m => m.Stadium)
        .WithMany(s => s.Matches)
        .HasForeignKey(m => m._stadium_id)
        .OnDelete(DeleteBehavior.Restrict);

    // ── Match ↔ Result (one-to-one) ──
    modelBuilder.Entity<ResultEntity>()
        .HasOne(r => r.Match)
        .WithOne(m => m.Result)
        .HasForeignKey<ResultEntity>(r => r._match_id)
        .OnDelete(DeleteBehavior.Cascade);

    // ── Result → Winner (many-to-one, nullable) ──
    modelBuilder.Entity<ResultEntity>()
        .HasOne(r => r.Winner)
        .WithMany(t => t.WonResults)
        .HasForeignKey(r => r._winner_id)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);

    // ── Result → Goals (one-to-many) ──
    modelBuilder.Entity<GoalEntity>()
        .HasOne(g => g.Result)
        .WithMany(r => r.Goals)
        .HasForeignKey(g => g._result_id)
        .OnDelete(DeleteBehavior.Cascade);

    // ── Result → Cards (one-to-many) ──
    modelBuilder.Entity<CardEntity>()
        .HasOne(c => c.Result)
        .WithMany(r => r.Cards)
        .HasForeignKey(c => c._result_id)
        .OnDelete(DeleteBehavior.Cascade);

    // ── Result → PeriodScores (one-to-many) ──
    modelBuilder.Entity<PeriodScoreEntity>()
        .HasOne(p => p.Result)
        .WithMany(r => r.PeriodScores)
        .HasForeignKey(p => p._result_id)
        .OnDelete(DeleteBehavior.Cascade);

    // ── Enum conversions (store as string) ──
    modelBuilder.Entity<MatchEntity>()
        .Property(m => m.Status)
        .HasConversion<string>();

    modelBuilder.Entity<CardEntity>()
        .Property(c => c.CardType)
        .HasConversion<string>();
  }
}