using Microsoft.AspNetCore.Mvc;
using AirHotel.Models;
using AirHotel.ViewModels;
using AirHotel.DAL;

namespace AirHotel.Controllers
{
    public class HotelController : Controller
        {

            private readonly IGenericRepository<Hotel> _hotelRepository;

            public HotelController(IGenericRepository<Hotel> hotelRepository)
            {
                _hotelRepository = hotelRepository;
            }

       

            public async Task<IActionResult> Table() {
                var hotels = await _hotelRepository.GetAll();
                var hotelListViewModel = new HotelListViewModel(hotels, "Liste");
                return View(hotelListViewModel); 
            }

            public async Task<IActionResult> Grid() {
            var hotels = await _hotelRepository.GetAll();
                var hotelListViewModel = new HotelListViewModel(hotels, "Grid");
                return View(hotelListViewModel);
            }

            public async Task<IActionResult> Card() {
               var  hotels = await _hotelRepository.GetAll();
            var hotelListViewModel = new HotelListViewModel(hotels, "Card");
                return View(hotelListViewModel);
            }


        public async Task<IActionResult> Details(int id) {
                var hotel = await _hotelRepository.GetEntitylById(id);
   
                if(hotel == null)
                    return BadRequest("Hotel not found");
                return View(hotel);
            }
       

            [HttpGet]
            public IActionResult Create(){
                return View();
            }

  

           [HttpPost]
           public async Task<IActionResult> Create(Hotel hotel)
           {
               if (ModelState.IsValid)
               {
                   await _hotelRepository.Create(hotel);
                   return RedirectToAction(nameof(Card));
               }
               return View(hotel);
           }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _hotelRepository.GetEntitylById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _hotelRepository.Update(hotel);
                
                return RedirectToAction(nameof(Card));
            }
            return View(hotel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _hotelRepository.GetEntitylById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hotelRepository.Delete(id);
            return RedirectToAction(nameof(Card));
        }


        /*
         public IActionResult Table()
         {
             var hotelListe = GetHotel();
             ViewBag.CurrentViewName = "Liste";
             return View(hotelListe);
         }

         public IActionResult Grid()
         {
             var hotelListe = GetHotel();
             ViewBag.CurrentViewName = "Grid";
             return View(hotelListe);
         }
       

        public List<Hotel> GetHotel()
        {
            var hotelListe = new List<Hotel>(); //lager en liste av type Hotel

            var hotel1 = new Hotel
            {
                HotelId = 1,
                HotelName = "Ali's Hotel",
                Price = 150,
                HotelDescription = "Rett vi Oslo S",
                ImageUrl = "/images/hotel1.jpg"
            };

            var hotel2 = new Hotel
            {
                HotelId = 2,
                HotelName = "JP's Hotel",
                Price = 200,
                HotelDescription = "Rett vi OsloMet",
                ImageUrl = "/images/hotel2.jpg"
            };

            var hotel3 = new Hotel
            {
                HotelId = 3,
                HotelName = "Gisle's Hotel",
                Price = 200,
                HotelDescription = "Rett vi Nasjonal",
                ImageUrl = "/images/hotel3.jpg"
            };

            var hotel4 = new Hotel
            {
                HotelId = 4,
                HotelName = "Olivers's Hotel",
                Price = 400,
                HotelDescription = "Rett vi slottet",
                ImageUrl = "/images/AirRooms/AirRoom1.jpg"
            };

            hotelListe.Add(hotel1);
            hotelListe.Add(hotel2);
            hotelListe.Add(hotel3);
            hotelListe.Add(hotel4);
            return hotelListe;
        }
        */
    }

}
