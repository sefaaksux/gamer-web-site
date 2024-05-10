namespace Gallery.Entity
{
    public class Image
    {
        public int ImageId { get; set; }
        
        public string Url { get; set; }
        // Diğer özellikler

        public int ProductId { get; set; } // Resmin hangi ürüne ait olduğunu belirten dış anahtar
        public Product Product { get; set; } // Bu resmin ait olduğu ürün
    }
    
}