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

        Usuario Admin = new Usuario();
        ArbolB<Usuario> UsersTree = new ArbolB<Usuario>(3, @"UsersTree.txt", new FabricaUsuario());


        // GET: Usuario
        public ActionResult MenuPrincipalAdmin()
        {
            UsersTree.Cerrar();
            return RedirectToAction("RedirectAdmin", "Video");
        }

        public ActionResult Login()
        {
            Session["admin"] = Session["admin"] ?? Admin;
            Admin.Username = "admin";
            Admin.Password = "admin";
            Session["admin"] = Admin;
            UsersTree.Cerrar();
            return View();
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            UsersTree.Cerrar();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario user)
        {
            int n = 0;
            Usuario nuevoUsuario = new Usuario(user);
            UsersTree.Agregar(nuevoUsuario.Username.Trim('x'), nuevoUsuario, "");
            UsersTree.Cerrar();
            return View("CreateUserSuccess");
        }

        private double GetASCII(string username)
        {
            char[] datos = username.ToCharArray();
            double result = 0;

            for (int i = 0; i < datos.Length; i++)
            {
                char value = datos[i];
                result += value;
            }

            return result;
        }

        [HttpPost]
        public ActionResult Log(Usuario user)
        {            
            try
            {
                Admin = (Usuario)Session["admin"];
                ViewData.Add(user.Nombre.ToString(), user);

                if (user.CompareTo(Admin) == 0)
                {
                    UsersTree.Cerrar();
                    return RedirectToAction("RedirectAdmin", "Video");
                }

                string llave = Utilidades.FormatearLlave(user.Username);
                Usuario login = UsersTree.Obtener(llave);
                string trimedUser = login.Username.Trim('x');
                login.watchlist = new ArbolB<Video>(3, trimedUser + ".watchlist", new FabricaVideo());
                login.watchlist.Cerrar();
                if (login.Password.Trim('x') == user.Password)
                {
                    UsersTree.Cerrar();
                    return RedirectToAction("RedirectUser", "Video",login);
                }
                else
                {
                    UsersTree.Cerrar();
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                UsersTree.Cerrar();
                //maneja una vista de error indicando que el usuario no existe o bien la contraseña
                return View("LogError");
            }
            
        }

        public ActionResult Logout()
        {
            UsersTree.Cerrar();
            return View("Login");
        }

        [HttpGet]
        public ActionResult LecturaArchivoU()
        {
            //aqui se abre la vista para poder subir el archivo
            UsersTree.Cerrar();

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
                return View("LectorUsuarios");
            }
            else
            {
                if (!isValidContentType(File))
                {
                    ViewBag.Error = "Solo archivos Json son válidos para la entrada";
                    return View("LectorUsuarios");
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
                        List<Usuario> lista = JsonConvert.DeserializeObject<List<Usuario>>(info);

                        for (int i = 0; i < lista.Count; i++)
                        {
                            UsersTree.Agregar(lista.ElementAt(i).Username, new Usuario(lista.ElementAt(i)), "");
                        }
                    }
                }
            }
            UsersTree.Cerrar();
            return View("CreateUserSuccess");
        }

        [HttpGet]
        public ActionResult InsertarManual()
        {
            UsersTree.Cerrar();
            return View();
        }       

        [HttpPost]
        public ActionResult Delete(Video deletedVideo)
        {
            UsersTree.Cerrar();
            return View();
        }
    }
}