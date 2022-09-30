using CRUDveterinaria.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Datos.Interfaz
{
    interface IDaoMascota
    {
        int ObtenerProximo();

        List<TipoMascota> ObtenerTipos();
        List<Cliente> ObtenerClientes();
    }
}
