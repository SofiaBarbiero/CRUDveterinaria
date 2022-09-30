using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDveterinaria.Dominio
{
    class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public int Sexo { get; set; }

        public Cliente(int idCliente, string nombre)
        {
            IdCliente = idCliente;
            Nombre = nombre;
        }
        public Cliente(int idCliente, string nombre,  int sexo)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Sexo = sexo;
        }
        public Cliente()
        {
            Nombre = string.Empty;
            IdCliente = 0;
            Sexo = 1;
        }
        public override string ToString()
        {
            return "Cliente:" + Nombre;
        }
    }
}
