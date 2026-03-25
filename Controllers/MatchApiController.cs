using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quest.Models.Matches;

[ApiController]
[Route("api/matches")]
public class MatchApiController : ControllerBase
{
  private readonly IMatchService _matchService;
  public MatchApiController(
      IMatchService matchService
  )
  {
    _matchService = matchService;
  }
  [HttpGet("get-all")]
  public async Task<IActionResult> GetAll([FromQuery] string? sport = null, [FromQuery] DateOnly? date = null)
  {
    var result = await _matchService.GetAllMatchesAsync(sport, date);
    return Ok(result);
  }

  [HttpGet("get")]
  public async Task<IActionResult> GetById([FromQuery] int id)
  {
    var result = await _matchService.GetMatchByIdAsync(id);
    return Ok(result);
  }
  [HttpPost("add")]
  public async Task<ActionResult<MatchEntity>> Add([FromBody] MatchEntity match)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    await _matchService.AddMatchAsync(match);

    return CreatedAtAction(nameof(GetById), new { id = match.Id }, match);
  }
}