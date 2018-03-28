using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDDProyecto.Models
{
    public class Usuario : IComparable
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}