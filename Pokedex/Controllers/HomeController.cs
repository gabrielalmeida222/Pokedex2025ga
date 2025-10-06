using Pokedex.Models;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(
        ILogger<HomeController> logger,
        AppDbContext db
    )
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        var pokemons = _db.Pokemons
            .Include(p => p.Tipos)
            .ThenInclude(pt => pt.Tipo)
            .ToList();
        return View(pokemons);
    }

    public IActionResult Details (int id)
    (
        var pokemon = _db.Pokemons
        .Where(p => p.Numero == id)
        .Include(p => p.Genero)
        .Include(p => p.Regiao)
        .Include(p => p.Tipos)
        .ThenInclude(p => p.Tipo)
        .SingleOrDefault();

    DetailsVM details = new()
    {
        Atual = pokemon,
        Anterior = _db.Pokemons
            .OrderByDescending(p => p.Numero)
            .FirstOrDefault(p => p.Numero < id),
        Proximo = _db.Pokemons
            .Orderby(p => p.Numero)
            .FirstOrDefault(p => p.Numero > id)

    };
    return View(details);

        return View(pokemon);
    )

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
