using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDDProyecto.Models;
using System.IO;
using Newtonsoft.Json;
using ArbolB;
using PagedList;

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

        public delegate Video SearchByName(ArbolB<Video> arbol, string dato);
        public delegate Video SearchByDate(ArbolB<Video> arbol, string dato);
        public delegate Video SearchByGender(ArbolB<Video> arbol, string dato);

        ArbolB<Video> NameShowTree = new ArbolB<Video>(3, @"NameShowTree.txt", new FabricaVideo());
        ArbolB<Video> YearShowTree = new ArbolB<Video>(3, @"YearShowTree.txt", new FabricaVideo());
        ArbolB<Video> GenderShowTree = new ArbolB<Video>(3, @"GenderShowTree.txt", new FabricaVideo());

        ArbolB<Video> NameMovieTree = new ArbolB<Video>(3, @"NameMovieTree.txt", new FabricaVideo());
        ArbolB<Video> YearMovieTree = new ArbolB<Video>(3, @"YearMovieTree.txt", new FabricaVideo());
        ArbolB<Video> GenderMovieTree = new ArbolB<Video>(3, @"GenderMovieTree.txt", new FabricaVideo());

        ArbolB<Video> NameDocumentaryTree = new ArbolB<Video>(3, @"NameDocumentaryTree.txt", new FabricaVideo());
        ArbolB<Video> YearDocumentaryTree = new ArbolB<Video>(3, @"YearDocumentaryTree.txt", new FabricaVideo());
        ArbolB<Video> GenderDocumentaryTree = new ArbolB<Video>(3, @"GenderDocumentaryTree.txt", new FabricaVideo());

        Usuario loggedUser;
        ArbolB<Usuario> logged; 

        // GET: Video
        public ActionResult Index()
        {
            Session["contador"] = Session["contador"] ?? contador;
            CerrarTodo();
            return View();
        }

        public ActionResult Out()
        {
            CerrarTodo();
            return RedirectToAction("Logout", "Usuario");
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

        //Insercion manual de videos 
        public ActionResult Ingreso(Video video)
        {

            if (video.Tipo == "Show")
            {
                NameShowTree.Agregar(video.Nombre, video, "");
                YearShowTree.Agregar(video.AñoLanzamiento.ToString(), video, video.Nombre);
                GenderShowTree.Agregar(video.Genero, video, video.Nombre);
            }

            if (video.Tipo == "Pel�cula")
            {
                NameMovieTree.Agregar(video.Nombre, video, "");
                YearMovieTree.Agregar(video.AñoLanzamiento.ToString(), video, video.Nombre);
                GenderMovieTree.Agregar(video.Genero, video, video.Nombre);
            }

            if (video.Tipo == "Documental")
            {
                NameDocumentaryTree.Agregar(video.Nombre, video, "");
                YearDocumentaryTree.Agregar(video.AñoLanzamiento.ToString(), video, video.Nombre);
                GenderDocumentaryTree.Agregar(video.Genero, video, video.Nombre);
            }

            CerrarTodo();
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

        //Insercion por carga de archivos para videos
        [HttpPost]
        public ActionResult InsertarArchivo(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                ViewBag.Error = "El archivo seleccionado está vacío o no hay archivo seleccionado";
                return View("Lector");
            }
            else
            {
                if (!isValidContentType(File))
                {
                    ViewBag.Error = "Solo archivos Json son válidos para la entrada";
                    return View("Lector");
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
                                    NameShowTree.Agregar(lista.ElementAt(i).Nombre, lista.ElementAt(i), "");
                                    YearShowTree.Agregar(lista.ElementAt(i).AñoLanzamiento.ToString(), lista.ElementAt(i), lista.ElementAt(i).Nombre.ToString());
                                    GenderShowTree.Agregar(lista.ElementAt(i).Genero, lista.ElementAt(i), lista.ElementAt(i).Nombre.ToString());
                                }

                                if (lista.ElementAt(i).Tipo == "Pel�cula")
                                {
                                    NameMovieTree.Agregar(lista.ElementAt(i).Nombre, lista.ElementAt(i), "");
                                    YearMovieTree.Agregar(lista.ElementAt(i).AñoLanzamiento.ToString(), lista.ElementAt(i), lista.ElementAt(i).Nombre.ToString());
                                    GenderMovieTree.Agregar(lista.ElementAt(i).Genero, lista.ElementAt(i), lista.ElementAt(i).Nombre.ToString());
                                }

                                if (lista.ElementAt(i).Tipo == "Documental")
                                {
                                    NameDocumentaryTree.Agregar(lista.ElementAt(i).Nombre, lista.ElementAt(i), "");
                                    YearDocumentaryTree.Agregar(lista.ElementAt(i).AñoLanzamiento.ToString(), lista.ElementAt(i), lista.ElementAt(i).Nombre.ToString());
                                    GenderDocumentaryTree.Agregar(lista.ElementAt(i).Genero, lista.ElementAt(i), lista.ElementAt(i).Nombre.ToString());
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
            return View("CreateVideoSuccess");
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

        [HttpGet]
        public ActionResult RedirectAdmin()
        {
            CerrarTodo();
            return View("CatalogoAdmin");
        }

        public ActionResult RedirectUser(Usuario usuario)
        {
            CerrarTodo();
            ViewData.Add(usuario.Nombre.ToString(), usuario);
            Session["Usuario"] = Session["Usuario"] ?? usuario;
            usuario = (Usuario)Session["Usuario"];        
            return View("Catalogo");
        }

        public ActionResult Buscar(Video video)
        {
            CerrarTodo();
            return View();
        }

        [HttpGet]
        public ActionResult AdminBuscar(FormCollection collection)
        {
            CerrarTodo();
            return View();
        }

        [HttpPost]
        public ActionResult AdminBuscarPost(FormCollection collection)
        {
            int type = Convert.ToInt32(collection["comboBoxTipo"]);
            int search = Convert.ToInt32(collection["comboBoxBusqueda"]);
            string llave = collection["llave"] + collection["llaveNombre"];
            Video video = new Video();

            try
            {
                if ("1" == type.ToString())
                {
                    if ("1" == search.ToString())
                    {
                        SearchByName searchByName = new SearchByName(Video.SearchByName);
                        video = searchByName(NameShowTree, llave);
                    }

                    if ("2" == search.ToString())
                    {
                        SearchByDate searchByDate = new SearchByDate(Video.SearchByYear);
                        video = searchByDate(YearShowTree, llave);
                    }
                    if ("3" == search.ToString())
                    {
                        SearchByGender searchByGender = new SearchByGender(Video.SearchByGenre);
                        video = searchByGender(GenderShowTree, llave);
                    }

                }
                if ("2" == type.ToString())
                {
                    if ("1" == search.ToString())
                    {
                        SearchByName searchByName = new SearchByName(Video.SearchByName);
                        video = searchByName(NameMovieTree, llave);
                    }

                    if ("2" == search.ToString())
                    {
                        SearchByDate searchByDate = new SearchByDate(Video.SearchByYear);
                        video = searchByDate(YearMovieTree, llave);
                    }
                    if ("3" == search.ToString())
                    {
                        SearchByGender searchByGender = new SearchByGender(Video.SearchByGenre);
                        video = searchByGender(GenderMovieTree, llave);
                    }

                }
                if ("3" == type.ToString())
                {
                    if ("1" == search.ToString())
                    {
                        SearchByName searchByName = new SearchByName(Video.SearchByName);
                        video = searchByName(NameDocumentaryTree, llave);
                    }

                    if ("2" == search.ToString())
                    {
                        SearchByDate searchByDate = new SearchByDate(Video.SearchByYear);
                        video = searchByDate(YearDocumentaryTree, llave);
                    }
                    if ("3" == search.ToString())
                    {
                        SearchByGender searchByGender = new SearchByGender(Video.SearchByGenre);
                        video = searchByGender(GenderDocumentaryTree, llave);
                    }

                }
            }
            catch (Exception e)
            {
                return View("VideoNotFound");
            }
            CerrarTodo();
            return View(video);
        }

        [HttpGet]
        public ActionResult MenuCatalogo()
        {
            CerrarTodo();
            return View(); 
        }

        public ActionResult CatalogoPaged(FormCollection collection, int page = 1, int pageSize = 20)
        {
            int type = Convert.ToInt32(collection["comboBoxTipo"]);
            List<string> tempList = new List<string>();
            List<Video> DecodedVideos = new List<Video>();
            
            ViewData.Add(type.ToString(), type);           

            if ("1" == type.ToString()) //Show
            {
                tempList = NameShowTree.RecorrerPreOrden();
                for (int i = 0; i < tempList.Count; i++)
                {
                    var video = tempList.ElementAt(i).Split('-');
                    Video newVideo = new Video
                    {
                        Tipo = video[0].Trim('x'),
                        Nombre = video[1].Trim('x'),
                        AñoLanzamiento = video[2].Trim('x'),
                        Genero = video[3].Trim('x')
                    };
                    DecodedVideos.Add(newVideo);                 
                }
                CerrarTodo();
            }

            if ("2" == type.ToString()) //Movie
            {
                tempList = NameMovieTree.RecorrerPreOrden();
                for (int i = 0; i < tempList.Count; i++)
                {
                    var video = tempList.ElementAt(i).Split('-');
                    Video newVideo = new Video
                    {
                        Tipo = video[0].Trim('x'),
                        Nombre = video[1].Trim('x'),
                        AñoLanzamiento = video[2].Trim('x'),
                        Genero = video[3].Trim('x')
                    };
                    DecodedVideos.Add(newVideo);
                }
                CerrarTodo();
            }

            if ("3" == type.ToString()) //Documentary
            {
                tempList = NameDocumentaryTree.RecorrerPreOrden();
                for (int i = 0; i < tempList.Count; i++)
                {
                    var video = tempList.ElementAt(i).Split('-');
                    Video newVideo = new Video
                    {
                        Tipo = video[0].Trim('x'),
                        Nombre = video[1].Trim('x'),
                        AñoLanzamiento = video[2].Trim('x'),
                        Genero = video[3].Trim('x')
                    };
                    DecodedVideos.Add(newVideo); 
                }
                CerrarTodo();
            }
            PagedList<Video> model = new PagedList<Video>(DecodedVideos, page, pageSize);
            CerrarTodo();
            return View(model); 
        }

        public ActionResult AddWatchlist(string id)
        {
            loggedUser = (Usuario)Session["Usuario"];
            Video newVideo = new Video();
            newVideo.Nombre = id;
            string llave = newVideo.Nombre;
            string trimedUser = loggedUser.Username.Trim('x');
            loggedUser.watchlist = new  ArbolB<Video>(3, trimedUser + ".watchlist", new FabricaVideo());
            loggedUser.watchlist.Agregar(llave, newVideo, "");
            loggedUser.watchlist.Cerrar();
            return View("CatalogoPaged");
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