using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quest.Models.Matches;

namespace Quest.Models.Sports;

public class SportEntity
{
  [Key]
  public int Id { get; set; }
  public string Name { get; set; } = String.Empty;
  public ICollection<MatchEntity> Matches { get; set; } = new List<MatchEntity>();
}