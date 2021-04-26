using Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.UI.Utilities
{
    public static class Utility
    {
        // To format "," to nonescape character 
        public static string FormatRow(string row)
        {
            MatchCollection matchCollection = Regex.Matches(row, "\"([^\"]*)\"");
            for (int z = 0; z < matchCollection.Count; z++)
            {
                // replace , with $x$
                row = row.Replace(matchCollection[z].ToString().Trim(), matchCollection[z].ToString().Replace(",", "$x$").Replace("\"", ""));
                
            }
            return row;
        }
        
        // Map datarow to Car Model
        public static CarModel MapCarModel(string datarow)
        {

            // replace $x$ with , in date 
            string[] date = datarow.Split(',')[5].Trim().Replace("$x$", ",").Split('/');

            // get currency symbol CAD $
            const string canada = "en-CA";

            var ca = new RegionInfo(canada);
            var cai = new CultureInfo(canada)
            {
                NumberFormat =
                        {
                            CurrencySymbol = ca.ISOCurrencySymbol + ca.CurrencySymbol,
                            CurrencyPositivePattern = 2,
                            CurrencyNegativePattern = 12
                        }
            };
            return new CarModel()
            {
                DealNumber = Convert.ToInt32(datarow.Split(',')[0].Replace("$x$", ",")),
                CustomerName = datarow.Split(',')[1].Replace("$x$", ","),
                DealershipName = datarow.Split(',')[2].Replace("$x$", ","),
                Vehicle = datarow.Split(',')[3].Replace("$x$", ","),
                Price = Convert.ToDecimal(datarow.Split(',')[4].Replace("$x$", ",")).ToString("C", cai),
                Date = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[0]), Convert.ToInt32(date[1]))

            };
        }
    }
}