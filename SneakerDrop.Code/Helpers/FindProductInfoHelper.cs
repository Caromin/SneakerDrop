using System;
using System.Collections.Generic;
using System.Linq;
using SneakerDrop.Domain.Models;

namespace SneakerDrop.Code.Helpers
{
    public static class FindProductInfoHelper
    {

        public static SneakerDropDbContext _db = new SneakerDropDbContext();

        public static List<ProductInfo> FindPossibleMatches(ProductInfo productInfoDomainModel)
        {   // stop at 20 closes matches
            var result = _db.ProductInfos.Where(p => p.ProductTitle == productInfoDomainModel.ProductTitle).ToList();

            return result;
        }
    }
}
