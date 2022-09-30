using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Dominio
{
    class Atencion
    {
        public string Descripcion { get; set; }
        public double Importe { get; set; }
        public DateTime Fecha { get; set; }



        public Atencion(string descripcion, double importe, DateTime fecha)
        {
            Descripcion = descripcion;
            Fecha = fecha;
            Importe = importe;
        }
        public Atencion()
        {
            Descripcion = string.Empty;
            Importe = 0;
            Fecha = DateTime.Now;
        }
        public override string ToString()
        {
            return  "Costo:" + Importe + " $ ";
        }
    }
}
