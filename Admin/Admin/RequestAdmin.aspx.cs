using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1.Admin
{
    public partial class RequestAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.NavigateUrl = ConfigurationManager.AppSettings["RequestUrl"] + "CelebList/CreateAllCeleb.ashx";
        }
    }
}