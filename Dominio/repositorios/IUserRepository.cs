using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ObtenerTodosAsync();
        Task<User> ObtenerPorIdAsync(int id);
        Task<IEnumerable<User>> ObtenerPorRolAsync(int roleId);
    }
}
