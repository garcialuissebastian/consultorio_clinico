using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Be;
using Bll;
using System.Web.Services;
namespace HardSoft.App
{
    public partial class Homes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ////////////////////////// prueba
                ///
                //mysql_usuarios lista = Bllmysql_usuarios.DameInstancia().Obtener_usuario("ORL");
                //HttpContext.Current.Session["ActiveSession"] = DateTime.Now.ToString();
                //HttpContext.Current.Session["UsuarioActual"] = lista;


                ///////////////////////////////

                if (HttpContext.Current.Session["UsuarioActual"] == null)
                {
                    Response.Redirect("http://www.corlquilmes.com.ar", false);
                }
                //if (!Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CORL"))
                //{
                //    Response.Redirect("http://www.corlquilmes.com.ar", false);

                //}
                if (!Page.IsPostBack)
                {
                    mysql_usuarios user = (mysql_usuarios)Session["UsuarioActual"];
                    lblEmpresa2.Text = user.USUARIO;
                    //if (user.CUILCUIT_LIMPIO != null && !string.IsNullOrEmpty(user.CUILCUIT_LIMPIO))
                    //{

                    //    lblCuit2.Text = user.CUILCUIT_LIMPIO;

                    //}
                    //else { lblCuit2.Text = user.DOC_NO.ToString(); }

                    if (Bllmysql_usuarios.DameInstancia().TienePermisoPara("VER_KARDEX"))
                    {

                        user.Config = Bllkx_config.DameInstancia().Obtener_config(user.ID_USUARIO)[0];
                        Session["UsuarioActual"] = user;
                        byte[] arrImg = user.Config.Logo;
                        string salida = "data:image/png;base64," + Convert.ToBase64String(arrImg);
                        img.ImageUrl = salida;
                        lblCuit2.Text = user.Config.Cuit;
                        lblModo.Text = "Modo: "+user.Config.Modo;
                    }
                    else {

                        img.ImageUrl = "/images/Milogo.png";
                    }

                    if (Bll.Bllmysql_usuarios.DameInstancia().TienePermisoPara("CONSULTORIO"))
                    {
                        user = (mysql_usuarios)Session["UsuarioActual"];
                        user.Descripcion = Bllkx_config.DameInstancia().Descripcion_Nombre(user.ID_USUARIO);
                        Session["UsuarioActual"] = user;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblModo.Text = ex.ToString();
                //Response.Redirect("../Default.aspx");
            }

        }
          [WebMethod()]

        public static void bitacora(Be.Bitacora v_obj)
        {
            try
            {
                v_obj.id_user = ((mysql_usuarios)HttpContext.Current.Session["UsuarioActual"]).ID_USUARIO;
                v_obj.config_user = ((mysql_usuarios)HttpContext.Current.Session["UsuarioActual"]).Config.id;
                v_obj.msg = v_obj.msg.Replace("'", string.Empty);

                Bll.Bllkx_config.DameInstancia().bitacora(v_obj);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}