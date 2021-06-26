using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Be;
using MySql.Data.MySqlClient;

namespace Dal
{
    public class Dalhosp_gen_turnos
    {
        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("ORL");


        MySqlCommand cmm;

        public string Regenerar(hosp_gen_turnos v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            string sal = "";
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                 

                cmm = null;
                string cmdTxt = "sp_regenerar_hosp";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_ini",v_obj.Fecha_Inicio);
                cnn.AgregarParametroAComando(cmm, "v_fin", v_obj.Fecha_Fin);
                cnn.AgregarParametroAComando(cmm, "v_id", v_obj.Id);
                MySqlParameter outSal22 = cnn.AgregarParametroAComandoOut(cmm, "v_sal");
                cmm.ExecuteNonQuery();
               sal = outSal22.Value.ToString();
              

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);
            }
            return sal;

        }

        public void Alta(hosp_gen_turnos v_obj)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();

                string cmdTxt = " insert into hosp_gen_turnos (Cada_Cuanto, Id_Profesional, Fecha_Inicio, Fecha_Fin, Id_Dias, Anulado, DE, CI, AP, TEL, WEB, Hora_Ini, Id_Servicio, Obs,Turno, Hora_Fin) values ('" + v_obj.Cada_Cuanto + "', '" + v_obj.Id_Profesional + "', STR_TO_DATE('" + v_obj.Fecha_Inicio + "','%d/%m/%Y')  ,STR_TO_DATE('" + v_obj.Fecha_Fin + "','%d/%m/%Y')  , '" + v_obj.Id_Dias + "', '" + v_obj.Anulado + "', '" + v_obj.DE + "', '" + v_obj.CI + "', '" + v_obj.AP + "', '" + v_obj.TEL + "', '" + v_obj.WEB + "', STR_TO_DATE('" + v_obj.Hora_Ini + "','%H:%i')  , '" + v_obj.Id_Servicio + "', '" + v_obj.Obs + "', '" + v_obj.Turno + "' , STR_TO_DATE('" + v_obj.Hora_Fin + "','%H:%i')  )  ";
                  
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt,cnn2,"");
                cmm.ExecuteNonQuery();


                cmdTxt = " select max(Id) cant from hosp_gen_turnos   ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                MySqlDataReader lector = cnn.ExecuteReader(cmm);
                string ds="0";
                while (lector.Read())
                {
                 //   kx_cbtes entidad = new kx_cbtes();
                    ds = DalModelo.VerifStringMysql(lector, "cant");
                }
                lector.Close();

                cmm = null;
                cmdTxt = "generar_tur_hosp";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_id",ds);              
                cmm.ExecuteNonQuery();
                  
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);
            }

        }


        public void Modificacion(hosp_gen_turnos v_obj)
        {
            try
            {
                string cmdTxt = "update hosp_gen_turnos set Cada_Cuanto='" + v_obj.Cada_Cuanto + "' , Id_Profesional='" + v_obj.Id_Profesional + "' , Fecha_Inicio='" + v_obj.Fecha_Inicio + "' , Fecha_Fin='" + v_obj.Fecha_Fin + "' , Id_Dias='" + v_obj.Id_Dias + "' , Anulado='" + v_obj.Anulado + "' , DE='" + v_obj.DE + "' , CI='" + v_obj.CI + "' , AP='" + v_obj.AP + "' , TEL='" + v_obj.TEL + "' , WEB='" + v_obj.WEB + "' , Hora_Ini='" + v_obj.Hora_Ini + "' , Id_Servicio='" + v_obj.Id_Servicio + "' , Obs='" + v_obj.Obs + "' where id ='" + v_obj.Id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }

        public void ModFechafinGenVar(string ids, string fecha)
        {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                cmm = null;
                string cmdTxt = "sp_cambiar_Fechafin_hosp";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_Id_Gen", ids);
                cnn.AgregarParametroAComando(cmm, "v_fin", fecha);
                cmm.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }
        }
        public List<hosp_gen_turnos> Listar(string v_serv, string v_prof)
        {

            List<hosp_gen_turnos> lista = new List<hosp_gen_turnos>();
            try
            {



                string cmdTxt = " select g.Id,g.Cada_Cuanto, p.Nombre Id_Profesional,DATE_FORMAT(g.Fecha_Inicio, '%d/%m/%Y')  Fecha_Inicio,DATE_FORMAT(g.Fecha_Fin, '%d/%m/%Y')  Fecha_Fin,d.Nombre Id_Dias, g.Anulado, g.DE, g.CI, g.AP, g.TEL, g.WEB,DATE_FORMAT(g.Hora_Ini, '%H:%i') Hora_Ini,DATE_FORMAT(g.Hora_Fin, '%H:%i') Hora_Fin, s.Nombre Id_Servicio, g.Obs, g.Turno from hosp_gen_turnos g , hosp_dias d, hosp_personal p, hosp_servicios  s where g.Id_Profesional = p.Id and g.Id_Servicio= s.Id and g.Id_Dias = d.Id  and g.Id_Profesional = '" + v_prof + "' and g.Id_Servicio= '" + v_serv + "'  order by g.Id_Dias,g.Id desc";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_gen_turnos entidad = new hosp_gen_turnos();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cada_Cuanto = DalModelo.VerifStringMysql(lector, "Cada_Cuanto");
                    entidad.Id_Profesional = DalModelo.VerifStringMysql(lector, "Id_Profesional");
                    entidad.Fecha_Inicio = DalModelo.VerifStringMysql(lector, "Fecha_Inicio");
                    entidad.Fecha_Fin = DalModelo.VerifStringMysql(lector, "Fecha_Fin");
                    entidad.Id_Dias = DalModelo.VerifStringMysql(lector, "Id_Dias");
                    entidad.Anulado = DalModelo.VerifStringMysql(lector, "Anulado");
                    entidad.DE = DalModelo.VerifStringMysql(lector, "DE");
                    entidad.CI = DalModelo.VerifStringMysql(lector, "CI");
                    entidad.AP = DalModelo.VerifStringMysql(lector, "AP");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "TEL");
                    entidad.WEB = DalModelo.VerifStringMysql(lector, "WEB");
                    entidad.Hora_Ini = DalModelo.VerifStringMysql(lector, "Hora_Ini");
                    entidad.Hora_Fin = DalModelo.VerifStringMysql(lector, "Hora_Fin");
                    entidad.Id_Servicio = DalModelo.VerifStringMysql(lector, "Id_Servicio");
                    entidad.Obs = DalModelo.VerifStringMysql(lector, "Obs");
                    entidad.Turno = DalModelo.VerifStringMysql(lector, "Turno");
                    lista.Add(entidad);

                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }


            return lista;
        }

        public void EliminarGen(string ids) {
            MySqlConnection cnn2 = new MySqlConnection();
            try
            {
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                  cmm = null;
                  string cmdTxt = "BorrarGen";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");
                cnn.AgregarParametroAComando(cmm, "v_id", ids);              
                cmm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }
        }
        public string existe_gen(string id_prof, string id_serv, string id_dia, string fecha_ini, string Turno)
        {
            string cant = "0";
            try
            {
                string cmdTxt = "select count(*) cant from hosp_gen_turnos where Id_Profesional= '" + id_prof + "' and Id_Servicio ='" + id_serv + "' and Id_Dias = '" + id_dia + "'  and Turno = '" + Turno + "'  and Fecha_Fin  >= STR_TO_DATE('" + fecha_ini + "','%d/%m/%Y')";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);
               
                while (lector.Read())
                {
                    cant = DalModelo.VerifStringMysql(lector, "cant");
                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }


            return cant;
        }


        public void Eliminar(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from hosp_gen_turnos  where Id='" + v_id + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                cnn.ExecuteNonQuery(cmm);
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }

        public Int32 maxId()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from hosp_gen_turnos  ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    id = DalModelo.VeriIntMysql(lector, "cant");
                }

                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

            return id;
        }

    }

}
