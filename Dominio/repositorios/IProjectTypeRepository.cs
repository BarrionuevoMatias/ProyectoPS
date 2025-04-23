using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IProjectTypeRepository
    {
        Task<IEnumerable<ProjectType>> ObtenerTodosAsync();
        Task<ProjectType> ObtenerPorIdAsync(int id);
    }
}
