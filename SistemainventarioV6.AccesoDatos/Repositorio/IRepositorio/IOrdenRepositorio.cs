using SistemainventarioV6.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemainventarioV6.AccesoDatos.Repositorio.IRepositorio
{
    public interface IOrdenRepositorio : IRepositorio<Orden>
    {
        void Actualizar(Orden orden);
        void ActualizarEstado(int id, string ordenEstado, string pagoEstado);
        void ActualizarPagoStripeId(int id, string sessionId, string transaccionId);
    }
}