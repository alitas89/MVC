using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Core.Utilities.Dal
{
    public class Datatables
    {
        public static string FilterFabric(string arrFilter)
        {
            if (arrFilter.Length==0)
            {
                return "";
            }
            string sqlQuery = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var deserializedJson = jss.DeserializeObject(arrFilter);
            IList collection = (IList)deserializedJson;

            foreach (var item in collection)
            {
                var dictionary = (Dictionary<string, object>)item;
                if (dictionary["value"].ToString().Length > 0)
                {
                    sqlQuery += $" and {dictionary["columnName"]} like '%{dictionary["value"]}%' ";
                }
            }

            return sqlQuery;
        }
    }
}
