using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text;
using ArbolB;

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

        ArbolB<Video> watchlist;

        public string Password { get; set; }


        private const string FormatoConst = "xxxxxxxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx-00-xxxxxxxxxxxxxxx-xxxxxxxxxxxxxxxxxxxx";

        public Usuario()
        {
            Nombre = "";
            Apellido = "";
            Edad = 0;
            Username = "";
            Password = "";
        }

        public Usuario(Usuario usuario)
        {
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Edad = usuario.Edad;
            Username = usuario.Username;
            Password = usuario.Password;

            watchlist = new ArbolB<Video>(3, Username + ".watchlist", new FabricaVideo());
            watchlist.Cerrar();
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
            StringBuilder sb = new StringBuilder();
            sb.Append(Nombre.PadLeft(20, 'x'));
            sb.Append('-');
            sb.Append(Apellido.PadLeft(20, 'x'));
            sb.Append('-');
            sb.Append(Edad.ToString().PadLeft(2, '0'));
            sb.Append('-');
            sb.Append(Username.PadLeft(15, 'x'));
            sb.Append('-');
            sb.Append(Password.PadLeft(20, 'x'));

            return sb.ToString();
        }
    }
}