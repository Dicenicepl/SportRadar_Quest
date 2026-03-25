using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
