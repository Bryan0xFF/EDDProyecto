using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDDProyecto.Models
{
    public class FabricaVideo : ArbolB.IFabricaTextoTamañoFijo<Video>
    {
        public Video Fabricar(string textoTamañoFijo)
        {
            Video video = new Video();

            var datos = textoTamañoFijo.Split('-');
            video.Tipo = datos[0].Trim();
            video.Nombre = datos[1].Trim();
            video.AñoLanzamiento = datos[2].Trim();
            video.Genero = datos[3].Trim();

            return video;

        }

        public Video FabricarNulo()
        {
            return new Video();
        }
    }
}