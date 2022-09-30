using CRUDveterinaria.Datos.Interfaz;
using CRUDveterinaria.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Datos.Implementacion
{
    class DaoMascota : IDaoMascota
    {
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> lst = new List<Cliente>();
            string sp = "sp_cargar_clientes";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp);
            foreach(DataRow dr in table.Rows)
            {
                int id = int.Parse(dr["id_cliente"].ToString());
                string nombre = dr["nombre"].ToString();
                Cliente aux = new Cliente(id, nombre);
                lst.Add(aux);
            }
            return lst;
        }

        public int ObtenerProximo()
        {
            string sp = "sp_proximo";
            string output = "@next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp, output);
        }

        public List<TipoMascota> ObtenerTipos()
        {
            List<TipoMascota> lst = new List<TipoMascota>();
            string sp = "sp_cargar_tipos";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp);
            foreach (DataRow dr in table.Rows)
            {
                int id = int.Parse(dr["id_tipo"].ToString());
                string tipo = dr["descripcion"].ToString();
                TipoMascota aux = new TipoMascota(id, tipo);
                lst.Add(aux);
            }
            return lst;
        }
    }
}
