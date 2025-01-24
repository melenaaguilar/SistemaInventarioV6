using Microsoft.EntityFrameworkCore.ChangeTracking;
using SistemainventarioV6.AccesoDatos.Data;
using SistemainventarioV6.AccesoDatos.Repositorio.IRepositorio;
using SistemainventarioV6.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace SistemainventarioV6.AccesoDatos.Repositorio
{
        public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Marca marca)
        {
            var marcaBD = _db.Marcas.FirstOrDefault(b => b.Id == marca.Id);
            if (marcaBD != null)
            {
                marcaBD.Nombre = marca.Nombre;
                marcaBD.Descripcion = marca.Descripcion;
                marcaBD.Estado = marca.Estado;
                _db.SaveChanges();
            }
        }
    }
}
