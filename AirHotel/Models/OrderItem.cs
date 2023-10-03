namespace AirHotel.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = default!;
        public decimal OrderItemPrice {  get; set; }
    }
}
