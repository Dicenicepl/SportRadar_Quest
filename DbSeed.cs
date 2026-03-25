using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quest.Models.Matches;
using Quest.Models.Results;
using Quest.Models.Sports;
using Quest.Models.Stadiums;
using Quest.Models.Teams;

namespace Quest;

//Created by AI
public static class DbSeed
{
  public static async Task SeedAsync(AppDbContext context)
  {
    if (await context.Matches.AnyAsync()) return;

    var football = new SportEntity { Name = "Football" };
    context.Sports.Add(football);

    // ── Stadiums ────────────────────────────────────────────
    var unknownStadium = new StadiumEntity { Name = "Unknown" };
    context.Stadiums.Add(unknownStadium);

    // ── Teams ───────────────────────────────────────────────
    var teams = new Dictionary<string, TeamEntity>
    {
      ["al-shabab-fc"] = new TeamEntity
      {
        Name = "Al Shabab",
        OfficialName = "Al Shabab FC",
        Slug = "al-shabab-fc",
        Abbreviation = "SHA",
        TeamCountryCode = "KSA"
      },
      ["fc-nasaf-qarshi"] = new TeamEntity
      {
        Name = "Nasaf",
        OfficialName = "FC Nasaf",
        Slug = "fc-nasaf-qarshi",
        Abbreviation = "NAS",
        TeamCountryCode = "UZB"
      },
      ["al-hilal-saudi-fc"] = new TeamEntity
      {
        Name = "Al Hilal",
        OfficialName = "Al Hilal Saudi FC",
        Slug = "al-hilal-saudi-fc",
        Abbreviation = "HIL",
        TeamCountryCode = "KSA"
      },
      ["shabab-al-ahli-club"] = new TeamEntity
      {
        Name = "Shabab Al Ahli",
        OfficialName = "SHABAB AL AHLI DUBAI",
        Slug = "shabab-al-ahli-club",
        Abbreviation = "SAH",
        TeamCountryCode = "UAE"
      },
      ["al-duhail-sc"] = new TeamEntity
      {
        Name = "Al Duhail",
        OfficialName = "AL DUHAIL SC",
        Slug = "al-duhail-sc",
        Abbreviation = "DUH",
        TeamCountryCode = "QAT"
      },
      ["al-rayyan-sc"] = new TeamEntity
      {
        Name = "Al Rayyan",
        OfficialName = "AL RAYYAN SC",
        Slug = "al-rayyan-sc",
        Abbreviation = "RYN",
        TeamCountryCode = "QAT"
      },
      ["al-faisaly-fc"] = new TeamEntity
      {
        Name = "Al Faisaly",
        OfficialName = "Al Faisaly FC",
        Slug = "al-faisaly-fc",
        Abbreviation = "FAI",
        TeamCountryCode = "KSA"
      },
      ["foolad-khuzestan-fc"] = new TeamEntity
      {
        Name = "Foolad",
        OfficialName = "FOOLAD KHOUZESTAN FC",
        Slug = "foolad-khuzestan-fc",
        Abbreviation = "FLD",
        TeamCountryCode = "IRN"
      },
      ["urawa-red-diamonds"] = new TeamEntity
      {
        Name = "Urawa Reds",
        OfficialName = "Urawa Red Diamonds",
        Slug = "urawa-red-diamonds",
        Abbreviation = "RED",
        TeamCountryCode = "JPN"
      },
    };
    context.Teams.AddRange(teams.Values);

    await context.SaveChangesAsync();

    // ── Matches ─────────────────────────────────────────────
    var matches = new List<(MatchEntity Match, ResultEntity? Result)>
        {
            // 1. Al Shabab vs Nasaf — PLAYED 1:2
            (
                new MatchEntity
                {
                    Sport     = football,
                    HomeTeam  = teams["al-shabab-fc"],
                    AwayTeam  = teams["fc-nasaf-qarshi"],
                    Stadium   = unknownStadium,
                    Season    = 2024,
                    Status    = StatusEnum.PLAYED,
                    DateVenue = new DateOnly(2024, 1, 3),
                    TimeVenue = new TimeOnly(0, 0),
                },
                new ResultEntity
                {
                    HomeGoals = 1,
                    AwayGoals = 2,
                    Winner    = teams["fc-nasaf-qarshi"],
                    Message   = string.Empty,
                }
            ),
            // 2. Al Hilal vs Shabab Al Ahli — SCHEDULED
            (
                new MatchEntity
                {
                    Sport     = football,
                    HomeTeam  = teams["al-hilal-saudi-fc"],
                    AwayTeam  = teams["shabab-al-ahli-club"],
                    Stadium   = unknownStadium,
                    Season    = 2024,
                    Status    = StatusEnum.SCHEDULED,
                    DateVenue = new DateOnly(2024, 1, 3),
                    TimeVenue = new TimeOnly(16, 0),
                },
                null
            ),
            // 3. Al Duhail vs Al Rayyan — SCHEDULED
            (
                new MatchEntity
                {
                    Sport     = football,
                    HomeTeam  = teams["al-duhail-sc"],
                    AwayTeam  = teams["al-rayyan-sc"],
                    Stadium   = unknownStadium,
                    Season    = 2024,
                    Status    = StatusEnum.SCHEDULED,
                    DateVenue = new DateOnly(2024, 1, 4),
                    TimeVenue = new TimeOnly(15, 25),
                },
                null
            ),
            // 4. Al Faisaly vs Foolad — SCHEDULED
            (
                new MatchEntity
                {
                    Sport     = football,
                    HomeTeam  = teams["al-faisaly-fc"],
                    AwayTeam  = teams["foolad-khuzestan-fc"],
                    Stadium   = unknownStadium,
                    Season    = 2024,
                    Status    = StatusEnum.SCHEDULED,
                    DateVenue = new DateOnly(2024, 1, 4),
                    TimeVenue = new TimeOnly(8, 0),
                },
                null
            ),
        };

    foreach (var (match, result) in matches)
    {
      context.Matches.Add(match);

      if (result != null)
      {
        result.Match = match;
        context.Results.Add(result);
      }
    }

    await context.SaveChangesAsync();
  }
}