using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class PaymentHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddPaymentById(Payment payment)
        {
            _db.Attach(payment.User);
            _db.Payment.Add(payment);
            _db.Entry(payment.User).State = EntityState.Detached;

            return _db.SaveChanges() == 1;
        }

        public static List<Payment> GetPaymentById(Payment payment)
        {
            List<Payment> results = _db.Payment.Where(p => p.User.UserId == payment.User.UserId).ToList();

            if (results != null)
            {
                return results;
            }
            return null;
        }

        public static bool DeletePaymentByPaymentId(Payment payment)
        {
            _db.Payment.RemoveRange(_db.Payment.Where(p => p.PaymentId == payment.PaymentId));

            return _db.SaveChanges() == 1;
        }
    }
}
