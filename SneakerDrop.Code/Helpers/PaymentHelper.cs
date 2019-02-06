﻿using System;
using System.Collections.Generic;
using System.Linq;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class PaymentHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddPaymentById(Payment payment)
        {
            _db.Payment.Add(payment);

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

        public static bool EditPaymentById(Payment payment)
        {
            var result = _db.Payment.Where(p => p.PaymentId == payment.PaymentId).FirstOrDefault();

            result.CCNumber = payment.CCNumber;
            result.CCUserName = payment.CCUserName;
            result.Month = payment.Month;
            result.Year = payment.Year;
            result.CVV = payment.CVV;

            return _db.SaveChanges() == 1;
        }

        public static bool DeletePaymentById(Payment payment)
        {
            _db.Payment.RemoveRange(_db.Payment.Where(p => p.PaymentId == payment.PaymentId));

            return _db.SaveChanges() == 1;
        }
    }
}
