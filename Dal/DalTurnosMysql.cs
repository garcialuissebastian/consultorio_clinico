using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be;
using MySql.Data.MySqlClient;
using System.Data;
using System.ComponentModel;
namespace Dal
{
    public class DalTurnosMysql
    {
        private MySqlConectarSqlDBVarias cnn = new MySqlConectarSqlDBVarias("ORL");

        MySqlCommand cmm;
        MySqlConnection cnn2;


        public void AltaHc(hosp_hc v_obj)
        {
            try
            {
                byte[] bytes= null;
                if (v_obj.img != null) {
                     bytes = Convert.FromBase64String(v_obj.img.ToString());
                }
                
                

                  cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            string cmdTxt = " insert into hosp_hc (Paciente, Fecha, Sintoma, Diag_Presu, Tratamiento, Indicaciones, Obs,Peso, Temp, PreArt, Sat, FreCard, HGTN, FreResp , Turno, Img,Consultorio) values ('" + v_obj.Paciente + "', STR_TO_DATE('" + v_obj.Fecha + "','%d/%m/%Y') , '" + v_obj.Sintoma + "', '" + v_obj.Diag_Presu + "', '" + v_obj.Tratamiento + "', '" + v_obj.Indicaciones + "', '" + v_obj.Obs + "', '" + v_obj.Peso + "', '" + v_obj.Temp + "', '" + v_obj.PreArt + "', '" + v_obj.Sat + "', '" + v_obj.FreCard + "', '" + v_obj.HGTN + "', '" + v_obj.FreResp + "', '" + v_obj.Id + "', @image, '" + v_obj.Consultorio + "')  ";
          
           
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cnn.AgregarParametroAComando(cmm, "@image", bytes);
                cmm.ExecuteNonQuery();
                cmm = null;

                cmdTxt = " update hosp_turnos set Visto_Hc =  'SI' where  Id = '" + v_obj.Id + "'";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cmm.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                 
                cnn2.Close();
            }

        }
        public void cerrarHc(string v_pac, string obs)
        {
            try
            {
                string cmdTxt = "update  hosp_pacientes set Obs= '" + obs + "' where Id= '" + v_pac + "'   ";

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

        public void Modificar_pwd(string v_leg, string v_pass)
        {

            try
            {
                string cmdTxt = "update USUARIO_INTRANET set CLAVE = '" + v_pass + "'";

                cmdTxt += " where USUARIO = '" + v_leg.Trim() + "' ";
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


        public void ModificacionHc(hosp_hc v_obj)
        {
            try
            { 
                byte[] bytes = Convert.FromBase64String(v_obj.img.ToString()); 
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
               string cmdTxt = "update hosp_hc set  Img=@image,Fecha= STR_TO_DATE('" + v_obj.Fecha + "','%d/%m/%Y')   , Sintoma='" + v_obj.Sintoma + "' , Diag_Presu='" + v_obj.Diag_Presu + "' , Tratamiento='" + v_obj.Tratamiento + "' , Indicaciones='" + v_obj.Indicaciones + "' , Obs='" + v_obj.Obs + "',    Peso='" + v_obj.Peso + "' , Temp='" + v_obj.Temp + "', PreArt='" + v_obj.PreArt + "', Sat='" + v_obj.Sat + "' ,FreCard='" + v_obj.FreCard + "',HGTN='" + v_obj.HGTN + "',FreResp ='" + v_obj.FreResp + "'  where id ='" + v_obj.Id + "'   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "");
                cnn.AgregarParametroAComando(cmm, "@image", bytes);
              
                cmm.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Clone();
            }

        }

        public List<hosp_hc> ListarHc(string v_pac)
        {

            List<hosp_hc> lista = new List<hosp_hc>();
            try
            {



                string cmdTxt = " select    Id,  DATE_FORMAT(Fecha, '%d/%m/%Y')   Fecha, Sintoma, Diag_Presu, Tratamiento, Indicaciones, Obs   from hosp_hc  where Paciente='" + v_pac + "'  and Id  not in ( select    Id   from hosp_hc  where  Sintoma ='' and  Diag_Presu ='' and  Paciente='" + v_pac + "'  )    ORDER BY Id";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySql.Data.MySqlClient.MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_hc entidad = new hosp_hc();
                     
                    entidad.Id = DalModelo.VerifStringMysql(lector, "Id");
                    
                    entidad.Fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.Sintoma = DalModelo.VerifStringMysql(lector, "Sintoma");
                    entidad.Diag_Presu = DalModelo.VerifStringMysql(lector, "Diag_Presu");
                    entidad.Tratamiento = DalModelo.VerifStringMysql(lector, "Tratamiento");
                    entidad.Indicaciones = DalModelo.VerifStringMysql(lector, "Indicaciones");
                    entidad.Obs = DalModelo.VerifStringMysql(lector, "Obs");

                
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
        public  hosp_hc ListarHc_id(string v_pac)
        {

            List<hosp_hc> lista = new List<hosp_hc>();
            try
            {



                string cmdTxt = " select   img,Id,Paciente, DATE_FORMAT(Fecha, '%d/%m/%Y')   Fecha, Sintoma, Diag_Presu, Tratamiento, Indicaciones, Obs , Peso, Temp, PreArt, Sat, FreCard, HGTN, FreResp ,fn_consultorio_hc(Id) Consultorio  from hosp_hc  where Id='" + v_pac + "' ORDER BY Fecha DESC";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySql.Data.MySqlClient.MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_hc entidad = new hosp_hc();
                    entidad.img = MiImag(lector, "img");
                    entidad.Id = DalModelo.VerifStringMysql(lector, "Id");
                    entidad.Paciente = DalModelo.VerifStringMysql(lector, "Paciente");
                    entidad.Fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.Sintoma = DalModelo.VerifStringMysql(lector, "Sintoma");
                    entidad.Diag_Presu = DalModelo.VerifStringMysql(lector, "Diag_Presu");
                    entidad.Tratamiento = DalModelo.VerifStringMysql(lector, "Tratamiento");
                    entidad.Indicaciones = DalModelo.VerifStringMysql(lector, "Indicaciones");
                    entidad.Obs = DalModelo.VerifStringMysql(lector, "Obs");
                    entidad.Peso = DalModelo.VerifStringMysql(lector, "Peso");
                    entidad.Temp = DalModelo.VerifStringMysql(lector, "Temp");
                    entidad.PreArt = DalModelo.VerifStringMysql(lector, "PreArt");
                    entidad.Sat = DalModelo.VerifStringMysql(lector, "Sat");
                    entidad.FreCard = DalModelo.VerifStringMysql(lector, "FreCard");
                    entidad.HGTN = DalModelo.VerifStringMysql(lector, "HGTN");
                    entidad.FreResp = DalModelo.VerifStringMysql(lector, "FreResp");
                    entidad.Consultorio = DalModelo.VerifStringMysql(lector, "Consultorio");

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


            return lista[0];
        }

        public static string MiImag(MySqlDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return "";
            }
            else
            {

                byte[] arrImg = (byte[])lector[s];
                string salida = "data:image/png;base64," + Convert.ToBase64String(arrImg);

                return salida;

            }


        }
        public void EliminarHc(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from hosp_hc  where Id='" + v_id + "'";

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


        public List<Be.turnos> listar_turnos(DateTime v_fecha,string v_prof,string  v_serv)
        {

            List<turnos> LISTA = new List<turnos>();

            try
            {
                string cmdTxt = "SELECT t.Id,DATE_FORMAT(t.Hora, '%H:%i')  Hora FROM  hosp_turnos t  WHERE Id_servicio='" + v_serv + "' and Consultorio='" + v_prof + "'  and t.Doc_No  is null  and t.Fecha = '" + v_fecha.ToString("yyyy-MM-dd") + "' order by No_tur asc ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    turnos entidad = new turnos();


                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.hora = DalModelo.VerifStringMysql(lector, "Hora");
                    entidad.fecha = v_fecha.ToString("dd/MM/yyyy");
                    LISTA.Add(entidad);

                }
                //cerrar lector
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
            return LISTA;




        }

        public string Reservar(turnos v_tur)
        {
            try  // hc es null  en la hc listar uso `fn_id_paciente`(v_id int) as hc
            {
                string sal = "S";
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                string cmdTxt = "  select count(*) cant from hosp_turnos where id = '" + v_tur.id + "' and Doc_No is null ";
                cmm = new MySqlCommand(cmdTxt, cnn2);
                MySqlDataReader lector = cmm.ExecuteReader();
                Int32 v_sal = 0;
                while (lector.Read())
                {
                    v_sal = DalModelo.VeriIntMysql(lector, "cant");
                }
                lector.Close();
                if (v_sal > 0)
                {
                  
                    if (v_tur.esPaciente == "0") {
                        cmm = null;
                        cmdTxt = " insert into hosp_pacientes (    Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial  ) values ('" + v_tur.Fecha_Nac + "','" + v_tur.APELLIDO + "','" + v_tur.NOMBRE + "', '" + v_tur.DOCUMENTO + "', '" + v_tur.TEL + "', '" + v_tur.MAIL + "','" + v_tur.OBRASOCIAL + "')  ";                      
                        cmm = new MySqlCommand(cmdTxt, cnn2);
                        cmm.ExecuteNonQuery();

                        cmm = null;
                        cmdTxt = "  select max(Id) cant from hosp_pacientes ";
                        cmm = new MySqlCommand(cmdTxt, cnn2);
                        MySqlDataReader lector2 = cmm.ExecuteReader();
                     
                        while (lector2.Read())
                        {
                           v_tur.esPaciente= DalModelo.VerifStringMysql(lector2, "cant");
                        }
                        lector2.Close();
                    }

                    cmm = null;
                    cmdTxt = "  UPDATE hosp_turnos SET  Origen='WEB',  Fecha_Nac='" + v_tur.Fecha_Nac + "',  Apellido='" + v_tur.APELLIDO + "' ,Nombre='" + v_tur.NOMBRE + "' ,Doc_No='" + v_tur.DOCUMENTO + "',Tel='" + v_tur.TEL + "',Mail='" + v_tur.MAIL + "', ObraSocial ='" + v_tur.OBRASOCIAL + "', Obs ='" + v_tur.OBS + "',  HC ='" + v_tur.esPaciente + "'  WHERE Id = '" + v_tur.id + "'";
                    cmm = new MySqlCommand(cmdTxt, cnn2);
                    cmm.ExecuteNonQuery();
                    sal = "N";

                }


                return sal;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);

            }
        }



        public string ReservarIntranet(turnos v_tur)
        {
            try
            {

                // hc es null  en la hc listar uso `fn_id_paciente`(v_id int) as hc
                string sal = "S";
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();
                string cmdTxt = "  select count(*) cant from hosp_turnos where id = '" + v_tur.id + "' and Doc_No is null ";
                cmm = new MySqlCommand(cmdTxt, cnn2);
                MySqlDataReader lector = cmm.ExecuteReader();
                Int32 v_sal = 0;
                while (lector.Read())
                {
                    v_sal = DalModelo.VeriIntMysql(lector, "cant");
                }
                lector.Close();
                if (v_sal > 0)
                {

                    if (v_tur.esPaciente == "0")
                    {
                        cmm = null;
                        cmdTxt = " insert into hosp_pacientes (    Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, NroOS ) values ('" + v_tur.Fecha_Nac + "','" + v_tur.APELLIDO + "','" + v_tur.NOMBRE + "', '" + v_tur.DOCUMENTO + "', '" + v_tur.TEL + "', '" + v_tur.MAIL + "','" + v_tur.OBRASOCIAL + "','" + v_tur.NroOS + "')  ";
                        cmm = new MySqlCommand(cmdTxt, cnn2);
                        cmm.ExecuteNonQuery();

                        cmm = null;
                        cmdTxt = "  select max(Id) cant from hosp_pacientes ";
                        cmm = new MySqlCommand(cmdTxt, cnn2);
                        MySqlDataReader lector2 = cmm.ExecuteReader();

                        while (lector2.Read())
                        {
                            v_tur.esPaciente = DalModelo.VerifStringMysql(lector2, "cant");
                        }
                        lector2.Close();
                    }
                    else
                    {
                        cmm = null;
                        cmdTxt = " update hosp_pacientes  set Fecha_Nac= '" + v_tur.Fecha_Nac + "',  Apellido='" + v_tur.APELLIDO + "', Nombre='" + v_tur.NOMBRE + "', Tel='" + v_tur.TEL + "',Mail= '" + v_tur.MAIL + "', ObraSocial= '" + v_tur.OBRASOCIAL + "', 	Obs ='" + v_tur.OBS_Pac + "', NroOS ='" + v_tur.NroOS + "'  where Doc_No= '" + v_tur.DOCUMENTO + "'";
                        cmm = new MySqlCommand(cmdTxt, cnn2);
                        cmm.ExecuteNonQuery();
                    }

                   

                    cmm = null;
                    cmdTxt = "  UPDATE hosp_turnos SET Origen='INTRANET',    Fecha_Nac='" + v_tur.Fecha_Nac + "',  Apellido='" + v_tur.APELLIDO + "' ,Nombre='" + v_tur.NOMBRE + "' ,Doc_No='" + v_tur.DOCUMENTO + "',Tel='" + v_tur.TEL + "',Mail='" + v_tur.MAIL + "', ObraSocial ='" + v_tur.OBRASOCIAL + "', Obs ='" + v_tur.OBS + "',  HC ='" + v_tur.esPaciente + "'  WHERE Id = '" + v_tur.id + "'";
                    cmm = new MySqlCommand(cmdTxt, cnn2);
                    cmm.ExecuteNonQuery();
                    sal = "N";

                }


                return sal;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn2.Close();
                cnn.Close(cmm);

            }
        }
        public void altaDiaTurn(turnos tur)
        {

            try
            {
                string cmdTxt = "  insert into feriado (fecha,descrip) values (STR_TO_DATE('" + tur.fecha + "','%d/%m/%Y'), '" + tur.NOMBRE + "') ";
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

        public List<turnos> ListarDiasTurnos()
        {

            List<turnos> lista = new List<turnos>();
            try
            {
                string cmdTxt = " select   id, DATE_FORMAT(fecha, '%d/%m/%Y') Fecha  , descrip Nombre from feriado   ";

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    turnos entidad = new turnos();
                    entidad.id = DalModelo.VeriIntMysql(lector, "id");
                    entidad.fecha = DalModelo.VerifStringMysql(lector, "Fecha");

                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");

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

        public void tur_cancelarTur(string v_id)
        {

            try
            {

                string cmdTxt = " update hosp_turnos set Doc_No = null, Nombre=null, Apellido=null, Tel=null ,Mail =null,ObraSocial=null,Obs=null ,Fecha_Nac=null  where id= '" + v_id + "'";
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
        public void tur_monto(string v_vino, string v_monto, string v_obs, string ids, string v_prac, string v_consulta, string v_copago)
        {

            try
            {

                string cmdTxt = " update hosp_turnos set Vino_aud=Now(), Pago='" + v_monto + "' , Copago='" + v_copago + "', Pago_Obs='" + v_obs + "' ,Vino='" + v_vino + "' ,Consulta='" + v_consulta + "'  ,Practica='" + v_prac + "' where Id= '" + ids + "'";
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
        public void tur_cancelarDias(string v_id)
        {

            try
            {

                string cmdTxt = " delete from feriado  where id= '" + v_id + "'";
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
        public List<turnos> ListarTurPers(string v_tipo, string v_valor)
        {

            List<turnos> lista = new List<turnos>();
            try
            {
                string cmdTxt = "  ";

                if (v_tipo == "0")
                {
                    cmdTxt = "SELECT t.Id,DATE_FORMAT(t.Fecha, '%d/%m/%Y') Fecha,  date_format(t.Hora, '%H:%i') Hora,  t.Apellido,  t.Nombre,  t.Doc_No, t.Tel,  t.Mail,  concat(s.Nombre, ' - ', p.Nombre) Especialidad,   t.ObraSocial FROM hosp_turnos t, hosp_servicios s,  hosp_personal p   WHERE    t.Id_servicio = s.Id and   t.Consultorio=p.Id and t.Fecha > NOW()-INTERVAL 1 DAY  and   t.Doc_No ='" + v_valor + "' ";
                                   }               
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                MySqlDataReader lector = cnn.ExecuteReader(cmm);
                while (lector.Read())
                {
                    turnos entidad = new turnos();
                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.hora = DalModelo.VerifStringMysql(lector, "Hora");
                    entidad.APELLIDO= DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.DOCUMENTO = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.SERVICIO = DalModelo.VerifStringMysql(lector, "Especialidad");
                    entidad.OBRASOCIAL = DalModelo.VerifStringMysql(lector, "ObraSocial");
                   
                 
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

        public  turnos  TurAsis_byPac(string v_doc)
        {
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            List<turnos> lista = new List<turnos>();
            try
            {
                string cmdTxt = "SELECT  t.Visto_Hc, fn_obs_paciente(t.Id) datos , fn_id_paciente(t.Id)  HC, pac.Fecha_Nac,t.Obs,t.Id,DATE_FORMAT(t.Fecha, '%d/%m/%Y') Fecha,  date_format(t.Hora, '%H:%i') Hora, pac.Apellido,  pac.Nombre,  t.Doc_No, pac.Tel,  pac.Mail,  concat(s.Nombre, ' - ', p.Nombre) Especialidad, fn_os_hc(fn_id_paciente(t.Id))   ObraSocial ,   t.Vino,   t.Pago_Obs,   t.Pago ,  DATE_FORMAT(t.Vino_aud , '%H:%i')  Vino_aud, fn_dir_paciente(fn_id_paciente(t.Id))  direccion, pac.FOTO ";
             cmdTxt += "    FROM   hosp_turnos   t, hosp_servicios s,  hosp_personal p, hosp_pacientes pac ";
             cmdTxt += "  WHERE  pac.Doc_No=t.Doc_No  and  t.Id_servicio = s.Id and   t.Consultorio=p.Id and t.Doc_No ='" + v_doc + "'";

             cmm = new MySqlCommand(cmdTxt,cnn2);
                MySqlDataReader lector = cmm.ExecuteReader();

                while (lector.Read())
                {
                    turnos entidad = new turnos();
                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.hora = DalModelo.VerifStringMysql(lector, "Hora");
                    entidad.APELLIDO = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.DOCUMENTO = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.SERVICIO = DalModelo.VerifStringMysql(lector, "Especialidad");
                    entidad.OBRASOCIAL = DalModelo.VerifStringMysql(lector, "ObraSocial");
                    entidad.Vino = DalModelo.VerifStringMysql(lector, "Vino");
                    entidad.Vino_aud = DalModelo.VerifStringMysql(lector, "Vino_aud");
                    entidad.Pago = DalModelo.VerifStringMysql(lector, "Pago");
                    entidad.Pago_Obs = DalModelo.VerifStringMysql(lector, "Pago_Obs");
                    entidad.OBS = DalModelo.VerifStringMysql(lector, "Obs");
                    entidad.OBS_Pac = DalModelo.VerifStringMysql(lector, "datos");
                    entidad.Fecha_Nac = DalModelo.VerifStringMysql(lector, "Fecha_Nac");
                    entidad.Calle = DalModelo.VerifStringMysql(lector, "direccion");
                    entidad.HC = DalModelo.VerifStringMysql(lector, "HC");
                    entidad.Visto_Hc = DalModelo.VerifStringMysql(lector, "Visto_Hc");
                    entidad.FOTO = DalModelo.VerifStringMysql(lector, "FOTO");
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
                cnn2.Close();
                cnn.Close(cmm);
            }


            return lista[0];
        }


        public List<turnos> ListarTurAsisRp(string v_serv, string v_prof, string v_fecha, string v_fecha2)
        {
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            List<turnos> lista = new List<turnos>();
            try
            {
                string cmdTxt = "";
             
               cmdTxt = "SELECT  t.Visto_Hc, fn_obs_paciente(t.Id) datos , fn_id_paciente(t.Id)  HC, pac.Fecha_Nac,t.Obs,t.Id,DATE_FORMAT(t.Fecha, '%d/%m/%Y') Fecha,  date_format(t.Hora, '%H:%i') Hora, pac.Apellido,  pac.Nombre,  t.Doc_No, pac.Tel,  pac.Mail,  concat(s.Nombre, ' - ', p.Nombre) Especialidad, fn_os_hc(fn_id_paciente(t.Id))   ObraSocial ,   t.Vino,   t.Pago_Obs,  t.Pago ,  IFNULL( t.Copago, 0)   Copago ,  DATE_FORMAT(t.Vino_aud , '%H:%i')  Vino_aud, fn_dir_paciente(fn_id_paciente(t.Id))  direccion, t.Consulta, t.Practica FROM hosp_turnos t, hosp_servicios s,  hosp_personal p, hosp_pacientes pac  WHERE  pac.Doc_No=t.Doc_No  and  t.Id_servicio = s.Id and   t.Consultorio=p.Id    and  t.Id_servicio = '"+v_serv+"'  and t.Consultorio =  '"+v_prof+"'  and   t.Fecha between  STR_TO_DATE('" + v_fecha + "','%d/%m/%Y') and STR_TO_DATE('" + v_fecha2 + "','%d/%m/%Y')   and t.Doc_No is not null order by Fecha,Hora asc  ";
               cmm = new MySqlCommand(cmdTxt, cnn2);
                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    turnos entidad = new turnos();
                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.hora = DalModelo.VerifStringMysql(lector, "Hora");
                    entidad.APELLIDO = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.DOCUMENTO = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.SERVICIO = DalModelo.VerifStringMysql(lector, "Especialidad");
                    entidad.OBRASOCIAL = DalModelo.VerifStringMysql(lector, "ObraSocial");
                    entidad.Vino = DalModelo.VerifStringMysql(lector, "Vino");
                    entidad.Vino_aud = DalModelo.VerifStringMysql(lector, "Vino_aud");
                    entidad.Pago = DalModelo.VerifStringMysql(lector, "Pago");
                    entidad.Copago = DalModelo.VerifStringMysql(lector, "Copago");
                    entidad.Pago_Obs = DalModelo.VerifStringMysql(lector, "Pago_Obs");
                    entidad.OBS = DalModelo.VerifStringMysql(lector, "Obs");
                    entidad.OBS_Pac = DalModelo.VerifStringMysql(lector, "datos");
                    entidad.Fecha_Nac = DalModelo.VerifStringMysql(lector, "Fecha_Nac");
                    entidad.Calle = DalModelo.VerifStringMysql(lector, "direccion");
                    entidad.HC = DalModelo.VerifStringMysql(lector, "HC");
                    entidad.Visto_Hc = DalModelo.VerifStringMysql(lector, "Visto_Hc");
                    entidad.Consulta = DalModelo.VerifStringMysql(lector, "Consulta");
                    entidad.Practica = DalModelo.VerifStringMysql(lector, "Practica");
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
                cnn2.Close();
                cnn.Close(cmm);
            }


            return lista;
        }



        public List<turnos> ListarTurAsis(string v_serv, string v_prof, string v_fecha )
        {
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            List<turnos> lista = new List<turnos>();
            try
            {
                string cmdTxt = "wi271852_orl.sp_asistenciaHC";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");

                cnn.AgregarParametroAComando(cmm, "v_serv", v_serv);
                cnn.AgregarParametroAComando(cmm, "v_prof", v_prof);
                cnn.AgregarParametroAComando(cmm, "v_fecha", v_fecha);
               // cmdTxt = "SELECT  t.Visto_Hc, fn_obs_paciente(t.Id) datos , fn_id_paciente(t.Id)  HC, pac.Fecha_Nac,t.Obs,t.Id,DATE_FORMAT(t.Fecha, '%d/%m/%Y') Fecha,  date_format(t.Hora, '%H:%i') Hora, pac.Apellido,  pac.Nombre,  t.Doc_No, pac.Tel,  pac.Mail,  concat(s.Nombre, ' - ', p.Nombre) Especialidad, fn_os_hc(fn_id_paciente(t.Id))   ObraSocial ,   t.Vino,   t.Pago_Obs,   t.Pago ,  DATE_FORMAT(t.Vino_aud , '%H:%i')  Vino_aud, fn_dir_paciente(fn_id_paciente(t.Id))  direccion FROM hosp_turnos t, hosp_servicios s,  hosp_personal p, hosp_pacientes pac  WHERE  pac.Doc_No=t.Doc_No  and  t.Id_servicio = s.Id and   t.Consultorio=p.Id    and  t.Id_servicio = '" + v_serv + "' and t.Consultorio = '" + v_prof + "' and  t.Fecha = STR_TO_DATE('" + v_fecha + "','%d/%m/%Y') and t.Doc_No is not null order by Hora asc";
              //  cmm = new MySqlCommand(cmdTxt, cnn2);
                MySqlDataReader lector = cmm.ExecuteReader();
                while (lector.Read())
                {
                    turnos entidad = new turnos();
                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.fecha = DalModelo.VerifStringMysql(lector, "Fecha");
                    entidad.hora = DalModelo.VerifStringMysql(lector, "Hora");
                    entidad.APELLIDO = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.DOCUMENTO = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.SERVICIO = DalModelo.VerifStringMysql(lector, "Especialidad");
                    entidad.OBRASOCIAL = DalModelo.VerifStringMysql(lector, "ObraSocial");
                    entidad.Vino = DalModelo.VerifStringMysql(lector, "Vino");
                    entidad.Vino_aud = DalModelo.VerifStringMysql(lector, "Vino_aud");
                    entidad.Pago = DalModelo.VerifStringMysql(lector, "Pago");
                    entidad.Copago = DalModelo.VerifStringMysql(lector, "Copago");
                    entidad.Pago_Obs = DalModelo.VerifStringMysql(lector, "Pago_Obs");
                    entidad.OBS = DalModelo.VerifStringMysql(lector, "Obs");
                    entidad.OBS_Pac = DalModelo.VerifStringMysql(lector, "datos");
                    entidad.Fecha_Nac = DalModelo.VerifStringMysql(lector, "Fecha_Nac");
                    entidad.Calle = DalModelo.VerifStringMysql(lector, "direccion");
                    entidad.HC = DalModelo.VerifStringMysql(lector, "HC");
                    entidad.Visto_Hc = DalModelo.VerifStringMysql(lector, "Visto_Hc");
                    entidad.Consulta = DalModelo.VerifStringMysql(lector, "Consulta");
                    entidad.Practica = DalModelo.VerifStringMysql(lector, "Practica");
                    entidad.FOTO = DalModelo.VerifStringMysql(lector, "FOTO");
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
                cnn2.Close();
                cnn.Close(cmm);
            }


            return lista;
        }



        public DataTable Rp_turListarVestibular(string v_id)
        {
            try
            {

                string cmdTxt = " select   h.id, h.motivo,h.definicion_sub,h.romberg,h.unterberger,h.test_dix_hall,h.dedo_nariz,h.adiadococinesia, DATE_FORMAT(h.aud, '%d/%m/%y') as aud,  pac.Fecha_Nac Fecha_Nac , concat( pac.Apellido ,' ', pac.Nombre )  Paciente , pac.Doc_No  Doc from hosp_examen_vestibular h, hosp_pacientes pac  where h.id ='" + v_id + "' and h.Id_pac = pac.Id ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                DataTable dt = cnn.ExecuteDataTable(cmm, "vestibular");             

                return dt;

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


        public DataTable Rp_turListar(string v_prof, string v_serv, string v_fecha, string v_tur, string v_tipo)
        {
               
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            List<turnos> lista = new List<turnos>();
            try
            {
                string cmdTxt = "wi271852_orl.sp_asistenciaHC_RP";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt, cnn2, "SP");

                cnn.AgregarParametroAComando(cmm, "v_serv", v_serv);
                cnn.AgregarParametroAComando(cmm, "v_prof", v_prof);
                cnn.AgregarParametroAComando(cmm, "v_fecha", v_fecha);
                cnn.AgregarParametroAComando(cmm, "v_tipo", v_tipo);
              //  cmdTxt = " select    DATE_FORMAT( t.Fecha, '%d/%m/%y')  Fecha, date_format( t.Hora,'%H:%i') Hora,  pac.Apellido,   pac.Nombre ,  pac.Doc_No ,  pac.Tel ,  pac.Mail ,   t.No_tur No, p.Nombre Profesional,concat(s.Nombre,' - ' ,p.Nombre) Especialidad , pac.ObraSocial ObraSocial, t.OBS OBS , pac.Fecha_Nac Fecha_Nac from  hosp_turnos t, hosp_servicios s,hosp_personal p ,  hosp_pacientes pac  WHERE  pac.Doc_No=t.Doc_No  and    t.Id_servicio = s.Id and   t.Consultorio=p.Id and  t.Id_servicio = '" + v_serv + "' and t.Consultorio = '" + v_prof + "' and  t.Fecha = STR_TO_DATE('" + v_fecha + "','%d/%m/%Y')  order by Hora asc";
                //cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataAdapter adaptador = new MySqlDataAdapter(cmm);

                DataTable tabla = new DataTable("turno");
                adaptador.Fill(tabla);
                return tabla;
               

            }
            catch
            {
                throw;
            }
            finally
            {
                cnn2.Close();
            }



        }

        public DataSet Rp_Hc(string v_id)
        {

            try
            {
                DataSet ds = new DataSet();

                string cmdTxt = " select    t. Fecha_Nac,  concat(t.Nombre,' ', t.Apellido) Nombre,      t.Doc_No Doc ,   t.Tel ,   t.Mail ,  fn_os_hc(Id) OS from  hosp_pacientes t  where t.Id ='" + v_id + "'";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                DataTable dt = cnn.ExecuteDataTable(cmm, "cab");
                ds.Tables.Add(dt);


                cmdTxt = " select     DATE_FORMAT(Fecha, '%d/%m/%Y')   Fecha, Sintoma, Diag_Presu, fn_consultorio_hc(Id) Consultorio   from hosp_hc  where Paciente='" + v_id + "'   and Id  not in ( select    Id   from hosp_hc  where  Sintoma ='' and  Diag_Presu ='' and  Paciente='" + v_id + "'  )    ORDER BY Id";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                DataTable dt2 = cnn.ExecuteDataTable(cmm, "det");
                ds.Tables.Add(dt2);

                return ds;

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
        public DataTable Rp_tur(string v_id)
        {

            try
            {

                string cmdTxt = " select    DATE_FORMAT( t.Fecha, '%d/%m/%Y')  Fecha,   t.Hora,   t.Apellido,   t.Nombre ,   t.Doc_No ,   t.Tel ,   t.Mail ,   t.No_tur, p.Nombre Profesional,s.Nombre Especialidad ,t.ObraSocial  ObraSocial, t.OBS OBS from  hosp_turnos t, hosp_servicios s,hosp_personal p   where  t.Id_servicio = s.Id and t.Consultorio = p.Id and  t.Id ='" + v_id + "'";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                DataTable dt = cnn.ExecuteDataTable(cmm, "turno");


                return dt;

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
        public void CancelarFecha(string v_consul,string v_prof,string v_dia)
        {
            
           
            try
            {
                string cmdTxt = "insert into hosp_cancelar (Consultorio,Id_servicio,Fecha) values ('" + v_prof + "','" + v_consul + "',  STR_TO_DATE('" + v_dia + "','%d/%m/%Y')) ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
 cnn.ExecuteNonQuery(cmm);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }
        
        }

        public string SobreTurno(Be.turnos v_tur)
        {


            try  // hc es null  en la hc listar uso `fn_id_paciente`(v_id int) as hc
            {
                string cmdTxt = "";
                cnn2 = cnn.MySqlCrearNuevaConexion();
                cnn2.Open();


                if (v_tur.esPaciente == "0")
                {
                    cmm = null;
                    cmdTxt = " insert into hosp_pacientes (    Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, 	Obs ,NroOS ) values ('" + v_tur.Fecha_Nac + "','" + v_tur.APELLIDO + "','" + v_tur.NOMBRE + "', '" + v_tur.DOCUMENTO + "', '" + v_tur.TEL + "', '" + v_tur.MAIL + "','" + v_tur.OBRASOCIAL + "','" + v_tur.OBS_Pac + "','" + v_tur.NroOS+ "')  ";
                    cmm = new MySqlCommand(cmdTxt, cnn2);
                    cmm.ExecuteNonQuery();

                    cmm = null;
                    cmdTxt = "  select max(Id) cant from hosp_pacientes ";
                    cmm = new MySqlCommand(cmdTxt, cnn2);
                    MySqlDataReader lector2 = cmm.ExecuteReader();

                    while (lector2.Read())
                    {
                        v_tur.esPaciente = DalModelo.VerifStringMysql(lector2, "cant");
                    }
                    lector2.Close();
                }
                else {
                    cmm = null;
                    cmdTxt = " update hosp_pacientes  set Fecha_Nac= '" + v_tur.Fecha_Nac + "',  Apellido='" + v_tur.APELLIDO + "', Nombre='" + v_tur.NOMBRE + "', Tel='" + v_tur.TEL + "',Mail= '" + v_tur.MAIL + "', ObraSocial= '" + v_tur.OBRASOCIAL + "', 	Obs ='" + v_tur.OBS_Pac + "', NroOS ='" + v_tur.NroOS + "'  where  Doc_No= '" + v_tur.DOCUMENTO + "'";
                    cmm = new MySqlCommand(cmdTxt, cnn2);
                    cmm.ExecuteNonQuery();
                }

                cmm = null;

                cmdTxt = "insert into hosp_turnos (  No_tur ,  Hora ,  HC ,  Origen ,  Fecha ,  Id_servicio,  Consultorio ,  Doc_No ,  Nombre , Apellido ,  Tel ,  Mail ,  ObraSocial ,  Id_Gen_Turnos,  Obs,Aud_Tur)  ";
                cmdTxt += "  values ('99', STR_TO_DATE('" + v_tur.hora + "','%H:%i') ,'" + v_tur.esPaciente + "','SOBRETURNO', STR_TO_DATE('" + v_tur.fecha + "','%d/%m/%Y') , '" + v_tur.SERVICIO + "', '" + v_tur.PROFESIONAL + "', '" + v_tur.DOCUMENTO + "', '" + v_tur.NOMBRE + "', '" + v_tur.APELLIDO + "', '" + v_tur.TEL + "', '" + v_tur.MAIL + "', '" + v_tur.OBRASOCIAL + "','99', '" + v_tur.OBS + "', NOW())";

                cmm = new MySqlCommand(cmdTxt, cnn2);
                cmm.ExecuteNonQuery();


                cmdTxt = "SELECT max(Id)  cant from hosp_turnos ";
                cmm = new MySqlCommand(cmdTxt, cnn2);
                MySqlDataReader lector = cmm.ExecuteReader();

                string sal = "0";
                while (lector.Read())
                {
                    sal = DalModelo.VerifStringMysql(lector, "cant");
                }
                //cerrar lector
                lector.Close();
                return sal;

            }
            catch (Exception)
            {

                throw;
            }
            finally {
                cnn2.Close();
            }

        }
        public void ModPaciente(turnos v_obj)
        {
            try
            {
                string cmdTxt = "update hosp_pacientes set Fecha_Nac='" + v_obj.Fecha_Nac + "' , Apellido='" + v_obj.APELLIDO + "' , Nombre='" + v_obj.NOMBRE + "' , Doc_No='" + v_obj.DOCUMENTO + "' , Tel='" + v_obj.TEL + "' , Mail='" + v_obj.MAIL + "' , ObraSocial='" + v_obj.OBRASOCIAL+ "' , Obs='" + v_obj.OBS_Pac + "' , Aud=now() , Calle='" + v_obj.Calle + "' , Numero='" + v_obj.Numero + "' , Dpto='" + v_obj.Dpto + "' , Pais='" + v_obj.Pais + "' , Cp='" + v_obj.Cp + "' , Provincia='" + v_obj.Provincia + "' , Departamento='" + v_obj.Departamento + "' , Distrito='" + v_obj.Distrito + "' , NroOS='" + v_obj.NroOS + "' , FOTO='" + v_obj.FOTO + "'   where id ='" + v_obj.id + "'   ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);
                cnn.ExecuteNonQuery(cmm);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close(cmm);
            }

        }


    
        public List<turnos> ListarPaciente(string doc, string tipo)
        {
            List<turnos> lista = new List< turnos>();
            try
            {
                string cmdTxt = "SELECT  Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs ,  Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito ,NroOS,FOTO from  hosp_pacientes where Doc_No ='" + doc + "'";
             
                if(tipo=="4"){
                    cmdTxt = "SELECT  Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito,NroOS ,FOTO   from  hosp_pacientes where Doc_No ='" + doc + "'";
                           }
                if (tipo == "6")
                {
                    cmdTxt = "SELECT  Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs,  Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito ,NroOS , FOTO from  hosp_pacientes where Apellido like '" + doc.ToUpper() + "%'";
                }
                if (tipo == "0")
                {
                    cmdTxt = "SELECT  Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs , Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito,NroOS, FOTO  from  hosp_pacientes where Nombre like '" + doc.ToUpper() + "%'";
                }

                if (tipo == "99")
                {// id 
                    cmdTxt = "SELECT  Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs , Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito,NroOS, FOTO  from  hosp_pacientes where Id = '" + doc.ToUpper() + "'";
                }

                if (doc == "")
                {
                    cmdTxt = "SELECT    Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs , Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito,NroOS, FOTO  from  hosp_pacientes  LIMIT 1000";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    turnos entidad = new turnos();

                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.APELLIDO = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.DOCUMENTO = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.Fecha_Nac = DalModelo.VerifStringMysql(lector, "Fecha_Nac");
                    entidad.OBRASOCIAL = DalModelo.VerifStringMysql(lector, "OBRASOCIAL");
                    entidad.OBS_Pac = DalModelo.VerifStringMysql(lector, "Obs");

                    entidad.Calle = DalModelo.VerifStringMysql(lector, "Calle");
                    entidad.Numero = DalModelo.VerifStringMysql(lector, "Numero");
                    entidad.Dpto = DalModelo.VerifStringMysql(lector, "Dpto");
                    entidad.Pais = DalModelo.VeriIntMysql(lector, "Pais");
                    entidad.Cp = DalModelo.VerifStringMysql(lector, "Cp");
                    entidad.Provincia = DalModelo.VeriIntMysql(lector, "Provincia");
                    entidad.Departamento = DalModelo.VeriIntMysql(lector, "Departamento");
                    entidad.Distrito = DalModelo.VeriIntMysql(lector, "Distrito");
                    entidad.NroOS = DalModelo.VerifStringMysql(lector, "NroOS");
                    entidad.FOTO = DalModelo.VerifStringMysql(lector, "FOTO");


                    lista.Add(entidad);

                }
                //cerrar lector
                lector.Close();
                return lista;
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
        
        public turnos BuscarPaciente(string doc)
        {
            turnos entidad = null;
            try
            {
                string cmdTxt = "SELECT  Id,  Fecha_Nac,  Apellido, Nombre, Doc_No, Tel,Mail, ObraSocial, Obs,NroOS   from  hosp_pacientes where Doc_No ='" + doc + "'";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    entidad = new turnos();

                    entidad.id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.APELLIDO = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.DOCUMENTO = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.Fecha_Nac = DalModelo.VerifStringMysql(lector, "Fecha_Nac");
                    entidad.OBRASOCIAL = DalModelo.VerifStringMysql(lector, "OBRASOCIAL");
                    entidad.NroOS = DalModelo.VerifStringMysql(lector, "NroOS");
                    entidad.OBS_Pac = DalModelo.VerifStringMysql(lector, "Obs");

                }
                //cerrar lector
                lector.Close();
                return entidad;
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
        public turnos BuscarDoc(string doc) {
            turnos entidad = null;
            try
            {
                string cmdTxt = "SELECT t.Nombre,t.Apellido,t.Tel,t.Mail,t.Doc_No,t.id, CAST(concat(DATE_FORMAT(t.Fecha, '%d/%m/%Y'),'-',t.Hora) as CHAR ) as fecha1, CAST(concat(s.Nombre, '-', p.Nombre ) as char) as SERVICIO  from  hosp_turnos t, hosp_servicios s,hosp_personal p   where  t.Id_servicio = s.Id and t.Consultorio = p.Id  and t.Doc_No='" + doc + "' and  t.Fecha >= now() ";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);
               
                while (lector.Read())
                {

                    entidad = new turnos();

                    entidad.id = DalModelo.VeriIntMysql(lector, "id");
                    entidad.NOMBRE = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.APELLIDO = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.TEL = DalModelo.VerifStringMysql(lector, "Tel");
                    entidad.MAIL = DalModelo.VerifStringMysql(lector, "Mail");
                    entidad.DOCUMENTO= DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.fecha = DalModelo.VerifStringMysql(lector, "fecha1");
                    entidad.SERVICIO = DalModelo.VerifStringMysql(lector, "SERVICIO");
                 

                }
                //cerrar lector
                lector.Close();
                return entidad;
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
        public string tiene_tur(string v_doc)
        {
            string v_sal = "";
            try
            {

                string cmdTxt = "select CAST(concat(  DATE_FORMAT(Fecha, '%d/%m/%Y') , ' ', Hora) AS CHAR)  Fecha  from turnos where Doc_No ='" + v_doc + "' and  fecha > now()";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    v_sal = DalModelo.VerifStringMysql(lector, "Fecha");
                }

                lector.Close();
                return v_sal;

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

        public List<Be.Combos> CmdEspecialidadHC(string v_user) {

            List<Combos> LISTA = new List<Combos>();

            try
            {
                string cmdTxt = "SELECT Id,Nombre  from hosp_servicios";
                if (!string.IsNullOrEmpty(v_user)) {
                    cmdTxt = "SELECT s.Id,s.Nombre  from hosp_servicios s,hosp_personal p,hosp_servicios_personal sp where s.Id = sp.Id_Servicio and sp.Id_Personal = p.Id and p.Usuario ='" + v_user + "'";
                
                }
                
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                   Combos entidad = new Combos();


                    entidad.valor= DalModelo.VerifStringMysql(lector, "Id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "Nombre");
                   
                    LISTA.Add(entidad);

                }
                //cerrar lector
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
            return LISTA;


            
        }
        public List<Be.Combos> CmdEspecialidade( )
        {

            List<Combos> LISTA = new List<Combos>();

            try
            {
                string cmdTxt = "SELECT Id,Nombre  from hosp_servicios";
               

                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Combos entidad = new Combos();


                    entidad.valor = DalModelo.VerifStringMysql(lector, "Id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "Nombre");

                    LISTA.Add(entidad);

                }
                //cerrar lector
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
            return LISTA;



        }


        public List<Be.Combos> CmdProfesinalhC(string id_servicio, string v_user)
        {

            List<Combos> LISTA = new List<Combos>();

            try
            {
                string cmdTxt = "SELECT pe.Id, pe.Nombre from hosp_servicios_personal sp,  hosp_personal pe where sp.Id_Personal = pe.Id and sp.Id_Servicio ='" + id_servicio + "'";
                if (!string.IsNullOrEmpty(v_user))
                {
                    cmdTxt = "SELECT pe.Id, pe.Nombre from hosp_servicios_personal sp,  hosp_personal pe where sp.Id_Personal = pe.Id and  pe.Usuario ='" +v_user + "'";
                }
                
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Combos entidad = new Combos();


                    entidad.valor = DalModelo.VerifStringMysql(lector, "Id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "Nombre");

                    LISTA.Add(entidad);

                }
                //cerrar lector
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
            return LISTA;



        }
        public List<Be.Combos> CmdObraSociales(string id_servicio)
        {

            List<Combos> LISTA = new List<Combos>();

            try
            {
                string cmdTxt = "SELECT Id, Nombre from hosp_obra_sociales order by Nombre ";
             
                if (id_servicio!="") {
                    cmdTxt = "SELECT Id, Nombre from hosp_obra_sociales where Id ='" + id_servicio + "' order by Nombre ";
                }
        
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Combos entidad = new Combos();


                    entidad.valor = DalModelo.VerifStringMysql(lector, "Id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "Nombre");

                    LISTA.Add(entidad);

                }
                //cerrar lector
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
            return LISTA;



        }
    

        public List<Be.Combos> CmdProfesinal(string id_servicio)
        {

            List<Combos> LISTA = new List<Combos>();

            try
            {
                string cmdTxt = "SELECT pe.Id, pe.Nombre from hosp_servicios_personal sp,  hosp_personal pe where sp.Id_Personal = pe.Id and sp.Id_Servicio ='" + id_servicio + "'";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {
                    Combos entidad = new Combos();


                    entidad.valor = DalModelo.VerifStringMysql(lector, "Id");
                    entidad.descripcion = DalModelo.VerifStringMysql(lector, "Nombre");

                    LISTA.Add(entidad);

                }
                //cerrar lector
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
            return LISTA;



        }
    
        public string TurnosDisp(string v_id, string v_id2)
        {
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            string v_sal = "";
            try
            {

                string cmdTxt55 = "sp_disponibles_hosp";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt55, cnn2, "SP");

                cnn.AgregarParametroAComando(cmm, "V_PROF", v_id);
                cnn.AgregarParametroAComando(cmm, "V_SERV", v_id2);
                MySqlParameter outSal22 = cnn.AgregarParametroAComandoOut(cmm, "V_SAL");
                cmm.ExecuteNonQuery();
                v_sal = outSal22.Value.ToString();
                 

                return v_sal;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }



        }

        public string TurnosDispList(string v_id, string v_id2)
        {
            cnn2 = cnn.MySqlCrearNuevaConexion();
            cnn2.Open();
            string v_sal = "";
            try
            {

                string cmdTxt55 = "sp_calendarioListar_hosp";
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt55, cnn2, "SP");

                cnn.AgregarParametroAComando(cmm, "V_PROF", v_id);
                cnn.AgregarParametroAComando(cmm, "V_SERV", v_id2);
                MySqlParameter outSal22 = cnn.AgregarParametroAComandoOut(cmm, "V_SAL");
                cmm.ExecuteNonQuery();
                v_sal = outSal22.Value.ToString();


                return v_sal;
            }
            catch
            {
                throw;
            }
            finally
            {
                cnn.Close(cmm);
                cnn2.Close();
            }



        }



        public List<kx_cliente> ListarPacientes(string v_tipo, string v_valor, string v_user)
        {

            List<kx_cliente> lista = new List<kx_cliente>();
            try
            {
                string cmdTxt = " select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Habilitado <>'N' AND Id_Usuario = '" + v_user + "' AND Habilitado IS NULL ";

                if (v_tipo == "0")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Nombre like  '" + v_valor + "%' and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "1")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Id like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "2")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Cod_Manual like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "3")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Tipo_Doc='CUIT'  AND  Doc_No like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "4")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Tipo_Doc='DNI'  AND  Doc_No like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                if (v_tipo == "5")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where  Doc_No like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";
                }
                if (v_tipo == "6")
                {
                    cmdTxt = "select Id,Cod_Manual, Tipo_Doc, Doc_No, Iva, Nombre, Apellido, Observacion, Cta_contable, Calle, Numero, Dpto, Pais, Cp, Provincia, Departamento, Distrito, Contac_Nomb, Contac_Tel, Contac_Cel, Contac_Mail, Emp_Tel, Emp_Cel, Emp_Mail, Emp_Web from kx_cliente where Apellido like  '" + v_valor + "%'  and Id_Usuario = '" + v_user + "'  AND Habilitado IS NULL ";

                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    kx_cliente entidad = new kx_cliente();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Cod_Manual = DalModelo.VerifStringMysql(lector, "Cod_Manual");
                    entidad.Tipo_Doc = DalModelo.VerifStringMysql(lector, "Tipo_Doc");
                    entidad.Doc_No = DalModelo.VerifStringMysql(lector, "Doc_No");
                    entidad.Iva = DalModelo.VerifStringMysql(lector, "Iva");
                    entidad.Nombre = DalModelo.VerifStringMysql(lector, "Nombre");
                    entidad.Apellido = DalModelo.VerifStringMysql(lector, "Apellido");
                    entidad.Observacion = DalModelo.VerifStringMysql(lector, "Observacion");
                    entidad.Cta_contable = DalModelo.VerifStringMysql(lector, "Cta_contable");
                    entidad.Calle = DalModelo.VerifStringMysql(lector, "Calle");
                    entidad.Numero = DalModelo.VerifStringMysql(lector, "Numero");
                    entidad.Dpto = DalModelo.VerifStringMysql(lector, "Dpto");
                    entidad.Pais = DalModelo.VerifStringMysql(lector, "Pais");
                    entidad.Cp = DalModelo.VerifStringMysql(lector, "Cp");
                    entidad.Provincia = DalModelo.VerifStringMysql(lector, "Provincia");
                    entidad.Departamento = DalModelo.VerifStringMysql(lector, "Departamento");
                    entidad.Distrito = DalModelo.VerifStringMysql(lector, "Distrito");
                    entidad.Contac_Nomb = DalModelo.VerifStringMysql(lector, "Contac_Nomb");
                    entidad.Contac_Tel = DalModelo.VerifStringMysql(lector, "Contac_Tel");
                    entidad.Contac_Cel = DalModelo.VerifStringMysql(lector, "Contac_Cel");
                    entidad.Contac_Mail = DalModelo.VerifStringMysql(lector, "Contac_Mail");
                    entidad.Emp_Tel = DalModelo.VerifStringMysql(lector, "Emp_Tel");
                    entidad.Emp_Cel = DalModelo.VerifStringMysql(lector, "Emp_Cel");
                    entidad.Emp_Mail = DalModelo.VerifStringMysql(lector, "Emp_Mail");
                    entidad.Emp_Web = DalModelo.VerifStringMysql(lector, "Emp_Web");
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

        // hosp_hc_files
        public void Alta_hosp_hc_files(hosp_hc_files v_obj)
        {
            try
            {
                string cmdTxt = " insert into hosp_hc_files (Paciente, Descripcion, Aud, Path, tipo) values ('" + v_obj.Paciente + "', '" + v_obj.Descripción + "', now(), '" + v_obj.Path + "', '" + v_obj.tipo + "')  ";

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


        public void Modificacion_hosp_hc_files(hosp_hc_files v_obj)
        {
            try
            {
                string cmdTxt = "update hosp_hc_files set Paciente='" + v_obj.Paciente + "' , Descripcion='" + v_obj.Descripción + "' , Aud='" + v_obj.Aud + "' , Path='" + v_obj.Path + "' , tipo='" + v_obj.tipo + "' where id ='" + v_obj.Id + "'   ";

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
        public List<hosp_hc_files> Listar_hosp_hc_files(string v_tipo, string v_valor)
        {

            List<hosp_hc_files> lista = new List<hosp_hc_files>();
            try
            {



                string cmdTxt = " select Id,Paciente, Descripcion, DATE_FORMAT(Aud, '%d/%m/%y')  Aud, Path, tipo from hosp_hc_files  ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select Id,Paciente, Descripcion, DATE_FORMAT(Aud, '%d/%m/%y')  Aud, Path, tipo from hosp_hc_files  where   Paciente='" + v_valor + "' order by Id desc  ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select Id,Paciente, Descripcion, DATE_FORMAT(Aud, '%d/%m/%y')  Aud, Path, tipo from hosp_hc_files order by  Aud  ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt = " select Id,Paciente, Descripcion,  DATE_FORMAT(Aud, '%d/%m/%y')  Aud, Path, tipo from hosp_hc_files order by  Aud ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_hc_files entidad = new hosp_hc_files();
                    entidad.Id = DalModelo.VeriIntMysql(lector, "Id");
                    entidad.Paciente = DalModelo.VerifStringMysql(lector, "Paciente");
                    entidad.Descripción = DalModelo.VerifStringMysql(lector, "Descripcion");
                    entidad.Aud = DalModelo.VerifStringMysql(lector, "Aud");
                    entidad.Path = DalModelo.VerifStringMysql(lector, "Path");
                    entidad.tipo = DalModelo.VerifStringMysql(lector, "tipo");
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


        public void Eliminar_hosp_hc_files(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from hosp_hc_files  where Id='" + v_id + "'";

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

  
    // examen vestibular

        public void Alta_hosp_examen_vestibular(hosp_examen_vestibular v_obj)
        {
            try
            {
                string cmdTxt = " insert into hosp_examen_vestibular (id, motivo, definicion_sub, romberg, unterberger, test_dix_hall, dedo_nariz, adiadococinesia, aud,Id_pac,Especialista) values ('" + v_obj.id.ToUpper() + "', '" + v_obj.motivo.ToUpper() + "', '" + v_obj.definicion_sub.ToUpper() + "', '" + v_obj.romberg.ToUpper() + "', '" + v_obj.unterberger.ToUpper() + "', '" + v_obj.test_dix_hall.ToUpper() + "', '" + v_obj.dedo_nariz.ToUpper() + "', '" + v_obj.adiadococinesia.ToUpper() + "', Now(), '" + v_obj.Id_pac.ToUpper() + "', '" + v_obj.Especialista.ToUpper() + "')  ";

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


        public void Modificacion_hosp_examen_vestibular(hosp_examen_vestibular v_obj)
        {
            try
            {
                string cmdTxt = "update hosp_examen_vestibular set id='" + v_obj.id.ToUpper() + "' , motivo='" + v_obj.motivo.ToUpper() + "' , definicion_sub='" + v_obj.definicion_sub.ToUpper() + "' , romberg='" + v_obj.romberg.ToUpper() + "' , unterberger='" + v_obj.unterberger.ToUpper() + "' , test_dix_hall='" + v_obj.test_dix_hall.ToUpper() + "' , dedo_nariz='" + v_obj.dedo_nariz.ToUpper() + "' , adiadococinesia='" + v_obj.adiadococinesia.ToUpper() + "' ,  aud = Now() where id ='" + v_obj.id + "'   ";

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
        public List<hosp_examen_vestibular> Listar_hosp_examen_vestibular(string v_tipo, string v_valor , string v_pac)
        {

            List<hosp_examen_vestibular> lista = new List<hosp_examen_vestibular>();
            try
            {



                string cmdTxt = " select id, motivo, definicion_sub, romberg, unterberger, test_dix_hall, dedo_nariz, adiadococinesia ,  DATE_FORMAT(aud, '%d/%m/%y')  aud , Especialista from hosp_examen_vestibular where Id_pac = '" + v_pac + "'  order by id desc ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select id, motivo, definicion_sub, romberg, unterberger, test_dix_hall, dedo_nariz, adiadococinesia ,  DATE_FORMAT(aud, '%d/%m/%y')  aud, Especialista from hosp_examen_vestibular  where   Id='" + v_valor + "' and Id_pac = '" + v_pac + "'   order by id desc";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select  id, motivo, definicion_sub, romberg, unterberger, test_dix_hall, dedo_nariz, adiadococinesia ,  DATE_FORMAT(aud, '%d/%m/%y')  aud, Especialista from hosp_examen_vestibular   where Id_pac = '" + v_pac + "'   order by id desc ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt = " select  id, motivo, definicion_sub, romberg, unterberger, test_dix_hall, dedo_nariz, adiadococinesia ,  DATE_FORMAT(aud, '%d/%m/%y')  aud , Especialista  from hosp_examen_vestibular where Id_pac = '" + v_pac + "'  order by id desc  ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_examen_vestibular entidad = new hosp_examen_vestibular();
              
                    entidad.id = DalModelo.VerifStringMysql(lector, "id");
                    entidad.motivo = DalModelo.VerifStringMysql(lector, "motivo");
                    entidad.definicion_sub = DalModelo.VerifStringMysql(lector, "definicion_sub");
                    entidad.romberg = DalModelo.VerifStringMysql(lector, "romberg");
                    entidad.unterberger = DalModelo.VerifStringMysql(lector, "unterberger");
                    entidad.test_dix_hall = DalModelo.VerifStringMysql(lector, "test_dix_hall");
                    entidad.dedo_nariz = DalModelo.VerifStringMysql(lector, "dedo_nariz");
                    entidad.adiadococinesia = DalModelo.VerifStringMysql(lector, "adiadococinesia");
                    entidad.aud = DalModelo.VerifStringMysql(lector, "aud");
                    entidad.Especialista = DalModelo.VerifStringMysql(lector, "Especialista");
                    
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


        public void Eliminar_hosp_examen_vestibular(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from hosp_examen_vestibular  where Id='" + v_id + "'";

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



        // obras sociales


        public void Alta_hosp_obra_sociales(hosp_obra_sociales v_obj)
        {
            try
            {
                string cmdTxt = " insert into hosp_obra_sociales (Nombre) values ('" + v_obj.Nombre.ToUpper() + "')  ";

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


        public void Modificacion_hosp_obra_sociales(hosp_obra_sociales v_obj)
        {
            try
            {
                string cmdTxt = "update hosp_obra_sociales set Nombre='" + v_obj.Nombre.ToUpper() + "' where id ='" + v_obj.Id + "'   ";

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
        public List<hosp_obra_sociales> Listar_hosp_obra_sociales(string v_tipo, string v_valor)
        {

            List<hosp_obra_sociales> lista = new List<hosp_obra_sociales>();
            try
            {



                string cmdTxt = " select Id,Nombre from hosp_obra_sociales  ";

                if (v_tipo == "99")
                {
                    cmdTxt = " select Id,Nombre from hosp_obra_sociales  where   Id='" + v_valor + "' ";
                }
                if (v_tipo == "0")
                {
                    cmdTxt = " select Id,Nombre from hosp_obra_sociales where Nombre like '" + v_valor + "%' ";
                }
                if (string.IsNullOrEmpty(v_valor))
                {
                    cmdTxt = " select Id,Nombre from hosp_obra_sociales  ";
                }
                cmm = cnn.MySqlCrearNuevoComando(cmdTxt);

                MySqlDataReader lector = cnn.ExecuteReader(cmm);

                while (lector.Read())
                {

                    hosp_obra_sociales entidad = new hosp_obra_sociales();
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


        public void Eliminar_hosp_obra_sociales(Int32 v_id)
        {
            try
            {
                string cmdTxt = "delete from hosp_obra_sociales  where Id='" + v_id + "'";

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



    
    }
}

