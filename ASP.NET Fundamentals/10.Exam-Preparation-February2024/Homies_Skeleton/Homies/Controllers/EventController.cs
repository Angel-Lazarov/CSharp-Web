using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static Homies.Constraints;
using Type = Homies.Data.Type;

namespace Homies.Controllers
{
	[Authorize]
	public class EventController : Controller
	{
		private readonly HomiesDbContext contex;
		public EventController(HomiesDbContext _contex)
		{
			contex = _contex;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			List<EventInfoViewModel> events = await contex.Events
				.AsNoTracking()
				.Select(e => new EventInfoViewModel()
				{
					Id = e.Id,
					Name = e.Name,
					Type = e.Type.Name,
					Start = e.Start.ToString(EventDataFormatAdd),
					Organiser = e.Organiser.UserName ?? string.Empty

				})
				.AsNoTracking()
				.ToListAsync();

			return View(events);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			EventViewModel model = new();
			model.Types = await GetTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(EventViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Types = await GetTypes();
				return View(model);
			}

			bool isValidStartDate = DateTime.TryParseExact(model.Start, EventDataFormatAdd, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime startDate);

			bool isValidEndDate = DateTime.TryParseExact(model.End, EventDataFormatAdd, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime endDate);

			if (!isValidStartDate)
			{
				ModelState.AddModelError(nameof(model.Start), "Start date must be in yyyy-MM-dd H:mm format.");
				model.Types = await GetTypes();
				return View(model);
			}

			if (!isValidEndDate)
			{
				ModelState.AddModelError(nameof(model.End), "End date must be in yyyy-MM-dd H:mm format.");
				model.Types = await GetTypes();
				return View(model);
			}

			if (startDate > endDate)
			{
				ModelState.AddModelError(nameof(model.End), "End date must be after start date.");
				model.Types = await GetTypes();
				return View(model);
			}

			EventParticipant ep = new EventParticipant()
			{
				Event = new Event()
				{
					Name = model.Name,
					Description = model.Description,
					Start = startDate,
					End = endDate,
					CreatedOn = DateTime.Now,
					TypeId = model.TypeId,
					OrganiserId = GetCurrentUserId() ?? string.Empty
				},
				HelperId = GetCurrentUserId() ?? string.Empty
			};

			await contex.EventsParticipants.AddAsync(ep);
			await contex.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			EventViewModel? model = await contex.Events
				.Where(e => e.Id == id)
				.AsNoTracking()
				.Select(e => new EventViewModel()
				{
					Name = e.Name,
					Description = e.Description,
					Start = e.Start.ToString(EventDataFormatAdd),
					End = e.End.ToString(EventDataFormatAdd),
					TypeId = e.TypeId
				})
				.FirstOrDefaultAsync();

			model.Types = await GetTypes();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EventViewModel model, int id)
		{
			if (!ModelState.IsValid)
			{
				model.Types = await GetTypes();
				return View(model);
			}

			bool isValidStartDate = DateTime.TryParseExact(model.Start, EventDataFormatEdit, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime startDate);

			bool isValidEndDate = DateTime.TryParseExact(model.End, EventDataFormatEdit, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime endDate);

			if (!isValidStartDate)
			{
				ModelState.AddModelError(nameof(model.Start), "Start date must be in dd/MM/yyyy H:mm format.");
				model.Types = await GetTypes();
				return View(model);
			}

			if (!isValidEndDate)
			{
				ModelState.AddModelError(nameof(model.End), "End date must be in yyyy-MM-dd H:mm format.");
				model.Types = await GetTypes();
				return View(model);
			}

			if (startDate > endDate)
			{
				ModelState.AddModelError(nameof(model.End), "End date must be after start date.");
				model.Types = await GetTypes();
				return View(model);
			}

			Event? newEvent = await contex.Events.FindAsync(id);

			if (newEvent == null)
			{
				throw new ArgumentException("Invalid Id");
			}

			string currentUser = GetCurrentUserId() ?? string.Empty;

			if (newEvent.OrganiserId != currentUser)
			{
				return RedirectToAction(nameof(All));
			}

			newEvent.Name = model.Name;
			newEvent.Description = model.Description;
			newEvent.Start = startDate;
			newEvent.End = endDate;
			newEvent.CreatedOn = DateTime.Now;
			newEvent.TypeId = model.TypeId;
			newEvent.OrganiserId = GetCurrentUserId() ?? string.Empty;

			await contex.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var model = await contex.Events
				.Where(e => e.Id == id)
				.AsNoTracking()
				.Select(e => new EventDetailsViewModel()
				{
					Id = e.Id,
					Name = e.Name,
					Description = e.Description,
					CreatedOn = e.CreatedOn.ToString(EventDataFormatAdd),
					Start = e.Start.ToString(EventDataFormatAdd),
					End = e.End.ToString(EventDataFormatAdd),
					Organiser = e.Organiser.UserName,
					Type = e.Type.Name
				})
				.FirstOrDefaultAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Joined()
		{
			string currentUser = GetCurrentUserId() ?? string.Empty;

			List<EventJoinViewModel> model = await contex.EventsParticipants
				.Where(ep => ep.HelperId == currentUser)
				.AsNoTracking()
				.Select(ep => new EventJoinViewModel()
				{
					Id = ep.EventId,
					Name = ep.Event.Name,
					Organiser = ep.Event.Organiser.UserName,
					Type = ep.Event.Type.Name,
					Start = ep.Event.Start.ToString(EventDataFormatAdd)
				})
				.ToListAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Join(int id)
		{
			string currentUser = GetCurrentUserId() ?? string.Empty;

			List<EventParticipant> userEvents = await contex.EventsParticipants
				.Where(ep => ep.HelperId == currentUser)
				.ToListAsync();

			if (userEvents.Any(ep => ep.EventId == id))
			{
				return RedirectToAction(nameof(All));
			}

			var ep = new EventParticipant()
			{
				EventId = id,
				HelperId = currentUser
			};

			await contex.EventsParticipants.AddAsync(ep);
			await contex.SaveChangesAsync();

			return RedirectToAction(nameof(Joined));
		}

		[HttpGet]
		public async Task<IActionResult> Leave(int id)
		{
			string currentUser = GetCurrentUserId() ?? string.Empty;

			EventParticipant? ep = await contex.EventsParticipants
				.Where(ep => ep.EventId == id)
				.Where(ep => ep.HelperId == currentUser)
				.FirstOrDefaultAsync();

			if (ep == null)
			{
				throw new ArgumentException("Invalid Id");
			}

			contex.EventsParticipants.Remove(ep);
			await contex.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}


		private async Task<List<Type>> GetTypes()
		{
			return await contex.Types.ToListAsync();
		}

		private string? GetCurrentUserId()
		{
			return User.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
