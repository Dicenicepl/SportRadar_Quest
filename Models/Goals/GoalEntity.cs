using System.ComponentModel.DataAnnotations;
using Quest.Models.Results;

namespace Quest.Models.Goals;

public class GoalEntity
{
  [Key]
  public int Id { get; set; }

  public int _result_id { get; set; }
  public ResultEntity Result { get; set; } = null!;

  public int Minute { get; set; }
  public string ScorerName { get; set; } = string.Empty;
  public string AssistName { get; set; } = string.Empty;
}