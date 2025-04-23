using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Dominio.repositorios
{
    public interface IProjectProposalRepository
    {
        Task<IEnumerable<ProjectProposal>> ObtenerTodosAsync();
        Task<ProjectProposal> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(ProjectProposal proposal);
        Task ActualizarAsync(ProjectProposal proposal);
    }
}
