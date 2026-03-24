using System.ComponentModel.DataAnnotations;
using Quest.Models.Results;

namespace Quest.Models.PeriodScores;

public class PeriodScoreEntity
{
  [Key]
  public int Id { get; set; }

  public int _result_id { get; set; }
  public ResultEntity Result { get; set; } = null!;

  public int PeriodNumber { get; set; }
  public int HomeScore { get; set; }
  public int AwayScore { get; set; }
}