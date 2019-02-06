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
            return true;
        }

        public static List<Orders> GetOrdersById(Orders orders)
        {
            List<Orders> results = _db.Orders.Where(o => o.User.UserId == orders.User.UserId).ToList();

            return results;
        }

        public static bool CancelOrderById(Orders orders)
        {
            _db.Orders.RemoveRange(_db.Orders.Where(o => o.OrderId == orders.OrderId));

            return _db.SaveChanges() == 1;
        }

    }
}
