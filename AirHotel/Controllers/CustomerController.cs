using AirHotel.DAL;
using AirHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirHotel.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerController(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IActionResult> Table()
        {
            var customers = await _customerRepository.GetAll();
            return View(customers);
        }
    }
}
