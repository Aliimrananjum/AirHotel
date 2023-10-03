using AirHotel.ViewModels;
using AirHotel.Models;

namespace AirHotel.ViewModels

{
    public class HotelListViewModel
      
    {
        public IEnumerable<Hotel> Hotels;
        public string? CurrentViewName;

        public HotelListViewModel(IEnumerable<Hotel> hotels, string? currentViewName)
        {
            Hotels = hotels;
            CurrentViewName = currentViewName;
        }
    }
}
