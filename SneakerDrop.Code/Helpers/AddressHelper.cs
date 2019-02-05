using SneakerDrop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SneakerDrop.Code.Helpers
{
   public  class AddressHelper
    {
        private static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static bool AddAddress(Address address)
        {
            

            _db.Addresses.Add(address);
            return _db.SaveChanges() == 1;
        }
        public static Address GetAddressInfoById(Address address)
        {
            Address dbAddressInfo = _db.Addresses.Where(a => a.AddressId == address.AddressId).FirstOrDefault();

            return dbAddressInfo;
        }

    }
}
