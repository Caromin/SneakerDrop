using Microsoft.EntityFrameworkCore;
using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakerDrop.Code.Helpers
{
    public static class AddressHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddAddressById(Address address)
        {
            _db.Attach(address.User);
            _db.Addresses.Add(address);
            _db.Entry(address.User).State = EntityState.Unchanged;

            return _db.SaveChanges() == 1;
        }
        public static List<Address> GetAddressInfoById(Address address)
        {
            var dbAddressInfo = _db.Addresses.Where(a => a.User.UserId == address.User.UserId).ToList();

            return dbAddressInfo;

        }

        // working, back to original
        public static bool EditAddressInfoById(Address address)
        {
            var editAddress = _db.Addresses.Where(a => a.AddressId == address.AddressId).FirstOrDefault();

            editAddress.Street = address.Street;
            editAddress.City = address.City;
            editAddress.State = address.State;
            editAddress.PostalCode = address.PostalCode;

            return _db.SaveChanges() == 1;
        }
        public static bool DeleteAddressInfoById(Address address)
        {
            _db.Addresses.RemoveRange(_db.Addresses.Where(a => a.AddressId == address.AddressId));

            return _db.SaveChanges() == 1;
        }
        public static Address GetAddressInfoByAddressId(int userid)
        {
            return _db.Addresses.Where(a => a.User.UserId == userid).FirstOrDefault();
        }

    }
}
