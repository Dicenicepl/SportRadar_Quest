using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quest.Models.Matches;

public interface IMatchService
{
  public Task<List<MatchEntity>> GetAllMatchesAsync(string? sport, DateOnly? date);
  public Task<MatchEntity> GetMatchByIdAsync(int id);
  public Task AddMatchAsync(MatchEntity match);
}