using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AskStorage.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre")]
        [Display(Name = "Nombre Categoría")]
        public string NombreCategoria { get; set; }
    }
}
