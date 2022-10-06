using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Product;
using Northwind.Services.Abstraction;
using System;
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

        [HttpPost]
        public async Task<IActionResult> CreateOrder(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                // create order dan order detail baru
                var products = productDto;
                var order = new OrderForCreateDto
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(3),
                    CustomerId = "SAID"
                };
                var orders = await _context.OrderService.FilterCustId(order.CustomerId, false);
                if (orders == null)
                {
                    var createOrder = _context.OrderService.CreateOrderId(order);
                    var orderDetail = new OrderDetailForCreateDto
                    {
                        ProductId = products.ProductId,
                        OrderId = createOrder.OrderId,
                        UnitPrice = (decimal)products.UnitPrice,
                        Quantity = Convert.ToInt16(products.QuantityPerUnit),
                        Discount = 0
                    };
                    _context.OrderDetailService.Insert(orderDetail);
                    return RedirectToAction("Checkout", new { id = createOrder.OrderId });
                }

                // orderid, productid ada tapi shippeddate null
                else
                {
                    OrderDetailDto orderDetails = new OrderDetailDto();
                    orderDetails = await _context.OrderDetailService.GetOrderDetail(orders.OrderId, products.ProductId, false);
                    if (orders.ShippedDate == null)
                    {
                        var orderDetail = new OrderDetailForCreateDto
                        {
                            ProductId = products.ProductId,
                            OrderId = orders.OrderId,
                            Quantity = Convert.ToInt16(products.QuantityPerUnit),
                            UnitPrice = (decimal)products.UnitPrice * Convert.ToInt16(products.QuantityPerUnit),
                            Discount = 0
                        };
                        if (orderDetails != null)
                        {
                            if (orderDetails.ProductId == products.ProductId)
                            {
                                var newQuantity = Convert.ToInt16(products.QuantityPerUnit);
                                orderDetails.OrderId = orderDetail.OrderId;
                                orderDetails.ProductId = orderDetail.ProductId;
                                orderDetails.Quantity += newQuantity;
                                orderDetails.UnitPrice += (decimal)products.UnitPrice * newQuantity;
                                _context.OrderDetailService.Edit(orderDetails);
                                return RedirectToAction("Index");
                                /*_context.OrderDetailService.Insert(orderDetail);
                                return RedirectToAction("Checkout", new { id = orders.OrderId });*/
                            }
                        }
                        else
                        {
                            _context.OrderDetailService.Insert(orderDetail);
                            return RedirectToAction("Index");
                        }
                        _context.OrderDetailService.Insert(orderDetail);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var createOrder = _context.OrderService.CreateOrderId(order);
                        var orderDetail = new OrderDetailForCreateDto
                        {
                            ProductId = products.ProductId,
                            OrderId = createOrder.OrderId,
                            UnitPrice = (decimal)products.UnitPrice,
                            Quantity = Convert.ToInt16(products.QuantityPerUnit),
                            Discount = 0
                        };
                        //_context.ProductService.CreateOrder(order, orderDetail);
                        _context.OrderDetailService.Insert(orderDetail);
                        return RedirectToAction("Checkout", new { id = createOrder.OrderId });
                    }
                }
            }

            return View(productDto);
        }

        // GET: ProductOnSale/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.ProductService.GetProductPhotoOnSalesById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<ActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.OrderService.GetOrderById((int)id, false);
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
