using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDDProyecto.Models
{
    public class FabricaUsuario : ArbolB.IFabricaTextoTamañoFijo<Usuario>
    {
        public Usuario Fabricar(string textoTamañoFijo)
        {
            Usuario usuario = new Usuario();
            var datos = textoTamañoFijo.Split('-');
            usuario.Nombre = datos[0].Trim();
            usuario.Apellido = datos[1].Trim();
            usuario.Edad = Convert.ToInt32(datos[2].Trim());
            usuario.Username = datos[3].Trim();
            usuario.Password = datos[4].Trim();

            return usuario;
        }

        public Usuario FabricarNulo()
        {
            return new Usuario();
        }
    }
}