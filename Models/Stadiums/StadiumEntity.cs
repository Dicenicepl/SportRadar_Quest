using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quest.Models.Matches;

namespace Quest.Models.Stadiums;

public class StadiumEntity
{
  [Key]
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;

  public ICollection<MatchEntity> Matches { get; set; } = new List<MatchEntity>();
}