using AskStorage.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskStorage.AccesoDatos.Data.Repository
{
    public interface IPreguntaRepository : IRepository<Pregunta>
    {
        void Update(Pregunta pregunta);

    }
}
