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
    public partial class HcMovil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 
            if (!Page.IsPostBack)
            {
                // comentado el entrarn con el celular
                // lo descomente para q no aparezca en google ver despues


                if (Request.QueryString["SIS"] != "MOVIL")
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);
                }



                // 
              

            }







        }
 
    }
}