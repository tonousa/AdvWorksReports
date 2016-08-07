using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms1
{
    /// <summary>
    /// Summary description for GetOrders
    /// </summary>
    public class GetOrders : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("<h1>Hello World</h1>");
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