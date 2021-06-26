using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll;
using Be;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Globalization; // sin esto no anda los reporte en el hosting
namespace HardSoft.App.ORL
{
    public partial class ListarHc : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("http://www.hardsoft.com.ar/App/ORL/HcMovil.aspx?SIS=MOVIL");
        }
 
    }
}