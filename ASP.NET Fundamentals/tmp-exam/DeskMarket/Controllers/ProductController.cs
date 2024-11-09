using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static DeskMarket.Constraints;

namespace DeskMarket.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ProductController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> products = await dbContext.Products
                .Where(p => p.IsDeleted == false)
                .AsNoTracking()
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Price = p.Price,
                    ProductName = p.ProductName,
                    ImageUrl = p.ImageUrl,
                    IsSeller = GetCurrentUserId() == p.SellerId,
                    HasBought = p.ProductsClients.Any(pc => pc.ClientId == GetCurrentUserId())
                })
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddViewModel model = new AddViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            bool isValidDate = DateTime.TryParseExact(model.AddedOn, AddedOnDateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime addedOn);

            if (!isValidDate)
            {
                ModelState.AddModelError(nameof(model.AddedOn), "Start date must be in dd-MM-yyyy format.");
                model.Categories = await GetCategories();
                return View(model);
            }

            Product product = new Product()
            {
                ProductName = model.ProductName,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                AddedOn = addedOn,
                CategoryId = model.CategoryId,
                SellerId = GetCurrentUserId() ?? string.Empty
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            List<CartViewModel> myCart = await dbContext
                .Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.ProductsClients.Any(pc => pc.ClientId == currentUserId))
                .Select(p => new CartViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();


            return View(myCart);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product? p = await dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (p == null)
            {
                return BadRequest();
            }

            if (p.SellerId != GetCurrentUserId())
            {
                return Unauthorized();
            }

            EditViewModel model = new EditViewModel()
            {
                ProductName = p.ProductName,
                AddedOn = p.AddedOn.ToString(AddedOnDateFormat),
                CategoryId = p.CategoryId,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                SellerId = GetCurrentUserId() ?? string.Empty
            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            bool isValidDate = DateTime.TryParseExact(model.AddedOn, AddedOnDateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime addedOn);

            if (!isValidDate)
            {
                ModelState.AddModelError(nameof(model.AddedOn), "Start date must be in dd-MM-yyyy format.");
                model.Categories = await GetCategories();
                return View(model);
            }

            Product? entityProduct = await dbContext.Products.FindAsync(id);

            if (entityProduct == null || entityProduct.IsDeleted)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (entityProduct.SellerId != currentUserId)
            {
                return RedirectToAction(nameof(Index));
            }

            entityProduct.ProductName = model.ProductName;
            entityProduct.Price = model.Price;
            entityProduct.Description = model.Description;
            entityProduct.ImageUrl = model.ImageUrl;
            entityProduct.AddedOn = addedOn;
            entityProduct.CategoryId = model.CategoryId;
            entityProduct.SellerId = GetCurrentUserId() ?? string.Empty;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            DetailViewModel? model = await dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == id)
                .AsNoTracking()
                .Select(p => new DetailViewModel()
                {
                    Id = p.Id,
                    AddedOn = p.AddedOn.ToString(AddedOnDateFormat),
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    ProductName = p.ProductName,
                    Seller = p.Seller.UserName ?? string.Empty,
                    HasBought = p.ProductsClients.Any(pc => pc.ClientId == GetCurrentUserId())
                })
                .FirstOrDefaultAsync();



            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteViewModel? model = await dbContext.Products
                .Where(p => p.Id == id)
                .Where(p => p.IsDeleted == false)
                .AsNoTracking()
                .Select(p => new DeleteViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Seller = p.Seller.UserName ?? string.Empty,
                    SellerId = p.SellerId
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            Product? product = await dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == model.Id)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            Product? product = await dbContext.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductsClients)
                .FirstOrDefaultAsync();

            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (product.ProductsClients.Any(pc => pc.ClientId == currentUserId))
            {
                return RedirectToAction(nameof(Index));
            }

            product.ProductsClients.Add(new ProductClient()
            {
                ClientId = currentUserId,
                ProductId = product.Id
            });

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            Product? product = await dbContext.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductsClients)
                .FirstOrDefaultAsync();

            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException("Invalid Id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            ProductClient? pc = product.ProductsClients.FirstOrDefault(pc => pc.ClientId == currentUserId);

            if (pc != null)
            {
                product.ProductsClients.Remove(pc);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }


        private async Task<List<Category>> GetCategories()
        {
            return await dbContext.Categories.ToListAsync();
        }
        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
