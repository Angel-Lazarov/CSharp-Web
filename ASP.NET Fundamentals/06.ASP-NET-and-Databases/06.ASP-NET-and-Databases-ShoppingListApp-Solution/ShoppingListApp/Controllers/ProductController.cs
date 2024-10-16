using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Data;
using ShoppingListApp.Models.Product;

namespace ShoppingListApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingListAppDbContext _data;

        public ProductController(ShoppingListAppDbContext data)
            => _data = data;

        public IActionResult Edit(int id)
        {
            var product = _data.Products.Find(id);

            return View(new ProductFormModel()
            {
                Name = product.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, Product model)
        {
            var product = _data.Products.Find(id);
            product.Name = model.Name;

            _data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Add()
            => View();

        [HttpPost]
        public IActionResult Add(ProductFormModel model)
        {
            var product = new Product()
            {
                Name = model.Name
            };

            _data.Products.Add(product);
            _data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var products = _data
                .Products
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToList();
            return View(products);
        }

       
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _data.Products.Find(id);

            _data.Products.Remove(product);
            _data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}