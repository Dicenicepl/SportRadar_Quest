using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quest.Models.DTO;
using Quest.Models.Matches;
using Quest.Models.Sports;
using Quest.Models.Stadiums;
using Quest.Models.Teams;

public interface IMatchService
{
  public Task<List<MatchEntity>> GetAllMatchesAsync(string? sport, DateOnly? date);
  public Task<MatchEntity> GetMatchByIdAsync(int id);
  public Task<int> AddMatchAsync(MatchAddDto match);
  public Task<List<SportEntity>> GetSportsAsync();
  public Task<List<TeamEntity>> GetTeamsAsync();
  public Task<List<StadiumEntity>> GetStadiumAsync();
}