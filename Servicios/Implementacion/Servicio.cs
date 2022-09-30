using CRUDveterinaria.Datos.Implementacion;
using CRUDveterinaria.Datos.Interfaz;
using CRUDveterinaria.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Servicios.Interfaz
{
    class Servicio : IServicio
    {
        private IDaoMascota dao;

        public Servicio()
        {
            dao = new DaoMascota();
        }

        public List<Cliente> ObtenerClientes()
        {
            return dao.ObtenerClientes();
        }

        public int ObtenerProximo()
        {
            return dao.ObtenerProximo();
        }

        public List<TipoMascota> ObtenerTipos()
        {
            return dao.ObtenerTipos();
        }
    }
}
