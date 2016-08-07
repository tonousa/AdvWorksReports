using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebForms1
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader reader = null;
            string connString = @"Data Source=MARK-PC\SQLEXPRESS;" +
                "Initial Catalog=AdventureWorks2008R2; Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            string queryString = "SELECT Top 5500    Sales.Customer.CustomerID, Sales.SalesOrderHeader.SalesOrderID, Sales.SalesOrderDetail.ProductID, Production.Product.Name FROM         Sales.SalesOrderDetail INNER JOIN Sales.SalesOrderHeader ON Sales.SalesOrderDetail.SalesOrderID = Sales.SalesOrderHeader.SalesOrderID INNER JOIN                      Production.Product ON Sales.SalesOrderDetail.ProductID = Production.Product.ProductID RIGHT OUTER JOIN                      Sales.Customer ON Sales.SalesOrderHeader.CustomerID = Sales.Customer.CustomerID ORDER BY Sales.Customer.CustomerID, Sales.SalesOrderHeader.SalesOrderID, Sales.SalesOrderDetail.ProductID";
            SqlCommand cmd = new SqlCommand(queryString, conn);

            try 
	        {	        
		        conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Response.Write(reader["Sales.Customer.CustomerID"] + "<br>");
                    //Response.Write(reader["CustomerID"] + " - ");
                    CustomerRow(reader["CustomerID"].ToString());
                    if (reader["SalesOrderID"] != DBNull.Value)
                    {
                        //Response.Write(reader["SalesOrderID"] + " - ");
                        OrderRow(reader["SalesOrderID"].ToString());
                    }
                    else
                    {
                        Response.Write(" no sales ");
                    }
                    ////Response.Write(reader["ProductID"] + " - " + reader["Name"]);
                    Response.Write("<br>");
                    //Response.Write(reader[0] + "<br>");

                } // while read
	        }
	        catch (Exception)
	        {
		        //throw;
	        }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        void CustomerRow(string custID)
        {
            Response.Write(custID + " - ");
        }

        void OrderRow(string orderID)
        {
            Response.Write(orderID + " - ");
        }
    }
}