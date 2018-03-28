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
    public class UsuarioController : Controller
    {
      //  ArbolB<Usuario> UsersTree = new ArbolB<Usuario>(); 

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
          //  Session["UsersTree"] = Session["UsersTree"] ?? UsersTree; 
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Log(Usuario user)
        {
            return View();
        }

        [HttpGet]
        public ActionResult LecturaArchivo()
        {
            //aqui se abre una vista para poder subir el archivo
            return View();
        }

        private bool isValidContentType(HttpPostedFileBase contentType)
        {
            return contentType.FileName.EndsWith(".json");
        }

        [HttpPost]
        public ActionResult LecturaArchivo(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("Index");
            }
            else
            {
                if (!isValidContentType(File))
                {
                    ViewBag.Error = "Solo archivos Json son válidos para la entrada";
                    return View("Index");
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

                        //Realizar if donde dependiendo el booleano es la lista que se va a seleccionar    
                        string info = reader.ReadToEnd();
                        List<string> lista = JsonConvert.DeserializeObject<List<string>>(info);
                        for (int i = 0; i < lista.Count; i++)
                        {
                            UsersTree.Insert(lista.ElementAt(i).ToString());
                        }
                        Session["UsersTree"] = UsersTree;
                    }
                }
            }

        }


    }
}