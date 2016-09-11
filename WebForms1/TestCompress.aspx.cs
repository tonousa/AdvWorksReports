using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms1
{
    public partial class TestCompress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpResponse response = HttpContext.Current.Response;
            Response.Filter = new GZipStream(Response.Filter, CompressionMode.Compress);
            Response.AddHeader("Content-Encoding", "gzip");
            Response.Write("Hello");
        }
    }
}