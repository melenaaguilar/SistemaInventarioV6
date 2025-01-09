using Microsoft.EntityFrameworkCore;
using SistemainventarioV6.AccesoDatos.Data;
using SistemainventarioV6.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemainventarioV6.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repositorio(ApplicationDbContext db)
        {
                _db= db;
            this.dbSet= _db.Set<T>();
        }
        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad); // insert into table
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id); // select * from tabla (solo por id)
        }

        public async Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,string IncluirPropiedades = null, bool istracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // select * from where ....

            }
            if (IncluirPropiedades != null)
            {
                foreach (var incluirProp in IncluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);// ejemplo Categoria, Marca
                }

            }
            if(orderby != null)
            {
                query = orderby(query);
            }
            if(!istracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();   
        }


        public async Task<T> obtenerPrimero(Expression<Func<T, bool>> filtro = null, string IncluirPropiedades = null, bool istracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // select * from where ....

            }
            if (IncluirPropiedades != null)
            {
                foreach (var incluirProp in IncluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);// ejemplo Categoria, Marca
                }

            }
            if (!istracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }


        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
