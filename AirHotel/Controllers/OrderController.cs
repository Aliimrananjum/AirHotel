using AirHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirHotel.ViewModels;
using AirHotel.DAL;

namespace AirHotel.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Hotel> _hotelRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;

        public OrderController(IGenericRepository<Order> orderRepository, IGenericRepository<Hotel> hotelRepository, IGenericRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _hotelRepository = hotelRepository;
            _orderItemRepository = orderItemRepository;
        }
        public async Task<IActionResult> Table()
        {
            var orders = await _orderRepository.GetAll();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrderItem()
        {
            var hotels = await _hotelRepository.GetAll();
            var orders = await _orderRepository.GetAll();
            var createOrderItemViewModel = new CreateOrderItemViewModel
            {
                OrderItem = new OrderItem(),

                ItemSelectList = hotels.Select(item => new SelectListItem
                {
                    Value = item.HotelId.ToString(),
                    Text = item.HotelId.ToString() + ": " + item.HotelName
                }).ToList(),

                OrderSelectList = orders.Select(order => new SelectListItem
                {
                    Value = order.OrderId.ToString(),
                    Text = "Order" + order.OrderId.ToString() + ", Date: " + order.OrderDate + ", Customer: " + order.Customer.Name
                }).ToList(),
            };
            return View(createOrderItemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(OrderItem orderItem)
        {
            try
            {
                var newHotel = await _hotelRepository.GetEntitylById(orderItem.HotelId);
                var newOrder = await _orderRepository.GetEntitylById(orderItem.OrderId);

                if (newHotel == null || newOrder == null) {
                    return BadRequest("Item or Order not found");
                }

                var newOrderItem = new OrderItem
                {
                    HotelId = orderItem.HotelId,
                    Hotel = newHotel,
                    Quantity = orderItem.Quantity,
                    OrderId = orderItem.OrderId,
                    Order = newOrder,
                };

                newOrderItem.OrderItemPrice = orderItem.Quantity * newOrderItem.Hotel.Price;

                await _orderItemRepository.Create(newOrderItem);
                return RedirectToAction(nameof(Table));
            }
            catch
            {
                return BadRequest("OrderItem Creation Failed");
            }
        }
    }
}
