using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Abstraction;
using System.Collections;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ProductOnSaleController : Controller
    {
        private readonly IServiceManager _context;

        public ProductOnSaleController(IServiceManager context)
        {
            _context = context;
        }

        // GET: ProductOnSale
        public async Task<ActionResult> Index()
        {
            var productOnSale = await _context.ProductService.GetProductOnSales(false);
            return View(productOnSale);
        }

        // GET: ProductOnSale/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.ProductService.GetProductOnSalesById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductOnSale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductOnSale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnSale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductOnSale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnSale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductOnSale/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
