using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static GameZone.Constraints;

namespace GameZone.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly GameZoneDbContext dbContext;

        public GameController(GameZoneDbContext _context)
        {
            dbContext = _context;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> All()
        {
            List<GameInfoViewModel> games = await dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Select(g => new GameInfoViewModel()
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title,
                    ReleasedOn = g.ReleasedOn.ToString(ReleaseDateFormat),
                    Genre = g.Genre.Name,
                    Publisher = g.Publisher.UserName ?? string.Empty  //????????????????
                })
                .AsNoTracking()
                .ToListAsync();
            return View(games);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            GameViewModel model = new GameViewModel();
            model.Genres = await GetGenres();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await GetGenres();
                return View(model);
            }

            bool isValidDate = DateTime.TryParseExact(model.ReleasedOn, ReleaseDateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date);
            if (!isValidDate)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Date must be in yyyy-MM-dd format.");
                model.Genres = await GetGenres();
                return View(model);
            }

            Game game = new Game()
            {
                Title = model.Title,
                Description = model.Description,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                ReleasedOn = date,
                PublisherId = GetCurrentUserId() ?? string.Empty
                //IsDeleted = false // false by default
            };

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            List<GameInfoViewModel> myGames = await dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Where(g => g.GamersGames.Any(gg => gg.GamerId == currentUserId))
                .Select(g => new GameInfoViewModel()
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString(ReleaseDateFormat),
                    Title = g.Title,
                    Publisher = g.Publisher.UserName ?? string.Empty,
                    Genre = g.Genre.Name
                })
                .ToListAsync();

            return View(myGames);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            //Game? game = await dbContext.Games.FirstOrDefaultAsync(g => g.Id == id);
            //Game? game = await dbContext.Games.FindAsync(id);
            Game? game = await dbContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            if (game == null || game.IsDeleted)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            //List<GamerGame> userGames = await dbContext.GamersGames.Where(gg => gg.GamerId == currentUserId).ToListAsync();

            //if (!userGames.Any(qq => game.Id == id))
            //{
            //    GamerGame gamerGame = new GamerGame()
            //    {
            //        Game = game,
            //        GamerId = currentUserId
            //    };

            //    await dbContext.GamersGames.AddAsync(gamerGame);
            //    await dbContext.SaveChangesAsync();
            //}

            if (game.GamersGames.Any(gg => gg.GamerId == currentUserId))
            {
                return RedirectToAction(nameof(All));

            }

            game.GamersGames.Add(new GamerGame()
            {
                GamerId = currentUserId,
                GameId = game.Id
            });

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(MyZone));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await dbContext.Games
                .Where(g => g.Id == id)
                .Where(g => g.IsDeleted == false)
                .AsNoTracking()
                .Select(g => new GameViewModel()
                {
                    Description = g.Description,
                    GenreId = g.GenreId,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString(ReleaseDateFormat),
                    Title = g.Title
                })
                .FirstOrDefaultAsync();

            model.Genres = await GetGenres();

            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Edit(GameViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await GetGenres();
                return View(model);
            }

            bool isValidDate = DateTime.TryParseExact(model.ReleasedOn, ReleaseDateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date);
            if (!isValidDate)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), "Date must be in yyyy-MM-dd format.");
                model.Genres = await GetGenres();
                return View(model);
            }

            Game? entityGame = await dbContext.Games.FindAsync(id);

            if (entityGame == null || entityGame.IsDeleted)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (entityGame.PublisherId != currentUserId)
            {
                return RedirectToAction(nameof(All));
            }

            entityGame.Title = model.Title;
            entityGame.Description = model.Description;
            entityGame.GenreId = model.GenreId;
            entityGame.ImageUrl = model.ImageUrl;
            entityGame.ReleasedOn = date;
            entityGame.PublisherId = currentUserId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------


        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {

            Game? game = await dbContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.GamersGames)
                .FirstOrDefaultAsync();

            if (game == null || game.IsDeleted)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            GamerGame? gamerGame = game.GamersGames.FirstOrDefault(gg => gg.GamerId == currentUserId);

            if (gamerGame != null)
            {
                game.GamersGames.Remove(gamerGame);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyZone));
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Where(g => g.Id == id)
                .AsNoTracking()
                .Select(g => new DetailModelView()
                {
                    Id = g.Id,
                    ImageUrl = g.ImageUrl,
                    Title = g.Title,
                    Description = g.Description,
                    Genre = g.Genre.Name,
                    ReleasedOn = g.ReleasedOn.ToString(ReleaseDateFormat),
                    Publisher = g.Publisher.UserName ?? string.Empty
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteViewModel? model = await dbContext.Games
                .Where(g => g.Id == id)
                .Where(g => g.IsDeleted == false)
                .AsNoTracking()
                .Select(g => new DeleteViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Publisher = g.Publisher.UserName
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DeleteViewModel model)
        {
            Game? game = await dbContext.Games
                .Where(g => g.IsDeleted == false)
                .Where(g => g.Id == model.Id)
                .FirstOrDefaultAsync();

            if (game != null)
            {
                game.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
        private async Task<List<Genre>> GetGenres()
        {
            return await dbContext.Genres.ToListAsync();
        }

        private string? GetCurrentUserId()
        {
            //return User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return User.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
