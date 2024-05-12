namespace Gallery.Entity
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal? Amount { get; set; }

        // Bir ürüne birden fazla resim eklemek için Image koleksiyonu
        public ICollection<Image> Images { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
    
}