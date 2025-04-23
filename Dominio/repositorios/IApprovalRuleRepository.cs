using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IApprovalRuleRepository
    {
        Task<IEnumerable<ApprovalRule>> ObtenerTodosAsync();
        Task<ApprovalRule> ObtenerPorIdAsync(int id);
    }
}
