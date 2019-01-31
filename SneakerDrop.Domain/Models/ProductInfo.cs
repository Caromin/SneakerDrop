using System;
namespace SneakerDrop.Domain.Models
{
    public class ProductInfo
    {
        public int ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int DisplayPrice { get; set; }

        public string ReleaseDate { get; set; }

        public string Color { get; set; }

    }
}
