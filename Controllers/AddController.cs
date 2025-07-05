using Microsoft.AspNetCore.Mvc;
using gnisTask.Interface;
using gnisTask.Models;  
using System.Threading.Tasks;
using System; 
using System.Linq;

namespace gnisTask.Controllers
{
    public class AddController : Controller
    {
        private readonly IAddRepository _addRepository;

        public AddController(IAddRepository addRepository)
        {
            _addRepository = addRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCorporateCustomer(CorporateCustomer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newId = await _addRepository.InsertCorporateCustomer(customer);
                    return Json(new { success = true, message = $"Corporate Customer '{customer.CustomerName}' added successfully with ID: {newId}." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error adding corporate customer: {ex.Message}" });
                }
            }
            return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIndividualCustomer(IndividualCustomer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newId = await _addRepository.InsertIndividualCustomer(customer);
                    return Json(new { success = true, message = $"Individual Customer '{customer.CustomerName}' added successfully with ID: {newId}." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error adding individual customer: {ex.Message}" });
                }
            }
            return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductService(ProductService product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newId = await _addRepository.InsertProductService(product);
                    return Json(new { success = true, message = $"Product/Service '{product.ProductName}' added successfully with ID: {newId}." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error adding product/service: {ex.Message}" });
                }
            }
            return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
    }
}