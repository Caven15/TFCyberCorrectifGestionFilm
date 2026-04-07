using Microsoft.AspNetCore.Mvc;
using TFCyberCorrectifGestionFilm.Models;

namespace TFCyberCorrectifGestionFilm.Controllers
{
    public class FilmController : Controller
    {
        public static List<Film> Films = new List<Film>
        {
            new Film { Id = 1, Titre = "Seigneur des Anneaux", Genre="Fantasie", Annee= 2001},
            new Film { Id = 2, Titre = "Le silence des agneaux", Genre="Thriller", Annee= 1991},
            new Film { Id = 3, Titre = "Inception", Genre="Science-fiction", Annee= 2010},
            new Film { Id = 4, Titre = "2001 : L'odyssée de l'espace", Genre="Science-fiction", Annee= 1968},
            new Film { Id = 5, Titre = "Rocky", Genre="Action", Annee= 1976},
            new Film { Id = 6, Titre = "Interstellar", Genre="Science-fiction", Annee= 2014}
        };
        [HttpGet]
        public IActionResult Index(string? genre = null, bool trierCroissant = false)
        {
            ViewData["TitrePage"] = string.IsNullOrEmpty(genre) ? "Tout les films" : $"Films de genre {genre}";
            ViewBag.NombreFilms = Films.Count;

            List<Film> filmsFiltres = string.IsNullOrEmpty(genre) ? Films : Films.Where(f => f.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (trierCroissant)
            {
                filmsFiltres = filmsFiltres.OrderBy(f => f.Titre).ToList();
            }

            return View(filmsFiltres);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Film film = Films.FirstOrDefault(f => f.Id == id);

            if (film == null)
            {
                return NotFound();
            }

            ViewData["TitrePage"] = $"Détails - {film.Titre}";
            return View(film);
        }

        [HttpGet]
        public IActionResult Apropos()
        {
            ViewData["TitrePage"] = "À propos";

            ViewBag.AnneeMin = Films.Min(f => f.Annee);
            ViewBag.AnneeMax = Films.Max(f => f.Annee);

            TempData["Message"] = "Bienvenue sur la page À propos";

            return View();
        }

        [HttpGet] // Action supprimer => Get pour confirmation
        public IActionResult Supprimer(int id)
        {
            Film film = Films.FirstOrDefault(f => f.Id == id);

            if (film == null)
            {
                return NotFound();
            }

            return View(film);

        }

        [HttpPost] // Action de confirmer suppression 
        public IActionResult SupprimerConfirm(int id)
        {
            Film film = Films.FirstOrDefault(f => f.Id == id);

            if (film != null)
            {
                Films.Remove(film);
                TempData["Message"] = $"Le film {film.Titre} a été supprimé";
            }

            return RedirectToAction("Index");
        }
    }
}
