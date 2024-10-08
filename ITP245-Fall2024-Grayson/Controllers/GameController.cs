using ITP245_Fall2024_GraysonModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

public class GameController : Controller
{
    private SportsEntities db = new SportsEntities();

    // GET: Game
    public ActionResult Index()
    {
        var games = db.Games
            .Include(g => g.Team) // Home Team
            .Include(g => g.Team1) // Visitor Team
            .Include(g => g.Field)
            .OrderBy(g => g.DateTime)
            .ThenBy(g => g.Team.Name)
            .ThenBy(g => g.Team1.Name)
            .ToList();
        return View(games);
    }

    // GET: Game/Details/5
    public ActionResult Details(int id)
    {
        var game = db.Games
            .Include(g => g.Team) // Home Team
            .Include(g => g.Team1) // Visitor Team
            .Include(g => g.Field)
            .FirstOrDefault(g => g.GameId == id);
        if (game == null)
        {
            return HttpNotFound();
        }
        return View(game);
    }
}
