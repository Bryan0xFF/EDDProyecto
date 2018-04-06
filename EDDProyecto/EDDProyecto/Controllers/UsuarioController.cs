using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDDProyecto.Models;
using System.IO;
using Newtonsoft.Json;
using ArbolB;
using EDDProyecto.Controllers; 


namespace EDDProyecto.Controllers
{
    public class UsuarioController : Controller
    {
         
        Usuario Admin = new Usuario(); 
        ArbolB<Usuario> UsersTree = new ArbolB<Usuario>(3, @"UsersTree.txt", new FabricaUsuario());


        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
         //   Session["UsersTree"] = Session["UsersTree"] ?? UsersTree; 
            Session["admin"] = Session["admin"] ?? Admin;
            Admin.Username = "admin";
            Admin.Password = "admin";
            Session["admin"] = Admin; 

            return View();
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario user)
        {
            int llave = GetASCII(user.Username);
            UsersTree.Agregar(llave, user);
            return View("CreateUserSuccess"); 
        }

        private int GetASCII(string username)
        {
            char[] datos = username.ToCharArray();
            int result = 0;

            for (int i = 0; i < datos.Length; i++)
            {
                result += (int)char.GetNumericValue(datos[i]);
            }

            return result;

        }

        [HttpPost]
        public ActionResult Log(Usuario user)
        {           
            Admin =(Usuario)Session["admin"];            

            if(user.CompareTo(Admin) == 0)
            {
                return RedirectToAction("RedirectAdmin", "Video");
            }
            //else if (Search == true)
            //{
               
            //}
            else
            {
                return View("LogError");
            }         
        }

        [HttpGet]
        public ActionResult LecturaArchivoU()
        {
            //aqui se abre la vista para poder subir el archivo
            return View("LectorUsuarios");
        }

        private bool isValidContentType(HttpPostedFileBase contentType)
        {
            return contentType.FileName.EndsWith(".json");
        }

        [HttpPost]
        public ActionResult InsertarUsuarios(HttpPostedFileBase File)
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
                        // UsersTree = (ArbolB<Usuario>)Session["ABBCadena"];                       

                        string info = reader.ReadToEnd();
                        List<Usuario> lista = JsonConvert.DeserializeObject<List<Usuario>>(info);
                        for (int i = 0; i < lista.Count; i++)
                        {
                            int llave = GetASCII(lista.ElementAt(i).Username);
                            UsersTree.Agregar(llave, lista.ElementAt(i));
                        }
                      
                    }
                }
            }
            return View(); 
        }

        [HttpGet]
        public ActionResult Eliminar()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Delete(Video deletedVideo)
        {
            return View(); 
        }



    }
}
