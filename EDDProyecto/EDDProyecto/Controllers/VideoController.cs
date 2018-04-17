﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDDProyecto.Models;
using System.IO;
using Newtonsoft.Json;
using ArbolB;

namespace EDDProyecto.Controllers
{
    enum TipoPelicula
    {
        Show = 1,
        Movie = 2,
        Documentary = 3
    };

    enum TipoInsercion
    {
        Name = 1,
        Year = 2,
        Gender = 3,
        Type = 4
    };

    enum TipoDeBusqueda
    {
        PorNombre = 1,
        PorFecha = 2,
        porGenero = 3
    };

    public class VideoController : Controller
    {
        int contador = 0; 

        public delegate List<Video> SearchByName(ArbolB<Video> arbol, string dato);
        public delegate List<Video> SearchByDate(ArbolB<Video> arbol, string dato);
        public delegate List<Video> SearchByGender(ArbolB<Video> arbol, string dato);

        ArbolB<Video> NameShowTree = new ArbolB<Video>(3, @"NameShowTree.txt", new FabricaVideo());
        ArbolB<Video> YearShowTree = new ArbolB<Video>(3, @"YearShowTree.txt", new FabricaVideo());
        ArbolB<Video> GenderShowTree = new ArbolB<Video>(3, @"GenderShowTree.txt", new FabricaVideo());

        ArbolB<Video> NameMovieTree = new ArbolB<Video>(3, @"NameMovieTree.txt", new FabricaVideo());
        ArbolB<Video> YearMovieTree = new ArbolB<Video>(3, @"YearMovieTree.txt", new FabricaVideo());
        ArbolB<Video> GenderMovieTree = new ArbolB<Video>(3, @"GenderMovieTree.txt", new FabricaVideo());

        ArbolB<Video> NameDocumentaryTree = new ArbolB<Video>(3, @"NameDocumentaryTree.txt", new FabricaVideo());
        ArbolB<Video> YearDocumentaryTree = new ArbolB<Video>(3, @"YearDocumentaryTree.txt", new FabricaVideo());
        ArbolB<Video> GenderDocumentaryTree = new ArbolB<Video>(3, @"GenderDocumentaryTree.txt", new FabricaVideo());


        // GET: Video
        public ActionResult Index()
        {
            Session["contador"] = Session["contador"] ?? contador; 
            CerrarTodo();
            return View();
        }

        public ActionResult Ingresar()
        {
            CerrarTodo();
            return View();
            //bool success = true;

            //if (success)
            //{
            //    return View("CatalogoAdmin");
            //}
            //else
            //{
            //    return View("Catalogo");
            //}
        }

        public ActionResult Ingreso(Video video)
        {
            CerrarTodo();
            //Ejecutar operacion de ingresa al arbol
            return View("CreateVideoSuccess");
        }

        [HttpGet]
        public ActionResult LecturaArchivo()
        {
            CerrarTodo();
            //aqui se abre una vista para poder subir el archivo
            return View("Lector");
        }

        private bool isValidContentType(HttpPostedFileBase contentType)
        {
            return contentType.FileName.EndsWith(".json");
        }

        [HttpPost]
        public ActionResult InsertarArchivo(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("CatalogoAdmin");
            }
            else
            {
                if (!isValidContentType(File))
                {
                    ViewBag.Error = "Solo archivos Json son válidos para la entrada";
                    return View("CatalogoAdmin");
                }

                if (File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/UserJsonFiles/" + fileName));
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    File.SaveAs(path);
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string info = reader.ReadToEnd();
                        List<Video> lista = JsonConvert.DeserializeObject<List<Video>>(info);
                        try
                        {
                            for (int i = 0; i < lista.Count; i++)
                            {
                                if (lista.ElementAt(i).Tipo == "Show")
                                {
                                    NameShowTree.Agregar(lista.ElementAt(i).Nombre, lista.ElementAt(i), Utilidades.countShowName);
                                    YearShowTree.Agregar(lista.ElementAt(i).AñoLanzamiento.ToString(), lista.ElementAt(i),Utilidades.countShowYear);
                                    GenderShowTree.Agregar(lista.ElementAt(i).Genero, lista.ElementAt(i),Utilidades.countShowGenre);                                 
                                }

                                if (lista.ElementAt(i).Tipo == "Pel�cula")
                                {
                                    NameMovieTree.Agregar(lista.ElementAt(i).Nombre, lista.ElementAt(i), Utilidades.countMovieName);
                                    YearMovieTree.Agregar(lista.ElementAt(i).AñoLanzamiento.ToString(), lista.ElementAt(i), Utilidades.countMovieYear);
                                    GenderMovieTree.Agregar(lista.ElementAt(i).Genero, lista.ElementAt(i), Utilidades.countMovieGenre);
                                }

                                if (lista.ElementAt(i).Tipo == "Documental")
                                {
                                    NameDocumentaryTree.Agregar(lista.ElementAt(i).Nombre, lista.ElementAt(i), Utilidades.countDocumentaryName);
                                    YearDocumentaryTree.Agregar(lista.ElementAt(i).AñoLanzamiento.ToString(), lista.ElementAt(i),Utilidades.countDocumentaryYear);
                                    GenderDocumentaryTree.Agregar(lista.ElementAt(i).Genero, lista.ElementAt(i),Utilidades.countDocumentaryGenre);
                                }
                            }
                        }
                        catch (Exception e)
                        {    
                            
                        }
                    }
                }
            }
            CerrarTodo();
            return View();
        }

        [HttpGet]
        public ActionResult Insertar()
        {
            CerrarTodo();
            return RedirectToAction("Registrar", "Usuario");
        }

        [HttpGet]
        public ActionResult InsertarCarga()
        {
            CerrarTodo();
            return RedirectToAction("LecturaArchivoU", "Usuario"); 
        }

        [HttpPost]
        public ActionResult InsertarManual(Usuario newuser)
        {
            CerrarTodo();
            return View("CreateUserSuccess");
        }

        [HttpGet]
        public ActionResult RedirectAdmin()
        {
            CerrarTodo();
            return View("CatalogoAdmin"); 
        }

        [HttpGet]
        public ActionResult RedirectUser()
        {
            CerrarTodo();
            return View("Catalogo"); 
        }

        public ActionResult Buscar()
        {
            CerrarTodo();
            //view not implemented
            return View();
        }

        public void CerrarTodo()
        {
            NameShowTree.Cerrar();
            YearShowTree.Cerrar();
            GenderShowTree.Cerrar();

            NameMovieTree.Cerrar();
            YearMovieTree.Cerrar();
            GenderMovieTree.Cerrar();

            NameDocumentaryTree.Cerrar();
            YearDocumentaryTree.Cerrar();
            GenderDocumentaryTree.Cerrar();
        }

    }
}