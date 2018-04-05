using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text;

namespace EDDProyecto.Models
{
    public class Usuario : IComparable<Usuario>, ArbolB.ITextoTamañoFijo
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

        public string Dato { get; set; }

        private const string FormatoConst = "xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-xx-xxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx";

        public Usuario()
        {
            
        }
        public int FixedSizeText
        {
            get
            {
                return 81;
            }
        }

        

        public int CompareTo(Usuario obj)
        {
            return this.Username.CompareTo(obj.Username);
        }

        public override string ToString()
        {
            return ToFixedSizeString();
        }

        public string ToFixedSizeString()
        {
            Dato = FormatoConst;
            return Dato;
        }
    }
}