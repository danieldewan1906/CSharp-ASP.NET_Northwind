using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;
using X.PagedList;

namespace Northwind.Web.Controllers
{
    public class ProductsPagedServerController : Controller
    {
        private readonly IServiceManager _context;
        private readonly IUtilityService _utilityService;

        public ProductsPagedServerController(IServiceManager context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        // GET: ProductsService4
        public async Task<IActionResult> Index(string searchString, string currentFilter,
            string sortOrder, int? page, int? fetchSize)
        {
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 5;
            //ViewBag.psize = pageSize;

            // keep state searching value
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var productForSearch = await _context.ProductService.GetAllProduct(false);
            var totalRows = productForSearch.Count();
            if (!String.IsNullOrEmpty(searchString))
            {
                productForSearch = productForSearch.Where(p => p.ProductName.ToLower().Contains(searchString.ToLower()) ||
                p.Supplier.CompanyName.ToLower().Contains(searchString.ToLower()));
            }

            ViewBag.ProductNameSort = String.IsNullOrEmpty(sortOrder) ? "product_name" : "";
            ViewBag.UnitPriceSort = sortOrder == "price" ? "unit_price" : "price";

            var productForSort = from p in productForSearch
                                 select p;
            switch (sortOrder)
            {
                case "product_name":
                    productForSort = productForSort.OrderByDescending(p => p.ProductName);
                    break;
                case "price":
                    productForSort = productForSort.OrderBy(p => p.UnitPrice);
                    break;
                case "unit_price":
                    productForSort = productForSort.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    productForSort = productForSort.OrderBy(p => p.ProductName);
                    break;
            }

            var productDtoPaged = new StaticPagedList<ProductDto>(productForSort, pageIndex, pageSize - (pageSize - 1), totalRows);
            ViewBag.psize = productDtoPaged.Count;
            ViewBag.PagedList = new SelectList(new List<int> { 8, 15, 20 });
            return View(productDtoPaged);
        }
        /*public async Task<IActionResult> Index(int? page)
        {
            
            return View();
        }*/

        [HttpPost]
        public async Task<IActionResult> CreateProductPhoto(ProductPhotoGroupDto productPhotoDto)
        {
            if (ModelState.IsValid)
            {
                var productPhotoGroup = productPhotoDto;
                var listPhoto = new List<ProductPhotoCreateDto>();
                foreach (var itemPhoto in productPhotoGroup.AllPhoto)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var convertSize = (Int16)itemPhoto.Length;
                    var photo = new ProductPhotoCreateDto
                    {
                        PhotoFilename = fileName,
                        PhotoFileSize = (byte)convertSize,
                        PhotoFileType = itemPhoto.ContentType
                    };
                    listPhoto.Add(photo);
                }
                _context.ProductService.CreateProductManyPhoto(productPhotoGroup.productForCreateDto, listPhoto);
                return RedirectToAction(nameof(Index));
            }
            var allCategory = await _context.CategoryService.GetAllCategory(false);
            var allSupplier = await _context.SupplierService.GetAllSupplier(false);
            ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName");
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> EditProductPhoto(ProductPhotoGroupDto productPhotoDto)
        {
            /*if (ModelState.IsValid)
            {
                var productPhotoGroup = productPhotoDto;
                var listPhoto = new List<ProductPhotoCreateDto>();
                foreach (var itemPhoto in productPhotoGroup.AllPhoto)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var convertSize = (Int16)itemPhoto.Length;
                    var photo = new ProductPhotoCreateDto
                    {
                        PhotoFilename = fileName,
                        PhotoFileSize = (byte)convertSize,
                        PhotoFileType = itemPhoto.ContentType
                    };
                    listPhoto.Add(photo);
                }
                _context.ProductService.CreateProductManyPhoto(productPhotoGroup.productForCreateDto, listPhoto);
                return RedirectToAction(nameof(Index));
            }*/
            /*var allCategory = await _context.CategoryService.GetAllCategory(false);
            var allSupplier = await _context.SupplierService.GetAllSupplier(false);
            ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName");*/
            return View("Edit");
        }

        // GET: ProductsService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.ProductService.GetProductById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsService/Create
        public async Task<IActionResult> Create()
        {
            var allCategory = await _context.CategoryService.GetAllCategory(false);
            var allSupplier = await _context.SupplierService.GetAllSupplier(false);
            ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName");
            return View();
        }

        // POST: ProductsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] ProductForCreateDto product)
        {
            if (ModelState.IsValid)
            {
                _context.ProductService.Insert(product);
                return RedirectToAction(nameof(Index));
            }

            var allCategory = await _context.CategoryService.GetAllCategory(false);
            var allSupplier = await _context.SupplierService.GetAllSupplier(false);
            ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }*/

        // GET: ProductsService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.ProductService.GetProductPhotoById((int)id, true);
            if (product == null)
            {
                return NotFound();
            }
            /*var allCategory = await _context.CategoryService.GetAllCategory(false);
            var allSupplier = await _context.SupplierService.GetAllSupplier(false);*/
            /*ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName", product.productForCreateDto.CategoryId);*/
            /*ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName", product.productForCreateDto.SupplierId);*/
            return View(product);
        }

        // POST: ProductsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] ProductDto product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProductService.Edit(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var allCategory = await _context.CategoryService.GetAllCategory(false);
            var allSupplier = await _context.SupplierService.GetAllSupplier(false);
            ViewData["CategoryId"] = new SelectList(allCategory, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(allSupplier, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }*/

        // GET: ProductsService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.ProductService.GetProductById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.ProductService.GetProductById((int)id, false);
            _context.ProductService.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
