using AirHotel.Models;
using Microsoft.EntityFrameworkCore;
namespace AirHotel.DAL
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            HotelDbContext context = serviceScope.ServiceProvider.GetRequiredService<HotelDbContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Hotels.Any())
            {
                var hotels = new List<Hotel>
            {
                new Hotel
                {
                    HotelName = "Oslo S Hotel",
                    Price = 150,
                    HotelDescription = "Rett i sentrum og kun 2 min fra Oslo City",
                    ImageUrl = "/images/AirHotel_1.jpg"
                },
                new Hotel
                {
                    HotelName = "Oslo City Hotel",
                    Price = 250,
                    HotelDescription = "I norges beste kjøpesenter.",
                    ImageUrl = "/images/AirHotel_2.jpg"
                },
                new Hotel
                {
                    HotelName = "Grand Hotel",
                    Price = 1250,
                    HotelDescription = "Norges beste hotel",
                    ImageUrl = "/images/AirHotel_3.jpg"
                },
                new Hotel
                {
                    HotelName = "Disco Hotel",
                    Price = 150,
                    HotelDescription = "Hotellet tilbyr fest 24/7",
                    ImageUrl = "/images/hotel1.jpg"
                },
                new Hotel
                {
                    HotelName = "Kebab Hotel",
                    Price = 50,
                    HotelDescription = "Du får servert kebab til frokost, lunch og middag.",
                    ImageUrl = "/images/hotel2.jpg"
                },
                 new Hotel
                {
                    HotelName = "Oslomet Hote",
                    Price = 1520,
                    HotelDescription = "Som student er dette hotellet for deg",
                    ImageUrl = "/images/hotel3.jpg"
                },

            };
                context.AddRange(hotels);
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
            {
                new Customer { Name = "Alice Hansen", Address = "Osloveien 1"},
                new Customer { Name = "Bob Johansen", Address = "Oslomet gata 2"},
            };
                context.AddRange(customers);
                context.SaveChanges();
            }

            if (!context.Orders.Any())
            {
                var orders = new List<Order>
            {
                new Order {OrderDate = DateTime.Today.ToString(), CustomerId = 1,},
                new Order {OrderDate = DateTime.Today.ToString(), CustomerId = 2,},
            };
                context.AddRange(orders);
                context.SaveChanges();
            }


            if (!context.OrderItems.Any())
            {
                var orderItems = new List<OrderItem>
            {
                new OrderItem { HotelId = 1, Quantity = 2, OrderId = 1},
                new OrderItem { HotelId = 2, Quantity = 1, OrderId = 1},
                new OrderItem { HotelId = 3, Quantity = 4, OrderId = 2},
            };
                foreach (var orderItem in orderItems)
                {
                    var item = context.Hotels.Find(orderItem.HotelId);
                    orderItem.OrderItemPrice = orderItem.Quantity * item?.Price ?? 0;
                }
                context.AddRange(orderItems);
                context.SaveChanges();
            }

            var ordersToUpdate = context.Orders.Include(o => o.OrderItems);
            foreach (var order in ordersToUpdate)
            {
                order.TotalPrice = order.OrderItems?.Sum(oi => oi.OrderItemPrice) ?? 0;
            }

            context.SaveChanges();


        }
    }
}
