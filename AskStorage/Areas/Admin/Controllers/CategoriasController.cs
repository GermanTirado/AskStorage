using AskStorage.AccesoDatos.Data.Repository;
using AskStorage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AskStorage.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriasController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Cargar formulario Agregar Categoria
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //Boton POST Categorías
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            //Validacion 
            if (ModelState.IsValid)
            {
                //Validación
                string CopiaNombreCategoria = Verificaciones(categoria.NombreCategoria);

                string conf = Confirmacion(CopiaNombreCategoria);

                if (conf == "")
                {
                    _contenedorTrabajo.Categoria.Add(categoria);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                //Retorno Categoría Existente
                ViewData["Mensaje"] = conf;
            }

            return View(categoria);
        }

        //Editar Get
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }
        //Boton Editar POST Categorías
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {

            // Validacion
            if (ModelState.IsValid)
            {
                string CopiaNombreCategoria = Verificaciones(categoria.NombreCategoria);

                string conf = Confirmacion(CopiaNombreCategoria);

                if (conf == "")
                {
                    _contenedorTrabajo.Categoria.Update(categoria);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Categoria"] = conf;
            }

            return View(categoria);
        }


        #region Llamadas a la API
        // Mostrar todos los datos
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        // Borrar
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Ocurrio un error al borrar la categoría" });
            }

            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se ha eliminado la categoría correctamente" });
        }
        #endregion

        #region Verificación

        public string Confirmacion(string cadena)
        {
            var verif = _contenedorTrabajo.Categoria.GetAll();
            foreach (var cat in verif)
            {
                if (cadena == Verificaciones(cat.NombreCategoria))
                {
                    return cat.NombreCategoria;
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
        #endregion

    }
}
