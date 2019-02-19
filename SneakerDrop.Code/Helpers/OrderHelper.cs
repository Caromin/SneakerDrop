using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class OrderHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddOrderById(Orders orders)
        {

            _db.Entry(orders.User).State = EntityState.Detached;
            _db.Entry(orders.Listing).State = EntityState.Detached;
            _db.Entry(orders.Payment).State = EntityState.Detached;
            _db.Entry(orders.Listing.User).State = EntityState.Detached;
            _db.Entry(orders.Listing.ProductInfo).State = EntityState.Detached;
            _db.Entry(orders.Listing.ProductInfo.Brand).State = EntityState.Detached;
            _db.Entry(orders.Listing.ProductInfo.Type).State = EntityState.Detached;
            _db.Entry(orders.Payment.User).State = EntityState.Detached;

            //_db.Attach(orders.User);
            _db.Attach(orders.Listing);
            _db.Attach(orders.Payment);
            _db.Attach(orders.Listing.User);
            _db.Attach(orders.Listing.ProductInfo);
            _db.Attach(orders.Listing.ProductInfo.Brand);
            _db.Attach(orders.Listing.ProductInfo.Type);
            _db.Attach(orders.Payment.User);

            _db.Orders.Add(orders);

            _db.Entry(orders.User).State = EntityState.Detached;
            _db.Entry(orders.Listing).State = EntityState.Detached;
            _db.Entry(orders.Payment).State = EntityState.Detached;
            _db.Entry(orders.Listing.User).State = EntityState.Detached;
            _db.Entry(orders.Listing.ProductInfo).State = EntityState.Detached;
            _db.Entry(orders.Listing.ProductInfo.Brand).State = EntityState.Detached;
            _db.Entry(orders.Listing.ProductInfo.Type).State = EntityState.Detached;
            _db.Entry(orders.Payment.User).State = EntityState.Detached;

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


        public static List<Orders> GetAllOrdersById(int id)
        {
            return _db.Orders.Include(u => u.User)
            .Include(u => u.Listing)
            .Include(u => u.Listing.ProductInfo)
            .Where(o => o.User.UserId == id).ToList();
        }

    }

}
