using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDDProyecto.Models;

namespace EDDProyecto.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ingresar()
        {
            return View("Ingresar"); 
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
            return View();
        }

        private bool isValidContentType(HttpPostedFileBase contentType)
        {
            return contentType.FileName.EndsWith(".json");
        }

        //[HttpPost]
        //public ActionResult Carga(HttpPostedFileBase File)
        //{
        //    if (File == null || File.ContentLength == 0)
        //    {
        //        ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
        //        return View("Index");
        //    }
        //    else
        //    {
        //        if (!isValidContentType(File))
        //        {
        //            ViewBag.Error = "Solo archivos Json son válidos para la entrada";
        //            return View("Index");
        //        }

        //        if (File.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(File.FileName);
        //            var path = Path.Combine(Server.MapPath("~/Content/UserJsonFiles/" + fileName));
        //            if (System.IO.File.Exists(path))
        //                System.IO.File.Delete(path);
        //            File.SaveAs(path);
        //            using (StreamReader reader = new StreamReader(path))
        //            {
        //                //  UsersTree = (ArbolB<Usuario>)Session["ABBCadena"];                       

        //                //string info = reader.ReadToEnd();
        //                //List<string> lista = JsonConvert.DeserializeObject<List<string>>(info);
        //                //for (int i = 0; i < lista.Count; i++)
        //                //{
        //                //    UsersTree.Insert(lista.ElementAt(i).ToString());
        //                //}
        //                //Session["UsersTree"] = UsersTree;
        //            }
        //        }
        //    }
        //}

    }
}