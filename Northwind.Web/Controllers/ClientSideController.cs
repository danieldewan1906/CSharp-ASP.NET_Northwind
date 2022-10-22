using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Base;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ClientSideController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;

        public ClientSideController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public IActionResult IndexJS()
        {
            return View();
        }

        public IActionResult IndexJQuery()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostCategory([FromBody]CategoryForCreateDto categoryForCreateDto)
        {
            var categoryDto = categoryForCreateDto;
            var result = new JsonResult(null)
            {
                Value = "Succeed"
            };
            return Ok(result);
        }

        public async Task<JsonResult> GetTotalProductByCategory(string categoryName)
        {
            var result = await _repositoryManager.ProductRepository.GetTotalProductCategoryById(categoryName);
            return Json(result);
        }

        public IActionResult IndexChart()
        {
            return View();
        }
    }
}
