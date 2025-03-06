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
    public class CarroCompraRepositorio : Repositorio<CarroCompra>, ICarroCompraRepositorio
    {

        private readonly ApplicationDbContext _db;

        public CarroCompraRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(CarroCompra carroCompra)
        {
            _db.Update(carroCompra);
        }
    }
}
