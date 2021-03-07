using AskStorage.AccesoDatos.Data.Repository;
using AskStorage.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskStorage.AccesoDatos.Data
{
    public class PreguntaRepository : Repository<Pregunta>, IPreguntaRepository
    {
        private readonly ApplicationDbContext _db;

        public PreguntaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       

        //Update Categoria
        public void Update(Pregunta pregunta)
        {
            var objDesdeDb = _db.Pregunta.FirstOrDefault(s => s.Id == pregunta.Id);
            objDesdeDb.Interrogante = pregunta.Interrogante;
            objDesdeDb.R1 = pregunta.R1;
            objDesdeDb.R2 = pregunta.R2;
            objDesdeDb.R3 = pregunta.R3;
            objDesdeDb.R4 = pregunta.R4;
            objDesdeDb.RC = pregunta.RC;
            objDesdeDb.URL = pregunta.URL;
            objDesdeDb.CategoriaId = pregunta.CategoriaId;
            //_db.SaveChanges();
        }
    }
}
