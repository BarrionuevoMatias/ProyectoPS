using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IApprovalStatusRepository
    {
        Task<IEnumerable<ApprovalStatus>> ObtenerTodosAsync();
        Task<ApprovalStatus> ObtenerPorIdAsync(int id);
    }
}
