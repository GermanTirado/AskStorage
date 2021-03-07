using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskStorage.Models.ViewModels
{
    public class PreguntaVM
    {
        public Pregunta Pregunta { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
