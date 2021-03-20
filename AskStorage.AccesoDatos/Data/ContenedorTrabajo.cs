using AskStorage.AccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskStorage.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Pregunta = new PreguntaRepository(_db);
            Usuario = new UsuarioRepository(_db);
        }
        public ICategoriaRepository Categoria { get; private set; }
        public IPreguntaRepository Pregunta { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
