﻿using System;
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
        ArbolB<Usuario> UsersTree = new ArbolB<Usuario>(3, "UsersTree.txt", new FabricaUsuario());

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session["UsersTree"] = Session["UsersTree"] ?? UsersTree;
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
            bool success = true; 

            //Implementar busqueda de usuario, obtener nodo y comparar contraseña

            if(success)
            {
                return RedirectToAction("RedirectAdmin", "Video");                    
            }
            else
            {
                return RedirectToAction("RedirectUser", "Video");
            }           
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
        public ActionResult InsertarUsuarios(HttpPostedFileBase File)
        {
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
