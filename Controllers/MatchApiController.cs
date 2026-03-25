using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quest.Models.DTO;
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
  [HttpGet()]
  public async Task<IActionResult> GetAll([FromQuery] string? sport = null, [FromQuery] DateOnly? date = null)
  {
    var data = await _matchService.GetAllMatchesAsync(sport, date);
    return Ok(data);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var data = await _matchService.GetMatchByIdAsync(id);
    if (data == null) return NotFound();
    return Ok(data);
  }
  [HttpPost()]
  public async Task<ActionResult<MatchEntity>> Add([FromBody] MatchAddDto dto)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var matchId = await _matchService.AddMatchAsync(dto);

    return CreatedAtAction(nameof(GetById), new { id = matchId }, dto);
  }
}