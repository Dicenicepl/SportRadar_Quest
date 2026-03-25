using System;
using System.ComponentModel.DataAnnotations;
using Quest.Models.Matches;

namespace Quest.Models.DTO;

public class MatchAddDto
{
  [Required]
  public int SportId { get; set; }
  [Required]
  public int HomeTeamId { get; set; }
  [Required]
  public int AwayTeamId { get; set; }
  [Required]
  public int StadiumId { get; set; }
  [Required]
  public int Season { get; set; }
  [Required]
  public StatusEnum Status { get; set; }
  [Required]
  public DateOnly DateVenue { get; set; }
  [Required]
  public TimeOnly TimeVenue { get; set; }
}