using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace WebForms1
{
    public class Procedures
    {
        public static StringBuilder myFunction(ref DataRow[] orderRows)
        {
            StringBuilder orderString = new StringBuilder();
            orderString.Append("<tr><td colspan='4'><table rules='all'>");
            foreach (DataRow orderRow in orderRows)
            {
                orderString.Append("<tr>");
                orderString.Append("<td>Order: </td>");
                orderString.Append("<td>" + orderRow["SalesOrderID"] + "</td>");
                orderString.Append("<td>" + orderRow["CustomerID"] + "</td>");
                orderString.Append("<td>" + orderRow["OrderDate"] + "</td>");
                orderString.Append("</tr>");

                //orderString.Append(orderString.ToString());
            }
            orderString.Append("</table></td></tr>");
            return orderString;
        }
    }
}