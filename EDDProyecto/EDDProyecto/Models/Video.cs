using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ArbolB;

namespace EDDProyecto.Models
{
   
    public class Video : IComparable<Video>, ArbolB.ITextoTamañoFijo
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int AñoLanzamiento { get; set; }
        public string Genero { get; set; }

        private const string FormatoVideo = "xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xxxx-xxxxxxxxxxxxxxxxxxxx";

        public string Dato { get; set; }

        public Video()
        {
            
        }
        public int FixedSizeText
        {
            get
            {
                return 67;
            }
        }

       

        public string ToFixedSizeString()
        {
            Dato = FormatoVideo;
            return Dato;
        }

        int IComparable<Video>.CompareTo(Video other)
        {
            return this.Nombre.CompareTo(other.Nombre);
        }

        public static List<Video> searchByName(ArbolB.ArbolB<Video> arbol, string dato)
        {
            
        }
    }
}