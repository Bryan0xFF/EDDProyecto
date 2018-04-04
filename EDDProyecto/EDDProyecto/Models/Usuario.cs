using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text;

namespace EDDProyecto.Models
{
    public class Usuario : IComparable, ArbolB.ITextoTamañoFijo
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public Usuario()
        {
            //TODO: logic here
        }
        public int FixedSizeText
        {
            get
            {
                return 83;
            }
        }

        private const string FormatoConst = "xxxxxxxxxxxxxxxxxxxx";

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

                for (int i = 0; i < 2; i++)
                {
                    UsuarioFormatoFijo += "x";
                }

                UsuarioFormatoFijo += "-";

                for (int i = 0; i < 15; i++)
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

        

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return ToFixedSizeString();
        }

        public string ToFixedSizeString()
        {
            string[] datos = UsuarioFormatoFijo.Split('-');
            datos[0] = Nombre.PadLeft(20, 'x');
            datos[1] = Apellido.PadLeft(20, 'x');
            datos[2] = Edad.ToString().PadLeft(2, '0');
            datos[3] = Username.ToString().PadLeft(15, 'x');
            datos[4] = Password.ToString().PadLeft(20, 'x');

            StringBuilder sb = new StringBuilder(); 
            
            for (int i = 0; i < datos.Length; i++)
            {
                sb.Append(datos[i] + "-");
            }

            sb.Append("\r\n");

            return sb.ToString();
        }
    }
}