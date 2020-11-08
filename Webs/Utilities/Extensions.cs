using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Webs.Utilities
{
    public static class Extensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fromWhich"></param>
        /// <param name="rowDelimiter"></param>
        /// <param name="columnDelimiter"></param>
        /// <param name="selectColumns"></param>
        /// <returns></returns>
        public static string GetRowsAsString(this Dictionary<Int32, Dictionary<Int32, SLCell>> source, SpreadsheetLight.SLDocument fromWhich, string rowDelimiter, string columnDelimiter, List<int> selectColumns)
        {

            if (source == null)
            {

            }
            var result = string.Join(rowDelimiter, source
                 .Skip(1)
                 .Select(v => v.GetRowAsString(fromWhich, columnDelimiter, selectColumns)));

            return result;
        }

        public static string[] splitHeader(this Dictionary<Int32, Dictionary<Int32, SLCell>> source, SpreadsheetLight.SLDocument fromWhich, string rowDelimiter, string columnDelimiter, List<int> selectColumns)
        {
            var headers = string.Join(rowDelimiter, source
              .Take(1)
              .Select(v => v.GetRowAsString(fromWhich, columnDelimiter, selectColumns))).Split(',');
            return headers;
        }

        public static string InnerXML(this XElement el)
        {
            var reader = el.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fromWhich"></param>
        /// <param name="columnDelimiter"></param>
        /// <param name="selectColumns"></param>
        /// <returns></returns>
        private static string GetRowAsString(this KeyValuePair<Int32, Dictionary<Int32, SLCell>> source, SpreadsheetLight.SLDocument fromWhich, string columnDelimiter, List<int> selectColumns)
        {
            if (source.Value == null)
            {
                return string.Empty;
            }

            var result = string.Join(", ", source.Value
            .Where(v => selectColumns == null || selectColumns.Count == 0 || selectColumns.Contains(v.Key))
            .Select(v => fromWhich.GetCellValueAsString(source.Key, v.Key)));

            return result;
        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fromWhich"></param>
        /// <param name="columnDelimiter"></param>
        /// <returns></returns>
        private static string GetRowAsString(this KeyValuePair<Int32, Dictionary<Int32, SLCell>> source, SpreadsheetLight.SLDocument fromWhich, string columnDelimiter)
        {
            return source.GetRowAsString(fromWhich, columnDelimiter, null);
        }


    }
}



