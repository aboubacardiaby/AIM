using Data;
using LumenWorks.Framework.IO.Csv;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Webs.Utilities
{
    public class FileLoader
    {
        public static List<tblBeverageSale> GetSaleItems(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "The argument \"source\" contained a null value instead of a stream.");
            }

            
            List<string> builder = new List<string>();
            List<tblBeverageSale> list = new List<tblBeverageSale>();
            using (CsvReader csv =
           new CsvReader(new StreamReader(source), true))
            {
                int fieldCount = csv.FieldCount;
               
                string[] headers = csv.GetFieldHeaders();
              
                var rows = csv.ToList();
                foreach (var item in rows)
                {
                    string result = string.Join("|", item);
                    builder.Add(result);
                }

                foreach (var item in builder)
                {
                    try
                    {

                    
                    string[] row = item.Split('|');
                    DateTime shipdate = Convert.ToDateTime(row[6].ToString());
                    DateTime PromisedDate = Convert.ToDateTime(row[8].ToString());
                    string item_desc = null;
                    if (row[20].ToString().Contains("\""))
                    {
                        item_desc = row[20].ToString().Replace("\"", "");
                        }
                        else
                        {
                            item_desc = row[20].ToString();
                        }
                    var table = new tblBeverageSale()
                    {

                        Bol_Number = row[0].ToString(),
                        Ship_To = row[1].ToString(),
                        Ship_To_Aplha_Name = row[2].ToString(),
                        Sold_To = row[3].ToString(),
                        Sold_To_Alpha_Name = row[4].ToString(),
                        Ship_From = row[5].ToString(),
                        Ship_Date = shipdate,
                        Ship_Time = row[7].ToString(),
                        Promised_Date = PromisedDate,
                        Promised_Time = row[9].ToString(),
                        Carrier = row[10].ToString(),
                        Trailer_Number = row[11].ToString(),
                        Seal_Number = row[12].ToString(),
                        Customer_PO = row[13].ToString(),
                        Customer_Release = row[14].ToString(),
                        Customer_Reference = row[15].ToString(),
                        Item_Number = row[16].ToString(),
                        Quantity = row[17].ToString(),
                        Eaches = row[18].ToString(),
                        Customer_Item = row[19].ToString(),
                        Item_Description = item_desc,
                        Size = row[21].ToString(),
                        Neck_Size = row[22].ToString(),
                        CAN_END = row[23].ToString(),
                        CreatedBy = "Application",
                        CreateDate = DateTime.Now
                    };
                    list.Add(table);
                    }
                    catch 
                    {

                     
                    }
                }
            }
            return list;
        }
    }
}