using System;
using System.Collections.Generic;
using System.Linq;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class OrderHelper
    {
        public static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddOrderById(Orders orders)
        {
            _db.Orders.Add(orders);
            _db.Attach(orders.Listing);
            _db.Attach(orders.Listing.ProductInfo);
            _db.Attach(orders.Listing.ProductInfo.Brand);
            _db.Attach(orders.Listing.ProductInfo.Type);
            _db.Attach(orders.Listing.User);
            _db.Attach(orders.Payment);
            _db.Attach(orders.Payment.User);
            _db.Attach(orders.User);



            return _db.SaveChanges() == 1;
        }

        public static List<Orders> GetOrdersById(Orders orders)
        {
            List<Orders> results = _db.Orders.Where(o => o.User.UserId == orders.User.UserId).ToList();

            return results;
        }

        public static bool CancelOrderByOrderId(Orders orders)
        {
            _db.Orders.RemoveRange(_db.Orders.Where(o => o.OrderId == orders.OrderId));

            return _db.SaveChanges() == 1;
        }

    }
}
