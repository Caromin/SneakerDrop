﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerDrop.Mvc.Models
{
    public class StaticCartViewModel
    {
        public static List<int> CartOfListId { get; set; }

        public static decimal CartTotal { get; set; }

        public static List<CreateNewListingViewModel> CartOfProducts { get; set; }

        public static List<int> QuantityBasedOnListId { get; set; }

        public StaticCartViewModel()
        {
            CartOfListId = new List<int>();
        }

        public static decimal TotalPrice(FindProductInfoViewModel findproductinfo)
        {
            var productsingle = findproductinfo.FindMatchingProductInfo(findproductinfo);
            decimal Price = 0;
            if (findproductinfo.HelperType == "buy")
            {
                foreach (var item in productsingle)
                {
                    if (item.ProductTitle == findproductinfo.ProductTitle)
                    {
                        Price += item.DisplayPrice;
                    }
                    Price += 0;
                }
            }
            if (findproductinfo.HelperType == "remove")
            {
                Price -= findproductinfo.DisplayPrice;
            }
            return Price;
        }

    }
}
