using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quest.Models.DTO;

namespace Quest.Controllers;

[Route("matches")]
public class MatchController : Controller
{
    private readonly IMatchService _matchService;
    public MatchController(
        IMatchService matchService
    )
    {
        _matchService = matchService;
    }
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] string? sport, [FromQuery] DateOnly? date)
    {
        var data = await _matchService.GetAllMatchesAsync(sport, date);
        return View(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var data = await _matchService.GetMatchByIdAsync(id);

        if (data == null) return NotFound();
        return View(data);
    }

    [HttpGet("add")]
    public async Task<IActionResult> Add()
    {
        await PopulateViewBag();
        return View(new MatchAddDto());
    }
    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(MatchAddDto dto)
    {
        if (!ModelState.IsValid)
        {
            await PopulateViewBag();
            return View(dto);
        }

        var id = await _matchService.AddMatchAsync(dto);
        return RedirectToAction(nameof(Details), new { id });
    }
    private async Task PopulateViewBag()
    {
        ViewBag.Sports = await _matchService.GetSportsAsync();
        ViewBag.Teams = await _matchService.GetTeamsAsync();
        ViewBag.Stadiums = await _matchService.GetStadiumAsync();
    }
}
