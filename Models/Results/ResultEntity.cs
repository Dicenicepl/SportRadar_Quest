using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quest.Models.Cards;
using Quest.Models.Goals;
using Quest.Models.Matches;
using Quest.Models.PeriodScores;
using Quest.Models.Teams;

namespace Quest.Models.Results;

public class ResultEntity
{
  [Key]
  public int Id { get; set; }

  public int _match_id { get; set; }
  public MatchEntity Match { get; set; } = null!;

  public int HomeGoals { get; set; }
  public int AwayGoals { get; set; }

  public int? _winner_id { get; set; }
  public TeamEntity? Winner { get; set; }

  public string Message { get; set; } = string.Empty;

  public ICollection<GoalEntity> Goals { get; set; } = new List<GoalEntity>();
  public ICollection<CardEntity> Cards { get; set; } = new List<CardEntity>();
  public ICollection<PeriodScoreEntity> PeriodScores { get; set; } = new List<PeriodScoreEntity>();
}