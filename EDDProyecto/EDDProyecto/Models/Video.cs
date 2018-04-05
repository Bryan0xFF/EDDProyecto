using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EDDProyecto.Models
{
    public class Video : IComparable<Video>, ArbolB.ITextoTamañoFijo
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int AñoLanzamiento { get; set; }
        public string Genero { get; set; }


        public Video()
        {
            
        }
        public int FixedSizeText
        {
            get
            {
                return 69;
            }
        }

        private string UsuarioFormatoFijo
        {
            get
            {
                for (int i = 0; i < 20; i++)
                {
                    UsuarioFormatoFijo += "x";
                }

                UsuarioFormatoFijo += "-";

                for (int i = 0; i < 20; i++)
                {
                    UsuarioFormatoFijo += "x";
                }

                UsuarioFormatoFijo += "-";

                for (int i = 0; i < 4; i++)
                {
                    UsuarioFormatoFijo += "x";
                }

                UsuarioFormatoFijo += "-";

                for (int i = 0; i < 20; i++)
                {
                    UsuarioFormatoFijo += "x";
                }

                return UsuarioFormatoFijo;
            }
            set
            {
                UsuarioFormatoFijo = value;
            }
        }

        public string ToFixedSizeString()
        {
            string[] datos = UsuarioFormatoFijo.Split('-');
            datos[0] = Tipo.PadLeft(20, ' ');
            datos[1] = Nombre.PadLeft(20, ' ');
            datos[2] = AñoLanzamiento.ToString().PadLeft(4, ' ');
            datos[3] = Genero.PadLeft(20, ' ');
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < datos.Length; i++)
            {
                sb.Append(datos[i] + "-");
            }

            sb.Append("\r\n");

            return sb.ToString();
        }

        int IComparable<Video>.CompareTo(Video other)
        {
            return this.Nombre.CompareTo(other.Nombre);
        }
    }
}