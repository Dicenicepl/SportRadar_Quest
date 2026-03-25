using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quest;
using Quest.Models.DTO;
using Quest.Models.Matches;
using Quest.Models.Sports;
using Quest.Models.Stadiums;
using Quest.Models.Teams;

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
  public async Task<int> AddMatchAsync(MatchAddDto dto)
  {
    var match = new MatchEntity
    {
      _sport_id = dto.SportId,
      _home_team_id = dto.HomeTeamId,
      _away_team_id = dto.AwayTeamId,
      _stadium_id = dto.StadiumId,
      Season = dto.Season,
      Status = dto.Status,
      DateVenue = dto.DateVenue,
      TimeVenue = dto.TimeVenue
    };
    await _context.Matches.AddAsync(match);
    await _context.SaveChangesAsync();
    return match.Id;
  }
  public async Task<List<SportEntity>> GetSportsAsync()
    => await _context.Sports.AsNoTracking().ToListAsync();
  public Task<List<TeamEntity>> GetTeamsAsync()
    => _context.Teams.AsNoTracking().ToListAsync();
  public Task<List<StadiumEntity>> GetStadiumAsync()
    => _context.Stadiums.AsNoTracking().ToListAsync();
}