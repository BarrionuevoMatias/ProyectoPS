using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Area>> ObtenerTodosAsync();
        Task<Area> ObtenerPorIdAsync(int id);
    }
}
