using System;
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
    enum TipoDeBusqueda
    {
        PorNombre = 1,
        PorFecha = 2,
        porGenero = 3
    };

    public class VideoController : Controller
    {
        public delegate List<Video> SearchByName(ArbolB<Video> arbol, string dato);
        public delegate List<Video> SearchByDate(ArbolB<Video> arbol, string dato);
        public delegate List<Video> SearchByGender(ArbolB<Video> arbol, string dato);

        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingresar()
        {
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
            //Ejecutar operacion de ingresa al arbol
            return View("CreateVideoSuccess");
        }

        [HttpGet]
        public ActionResult LecturaArchivo()
        {
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
                        //  UsersTree = (ArbolB<Usuario>)Session["ABBCadena"];                       

                        string info = reader.ReadToEnd();
                        List<Video> lista = JsonConvert.DeserializeObject<List<Video>>(info);
                        //for (int i = 0; i < lista.Count; i++)
                        //{
                        //    UsersTree.Insert(lista.ElementAt(i).ToString());
                        //}
                        //Session["UsersTree"] = UsersTree;
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Insertar()
        {
            return RedirectToAction("Registrar", "Usuario");
        }

        [HttpGet]
        public ActionResult InsertarCarga()
        {
            return RedirectToAction("LecturaArchivoU", "Usuario"); 
        }

        [HttpPost]
        public ActionResult InsertarManual(Usuario newuser)
        {
            return View("CreateUserSuccess");
        }

        [HttpGet]
        public ActionResult RedirectAdmin()
        {
            return View("CatalogoAdmin"); 
        }

        [HttpGet]
        public ActionResult RedirectUser()
        {
            return View("Catalogo"); 
        }

        public ActionResult Buscar()
        {
            //view not implemented
            return View();
        }

    }
}