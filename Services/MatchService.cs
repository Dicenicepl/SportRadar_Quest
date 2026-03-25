using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quest;
using Quest.Models.Matches;

public class MatchService : IMatchService
{
  private readonly AppDbContext _context;
  public MatchService
  (
    AppDbContext context
  )
  {
    _context = context;
  }
  public async Task<List<MatchEntity>> GetAllMatchesAsync(string? sport, DateOnly? date)
  {
    var request = _context.Matches
    .Include(m => m.Sport)
    .Include(m => m.HomeTeam)
    .Include(m => m.AwayTeam)
    .Include(m => m.Stadium)
    .Include(m => m.Result)
    .AsNoTracking().AsQueryable();

    if (!string.IsNullOrEmpty(sport)) request = request.Where(m => m.Sport.Name == sport);
    if (date.HasValue) request = request.Where(m => m.DateVenue == date.Value);

    return await request.ToListAsync();
  }
  public async Task<MatchEntity> GetMatchByIdAsync(int id)
  {
    if (id < 0) return new MatchEntity { };
    return await _context.Matches
    .Include(m => m.Sport)
    .Include(m => m.HomeTeam)
    .Include(m => m.AwayTeam)
    .Include(m => m.Stadium)
    .Include(m => m.Result).ThenInclude(r => r.Goals)
    .Include(m => m.Result).ThenInclude(r => r.Cards)
    .Include(m => m.Result).ThenInclude(r => r.PeriodScores)
    .AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task AddMatchAsync(MatchEntity match)
  {
    try
    {
      await _context.Matches.AddAsync(match);
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }
  }
}