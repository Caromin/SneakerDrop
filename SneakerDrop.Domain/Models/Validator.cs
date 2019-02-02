using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SneakerDrop.Domain.Models
{
  public class Validator
    {
        public static bool ValidateString(object a)
        {
            var properties = a.GetType().GetProperties().ToList();

            foreach (var prop in properties)
            {
                if (prop.GetType() == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace(prop.GetValue(prop).ToString()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool ValidateNumber(object b)
        {
            var properties = b.GetType().GetProperties().ToList();

            foreach (var prop in properties)
            {
                if (prop.GetType() == typeof(long) || prop.GetType() == typeof(int))
                {
                    if ((long)prop.GetValue(prop) < 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool ValidateMoney(object c)
        {
            var properties = c.GetType().GetProperties().ToList();

            foreach (var prop in properties)
            {
                if (prop.GetType() == typeof(decimal))
                {
                    if ((decimal)prop.GetValue(prop) > 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
