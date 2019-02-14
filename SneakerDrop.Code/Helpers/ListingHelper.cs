using System;
using System.Collections.Generic;
using System.Linq;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class ListingHelper
    {
        public static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddListingById(Listing listing)
        {
            _db.Listings.Add(listing);

            return _db.SaveChanges() == 1;
        }

        public static List<Listing> GetAllListingsByProductName(Listing listing)
        {
            return _db.Listings.Where(l => l.ProductInfo.ProductTitle == listing.ProductInfo.ProductTitle).ToList();
        }

        public static List<Listing> GetAllListingsByProductInfoId(int selectedProductId)
        {
            return _db.Listings.Where(l => l.ProductInfo.ProductInfoId == selectedProductId).ToList();
        }
    }
}
