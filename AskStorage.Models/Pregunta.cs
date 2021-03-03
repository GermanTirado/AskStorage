using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AskStorage.Models
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La pregunta es necesaria")]
        [Display(Name = "Pregunta")]
        public string Interrogante { get; set; }

        [Required(ErrorMessage = "La respuesta es necesaria")]
        [Display(Name = "Respuesta 1")]
        public string R1 { get; set; }

        [Required(ErrorMessage = "La respuesta es necesaria")]
        [Display(Name = "Respuesta 2")]
        public string R2 { get; set; }

        [Required(ErrorMessage = "La respuesta es necesaria")]
        [Display(Name = "Respuesta 3")]
        public string R3 { get; set; }

        [Required(ErrorMessage = "La respuesta es necesaria")]
        [Display(Name = "Respuesta 4")]
        public string R4 { get; set; }

        [Required(ErrorMessage = "La respuesta es necesaria")]
        [Display(Name = "Respuesta Correcta")]
        public string RC { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string URL { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
