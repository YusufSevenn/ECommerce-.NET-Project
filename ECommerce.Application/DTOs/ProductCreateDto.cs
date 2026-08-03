namespace ECommerce.Application.DTOs
{
    public class ProductCreateDto
    {
        public String Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}