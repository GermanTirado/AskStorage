using AskStorage.AccesoDatos.Data.Repository;
using AskStorage.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AskStorage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PreguntasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public PreguntasController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {           
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            PreguntaVM prevm = new PreguntaVM()
            {
                Pregunta = new Models.Pregunta(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(prevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PreguntaVM preVM)
        {
            if (ModelState.IsValid)
            {

                //Validación
                string CopiaNombrePregunta = Verificaciones(preVM.Pregunta.Interrogante);
                
                string conf = Confirmacion(CopiaNombrePregunta);
                

                if (conf == "")
                {
                    if (preVM.Pregunta.Id == 0)
                    {
                        //Nueva Pregunta
                        _contenedorTrabajo.Pregunta.Add(preVM.Pregunta);
                        _contenedorTrabajo.Save();

                        return RedirectToAction(nameof(Index));
                    }
                }
                ViewData["Mensaje"] = conf;

            }

            preVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(preVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            PreguntaVM prevm = new PreguntaVM()
            {
                Pregunta = new Models.Pregunta(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            if (id != null)
            {
                prevm.Pregunta = _contenedorTrabajo.Pregunta.Get(id.GetValueOrDefault());
            }
            return View(prevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PreguntaVM preVM)
        {
            if (ModelState.IsValid)
            {
                var preguntaDesdeDB = _contenedorTrabajo.Pregunta.Get(preVM.Pregunta.Id);               

                _contenedorTrabajo.Pregunta.Update(preVM.Pregunta);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var preguntaDesdeDB = _contenedorTrabajo.Pregunta.Get(id);
            string rutaDirectorioPrincipal = _hostingEnviroment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, preguntaDesdeDB.URL.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (preguntaDesdeDB == null)
            {
                return Json(new { success = false, message = "Error al borrar la pregunta" });
            }

            _contenedorTrabajo.Pregunta.Remove(preguntaDesdeDB);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Pregunta eliminada con éxito" });
        }

        #region Llamadas a la API
        // Mostrar todos los datos
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Pregunta.GetAll(includeProperties: "Categoria") });

        }

        #endregion

        #region Verificación

        public string Confirmacion(string cadena)
        {
            var verif = _contenedorTrabajo.Pregunta.GetAll();
            foreach (var cat in verif)
            {
                if (cadena == Verificaciones(cat.Interrogante))
                {
                    return cat.Interrogante;
                }
            }
            return "";
        }

        private string Verificaciones(string cadena)
        {
            //Quitar Espacios
            cadena = cadena.Replace(" ", String.Empty);
            //Lower Case
            cadena = cadena.ToLower();
            //Acentos
            cadena = RemoveAcentos(cadena);
            //Caracteres
            cadena = CaracteresRemove(cadena);

            return cadena;
        }

        // Remover Acentos
        public static string RemoveAcentos(string cadena)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            cadena = replace_a_Accents.Replace(cadena, "a");
            cadena = replace_e_Accents.Replace(cadena, "e");
            cadena = replace_i_Accents.Replace(cadena, "i");
            cadena = replace_o_Accents.Replace(cadena, "o");
            cadena = replace_u_Accents.Replace(cadena, "u");
            return cadena;
        }
        // Quitar Caracteres Especiales
        public static string CaracteresRemove(string cadena)
        {
            cadena = cadena.Replace("¿", "");
            cadena = cadena.Replace("?", "");
            cadena = cadena.Replace("!", "");
            cadena = cadena.Replace("¡", "");
            return cadena;
        }
        #endregion+
    }
}
