using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        }
        public int FixedSizeText
        {
            get
            {
                return 77;
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

            return "";

        }
    }
}