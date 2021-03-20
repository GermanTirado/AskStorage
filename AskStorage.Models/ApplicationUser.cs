using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AskStorage.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Ingrese el nombre.")]
        public string Nombre { get; set; }
    }
}
