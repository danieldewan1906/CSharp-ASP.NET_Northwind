using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Services.Abstraction;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class OrderController : Controller
    {
        private IServiceManager _context;

        public OrderController(IServiceManager context)
        {
            _context = context;
        }

        // GET: OrderController1
        public async Task<ActionResult> Index()
        {
            var order = await _context.OrderService.GetAllOrder(false);
            return View(order);
        }

        // GET: OrderController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController1/Create
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

        // GET: OrderController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.OrderService.GetOrderById((int)id, true);
            if (order == null)
            {
                return NotFound();
            }
            /*var allProduct = await _context.ProductService.GetAllProduct(false);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName", orderDetail.ProductId);*/
            return View(order);
        }

        // GET: OrderController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController1/Delete/5
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
