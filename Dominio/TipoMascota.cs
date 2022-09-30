using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Dominio
{
    class TipoMascota
    {
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public TipoMascota(int id, string nombre)
        {
            this.IdTipo = id;
            this.NombreTipo = nombre;
        }

        public override string ToString()
        {
            return NombreTipo; 
        }
    }
}
