using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebForms1
{
    public class GetOrders : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string custID = context.Request.Params["custID"];
            string query = "SELECT Sales.SalesOrderHeader.SalesOrderID, Sales.SalesOrderHeader.CustomerID, OrderDate FROM Sales.SalesOrderHeader WHERE CustomerID = " + custID;
            StringBuilder sb = new StringBuilder();

            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorks2008"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);

            using (conn)
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                sb.Append("<tr><td><table>");
                while (reader.Read())
                {
                    sb.Append("<tr><td>" + reader["SalesOrderID"] + "</td></tr>");
                }
                sb.Append("</table></tr></td>");
            }

            //context.Response.Write("<tr><td>" + custID + "</td></tr>");
            context.Response.Write(sb.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}