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
        {
            string inputPassed = productInfoDomainModel.ProductTitle;

            var query = from item in _db.ProductInfos
                        where item.ProductTitle.Contains(inputPassed)
                        select item;

            var productInfos = query.ToList<ProductInfo>().Take(10);

            return (System.Collections.Generic.List<SneakerDrop.Domain.Models.ProductInfo>)productInfos;
        }

        public static ProductInfo SingleProductInfo(ProductInfo product)
        {
            var result = _db.ProductInfos.Where(p => p.ProductTitle == product.ProductTitle).FirstOrDefault();

            return result;
        }

        public static IEnumerable<ProductInfo> GetAllRecentProducts()
        {
            var query = _db.ProductInfos.Where(x => x.ProductInfoId > 1)
                .OrderByDescending(x => x.ProductInfoId).ToList().Take(4);

            return query;
        }

        public static IEnumerable<ProductInfo>FindSearch(string input)
        {

            var query = from item in _db.ProductInfos
                        where item.ProductTitle.Contains(input)
                        select item;

            var searchstuff = query.ToList<ProductInfo>().Take(20);

            return searchstuff;

        }
    }
}
