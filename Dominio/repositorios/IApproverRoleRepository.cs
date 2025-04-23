using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IApproverRoleRepository
    {
        Task<IEnumerable<ApproverRole>> ObtenerTodosAsync();
        Task<ApproverRole> ObtenerPorIdAsync(int id);
    }
}
