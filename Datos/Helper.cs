using CRUDveterinaria.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Datos
{
    internal class Helper
    {
        private static Helper instancia;
        private SqlConnection cnn;

        public Helper()
        {
            cnn = new SqlConnection(Properties.Resources.cnnString);
        }

        public static Helper ObtenerInstancia()
        {
            if(instancia == null)
            instancia = new Helper();
            return instancia;
        }

        public int ObtenerProximo(string sp, string Output)
        {
            SqlCommand cmdProx = new SqlCommand();
            cnn.Open();
            cmdProx.Connection = cnn;
            cmdProx.CommandType = CommandType.StoredProcedure;
            cmdProx.CommandText = sp;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = Output;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmdProx.Parameters.Add(pOut);
            cmdProx.ExecuteNonQuery();
            cnn.Close();
            return (int)pOut.Value;
        }

        public DataTable CargarCombo(string sp)
        {
            DataTable table = new DataTable();
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandType = CommandType.StoredProcedure;
            cmdCombo.CommandText = sp;
            table.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return table;
        }

        public bool ConfirmarMascota(Mascota oMascota)
        {
            bool ok = true;
            SqlTransaction trs = null;
            try
            {
                SqlCommand cmdMaestro = new SqlCommand();
                cnn.Open();
                trs = cnn.BeginTransaction();
                cmdMaestro.Connection = cnn;
                cmdMaestro.Transaction = trs;
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.CommandText = "sp_insertar_mascotas";
                cmdMaestro.Parameters.AddWithValue("@nombre", oMascota.Nombre);
                cmdMaestro.Parameters.AddWithValue("@edad", oMascota.Edad);
                cmdMaestro.Parameters.AddWithValue("@id_tipo", oMascota.Tipo.IdTipo);
                cmdMaestro.Parameters.AddWithValue("@id_cliente", oMascota.Cliente.IdCliente);
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id_mascota";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(pOut);
                cmdMaestro.ExecuteNonQuery();

                int nroMascota = (int)pOut.Value;
                
                foreach(Atencion a in oMascota.Atenciones)
                {
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection = cnn;
                    cmdDetalle.Transaction = trs;
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.CommandText = "sp_insertar_atencion";
                    cmdDetalle.Parameters.AddWithValue("@fecha", a.Fecha);
                    cmdDetalle.Parameters.AddWithValue("@descripcion", a.Descripcion);
                    cmdDetalle.Parameters.AddWithValue("@importe", a.Importe);
                    cmdDetalle.Parameters.AddWithValue("@id_mascota", nroMascota);
                    cmdDetalle.ExecuteNonQuery();
                }
                trs.Commit();
            }
            catch(Exception)
            {
                if (trs != null)
                {
                    trs.Rollback();
                    ok = false;
                }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }
    }
}
