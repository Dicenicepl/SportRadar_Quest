
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quest.Models.Matches;
using Quest.Models.Results;

namespace Quest.Models.Teams;

public class TeamEntity
{
  [Key]
  public int Id { get; set; }
  public string Name { get; set; } = String.Empty;
  public string OfficialName { get; set; } = String.Empty;
  public string Slug { get; set; } = String.Empty;
  public string Abbreviation { get; set; } = String.Empty;
  public string TeamCountryCode { get; set; } = String.Empty;

  public ICollection<MatchEntity> HomeMatches { get; set; } = new List<MatchEntity>();
  public ICollection<MatchEntity> AwayMatches { get; set; } = new List<MatchEntity>();
  public ICollection<ResultEntity> WonResults { get; set; } = new List<ResultEntity>();
}