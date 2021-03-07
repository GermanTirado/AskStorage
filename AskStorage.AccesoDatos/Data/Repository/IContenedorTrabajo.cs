using System;
using System.Collections.Generic;
using System.Text;

namespace AskStorage.AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        // Repositorios
        ICategoriaRepository Categoria { get; }
        IPreguntaRepository Pregunta { get; }
        void Save();
    }
}
