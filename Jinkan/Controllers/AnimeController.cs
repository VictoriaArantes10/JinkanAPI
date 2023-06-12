using JikanDotNet;
using Jinkan.Data;
using Jinkan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jinkan.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _context;

        IJikan jikan = new Jikan();

        public AnimeController(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<IActionResult> Search(string searchQuery)
        {
            var buscarAnime = await jikan.SearchAnimeAsync(searchQuery);

            if (!searchQuery.IsNullOrEmpty())
            {
                var login = new log
                {
                    Nome = searchQuery,
                    login = DateTime.Now
                };

                _context.logs.Add(login);
                _context.SaveChanges();
            }

            return View("Search", buscarAnime.Data);

        }
        public IActionResult GenerateLogsFile()
        {
            var logins = _context.logs.ToList();
            var fileContent = GenerateLogsFileContent(logins);
            var fileName = "login.txt";

            return File(Encoding.UTF8.GetBytes(fileContent), "text/pain", fileName);
        }

        private string GenerateLogsFileContent(List<log> logs)
        {
            var sb = new StringBuilder();

            sb.AppendLine(" Animes Procurados ");
            sb.AppendLine("---------------------------");

            foreach (var log in logs)
            {
                sb.AppendLine($"Nome do Anime: {log.Nome}");
                sb.AppendLine($"Data e Hora de entrada : {log.login}");
                sb.AppendLine("---------------------------");
            }

            return sb.ToString();
        }
    }
}
