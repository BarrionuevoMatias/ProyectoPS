using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IProjectApprovalStepRepository
    {
        Task<IEnumerable<ProjectApprovalStep>> ObtenerTodosAsync();
        Task<ProjectApprovalStep> ObtenerPorIdAsync(long id);
        Task<long> CrearAsync(ProjectApprovalStep step);
        Task ActualizarAsync(ProjectApprovalStep step);
        Task<IEnumerable<ProjectApprovalStep>> ObtenerPorProyectoIdAsync(int projectId);
    }
}
