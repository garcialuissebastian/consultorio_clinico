using Be;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{


    public class Dalhosp_dias
    {
        MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("ORL");

        MySqlCommand cmm;

        public void Alta_hosp_dias(hosp_dias v_obj)
        {
            try
            {
                string cmdTxt = " insert into hosp_dias (Nombre) values ('" + v_obj.Nombre.ToUpper() + "')  ";

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


        public void Modificacion_hosp_dias(hosp_dias v_obj)
        {
            try
            {
                string cmdTxt = "update hosp_dias set Nombre='" + v_obj.Nombre.ToUpper() + "' where id ='" + v_obj.Id + "'   ";

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
        public List<hosp_dias> Listar_hosp_dias(string v_tipo, string v_valor)
        {

            List<hosp_dias> lista = new List<hosp_dias>();
            try
            {



                string cmdTxt = " select Id,Nombre from hosp_dias  ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select Id,Nombre from hosp_dias  where   Id='"+v_valor+"' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select Id,Nombre from hosp_dias  ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt = " select Id,Nombre from hosp_dias  ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_dias entidad = new hosp_dias();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
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


        public void Eliminar_hosp_dias(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from hosp_dias  where Id='" + v_id + "'";

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

        public Int32 maxId_hosp_dias()
        {
            Int32 id = 0;

            try
            {
                string cmdTxt = " select max(Id) as cant from hosp_dias  ";

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
