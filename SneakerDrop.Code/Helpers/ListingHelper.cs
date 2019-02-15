using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class ListingHelper
    {
        public static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddListingById(Listing listing)
        {
            _db.Attach(listing.User);
            _db.Attach(listing.ProductInfo);
            _db.Listings.Add(listing);
            _db.Entry(listing.User).State = EntityState.Detached;
            _db.Entry(listing.ProductInfo).State = EntityState.Detached;

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

        public static List<Listing> GetallListingsByListingId(int selectedListingId)
        {
            return _db.Listings.Where(l => l.ListingId == selectedListingId).ToList();
        }

        public static List<Listing> GetAllListingById(int id)
        {
            return _db.Listings.Where(l => l.User.UserId == id).ToList();
        }

        public static ProductInfo GetProductIdByListingId(int listingId)
        {
            Listing result1 = _db.Listings.Where(l => l.ListingId == listingId).Include(p => p.ProductInfo).FirstOrDefault();

            return _db.ProductInfos.Where(p => p.ProductInfoId == result1.ProductInfo.ProductInfoId).FirstOrDefault();
        }

        public static bool DeleteListingById(int listingId)
        {
            _db.Listings.RemoveRange(_db.Listings.Where(l => l.ListingId == listingId));

            return _db.SaveChanges() == 1;
        }
    }
}
