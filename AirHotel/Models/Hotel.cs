namespace AirHotel.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string HotelDescription { get; set; } = string.Empty; //string? gjør at classen kan være null. Velger å sette som Emtpy

        public string? ImageUrl { get; set; }    //string? kan være null

        public virtual List<OrderItem>? OrderItems { get; set; }    
    }
}
