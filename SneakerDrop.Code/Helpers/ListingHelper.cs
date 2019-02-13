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
            _db.Listings.Add(listing);

            return _db.SaveChanges() == 1;
        }

        public static List<Listing> GetAllListingsByProductName(Listing listing)
        {
            return _db.Listings.Where(l => l.ProductInfo.ProductTitle == listing.ProductInfo.ProductTitle).ToList();
        }
        public static Listing GetListingInfoByIdForOrder(Orders order)
        {
            Listing dbInfo = _db.Listings.Where(l => l.ListingId == order.Listing.ListingId)
                                         .Include(l => l.ProductInfo)
                                         .Include(l => l.User)
                                         .Include(l => l.ProductInfo.Brand)
                                         .Include(l => l.ProductInfo.Type)
                                         .FirstOrDefault();

            return dbInfo;
        }
        public static ProductInfo GetProductInfoByIdForListing(Listing listing)
        {
            ProductInfo dbInfo = _db.ProductInfos.Where(p => p.ProductInfoId == listing.ProductInfo.ProductInfoId).FirstOrDefault();

            return dbInfo;
        }
    }
}
