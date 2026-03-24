
using System;
using Quest.Models.Results;
using Quest.Models.Sports;
using Quest.Models.Stadiums;
using Quest.Models.Teams;

namespace Quest.Models.Matches;

public class MatchEntity
{
    public int Id { get; set; }

    public int _sport_id { get; set; }
    public SportEntity Sport { get; set; } = null!;

    public int _home_team_id { get; set; }
    public TeamEntity HomeTeam { get; set; } = null!;

    public int _away_team_id { get; set; }
    public TeamEntity AwayTeam { get; set; } = null!;

    public int _stadium_id { get; set; }
    public StadiumEntity Stadium { get; set; } = null!;

    public ResultEntity? Result { get; set; }

    public int Season { get; set; }
    public StatusEnum Status { get; set; }
    public DateOnly DateVenue { get; set; }
    public TimeOnly TimeVenue { get; set; }
}
public enum StatusEnum
{
    PLAYED,
    SCHEDULED
}