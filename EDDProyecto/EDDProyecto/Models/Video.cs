using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ArbolB;
using Newtonsoft.Json; 

namespace EDDProyecto.Models
{
   
    public class Video : IComparable<Video>, ArbolB.ITextoTamañoFijo
    {
        [JsonProperty ("type")]
        public string Tipo { get; set; }
        [JsonProperty ("name")]
        public string Nombre { get; set; }
        [JsonProperty ("year")]
        public string AñoLanzamiento { get; set; }
        [JsonProperty ("genre")]
        public string Genero { get; set; }

        private const string FormatoVideo = "xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxxxxxxx-xxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx";


        public Video()
        {
            this.Tipo = "";
            this.Nombre = "";
            this.AñoLanzamiento = "";
            this.Genero = "";
        }

        public Video(Video video)
        {
            this.Tipo = video.Tipo;
            this.Nombre = video.Nombre;
            this.AñoLanzamiento = video.AñoLanzamiento;
            this.Genero = video.Genero;
        }
        public int FixedSizeText
        {
            get
            {
                return 78;
            }
        }

       

        public string ToFixedSizeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Tipo.PadLeft(20, 'x'));
            sb.Append('-');
            sb.Append(Nombre.PadLeft(25, 'x'));
            sb.Append('-');
            sb.Append(AñoLanzamiento.ToString().PadLeft(10, 'x'));
            sb.Append('-');
            sb.Append(Genero.PadLeft(20, 'x'));
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToFixedSizeString();
        }

        int IComparable<Video>.CompareTo(Video other)
        {
            return this.Nombre.CompareTo(other.Nombre);
        }

        public static List<Video> searchByName(ArbolB.ArbolB<Video> arbol, string dato)
        {
            return null;
        }
    }
}