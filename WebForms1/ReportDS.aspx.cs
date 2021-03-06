﻿using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace WebForms1
{
    public partial class ReportDS : System.Web.UI.Page
    {
        //DataSet dataset = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            ////SqlDataReader reader = null;
            string connString = @"Data Source=MARK-PC\SQLEXPRESS;" +
                "Initial Catalog=AdventureWorks2008R2; Integrated Security=True";
            //string connString = @"Data Source=staging01;" +
            //    "Initial Catalog=AdventureWorks2008; Integrated Security=True";

            string customerQry = "SELECT Sales.Customer.CustomerID, Sales.Customer.StoreID FROM Sales.Customer WHERE Sales.Customer.CustomerID > 12703 ORDER BY Sales.Customer.CustomerID";
            string orderQry = "SELECT Sales.SalesOrderHeader.SalesOrderID, Sales.SalesOrderHeader.CustomerID, OrderDate FROM Sales.SalesOrderHeader INNER JOIN Sales.Customer ON Sales.SalesOrderHeader.CustomerID = Sales.Customer.CustomerID WHERE Sales.Customer.CustomerID > 12703";
            string orderDetailQry = "SELECT Sales.SalesOrderDetail.ProductID, Sales.SalesOrderDetail.SalesOrderID, UnitPrice FROM Sales.SalesOrderDetail INNER JOIN Sales.SalesOrderHeader ON Sales.SalesOrderDetail.SalesOrderID = Sales.SalesOrderHeader.SalesOrderID INNER JOIN Sales.Customer ON Sales.SalesOrderHeader.CustomerID = Sales.Customer.CustomerID WHERE Sales.Customer.CustomerID > 12703";

            DataSet dataset = new DataSet();
            DataTable orderDetail_DT = new DataTable("OrderDetail");
            DataTable order_DT = new DataTable("Order");
            DataTable customer_DT = new DataTable("Customer");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter orderDetail_DA = new SqlDataAdapter();
                orderDetail_DA.SelectCommand = new SqlCommand(orderDetailQry, conn);
                orderDetail_DA.Fill(orderDetail_DT);

                SqlDataAdapter order_DA = new SqlDataAdapter();
                order_DA.SelectCommand = new SqlCommand(orderQry, conn);
                order_DA.Fill(order_DT);

                SqlDataAdapter customer_DA = new SqlDataAdapter();
                customer_DA.SelectCommand = new SqlCommand(customerQry, conn);
                customer_DA.Fill(customer_DT);
            }

            dataset.Tables.Add(orderDetail_DT);
            dataset.Tables.Add(order_DT);
            dataset.Tables.Add(customer_DT);

            DataRelation cust_order = new DataRelation("cust_order",
                dataset.Tables["Customer"].Columns["CustomerID"],
                dataset.Tables["Order"].Columns["CustomerID"]);
            DataRelation order_orderDetail = new DataRelation("order_orderDetail",
                dataset.Tables["Order"].Columns["SalesOrderID"],
                dataset.Tables["OrderDetail"].Columns["SalesOrderID"]);

            dataset.Relations.Add(cust_order);
            dataset.Relations.Add(order_orderDetail);

            CustomersPrint(ref dataset);


            //Response.Write("lalalalal");
            Response.End();

        }

        void CustomersPrint(ref DataSet dataset)
        {
            //Response.Write("<table rules='all'>");
            foreach (DataRow CustomerRow in dataset.Tables["Customer"].Rows)
            {
                Response.Write("<table rules='all'>");
                CustomerRowPrint(CustomerRow);
                Response.Write("</table>");
                Response.Flush();
            }
            //Response.Write("</table>");
        }

        void CustomerRowPrint(DataRow row)
        {
            StringBuilder customerString = new StringBuilder();
            customerString.Append("<tr>");
            customerString.Append("<td>Customer: </td>");
            customerString.Append("<td>" + row["CustomerID"] + "</td>");
            customerString.Append("<td>");
            customerString.Append(DBNull.Value.Equals(row["StoreID"]) ? "null" : "not null");
            customerString.Append("</td>");
            customerString.Append("</tr>");

            Response.Write(customerString.ToString());
            //Response.Write("Customer: " + row["CustomerID"] + " <br> ");
            //DataRow[] orderRows = row.GetChildRows("cust_order");
            OrderRowPrint(row);
        }

        void OrderRowPrint(DataRow row)
        {
            DataRow[] orderRows = row.GetChildRows("cust_order");
            if (orderRows.Length == 0) return;

            Response.Write("<td colspan='3'><table rules='all'>");
            foreach (DataRow orderRow in orderRows)
            {
                StringBuilder orderString = new StringBuilder();
                orderString.Append("<tr>");
                orderString.Append("<td>Order: </td>");
                orderString.Append("<td>" + orderRow["SalesOrderID"] + "</td>");
                orderString.Append("<td>" + orderRow["CustomerID"] + "</td>");
                orderString.Append("<td>" + orderRow["OrderDate"] + "</td>");
                orderString.Append("</tr>");

                Response.Write(orderString.ToString());
            }
            Response.Write("</table></td>");
            // to be deleted
        }
    }
}

