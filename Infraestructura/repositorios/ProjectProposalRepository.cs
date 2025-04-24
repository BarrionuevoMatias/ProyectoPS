using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;
using Dominio.repositorios;
using Infraestructura.datos;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.repositorios
{
    class ProjectProposalRepository : IProjectProposalRepository
    {
        private readonly AppDbContext _context;

        public ProjectProposalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectProposal>> ObtenerTodosAsync()
        {
            return await _context.projectProposals
                .Include(pp => pp.AreaNavigation)
                .Include(pp => pp.TypeNavigation)
                .Include(pp => pp.StatusNavigation)
                .Include(pp => pp.CreatedByNavigation)
                .Include(pp => pp.ProjectApprovalSteps)
                .ToListAsync();
        }

        public async Task<ProjectProposal> ObtenerPorIdAsync(int id)
        {
            return await _context.projectProposals
                .Include(pp => pp.AreaNavigation)
                .Include(pp => pp.TypeNavigation)
                .Include(pp => pp.StatusNavigation)
                .Include(pp => pp.CreatedByNavigation)
                .Include(pp => pp.ProjectApprovalSteps)
                    .ThenInclude(pas => pas.ApproverRole)
                .Include(pp => pp.ProjectApprovalSteps)
                    .ThenInclude(pas => pas.StatusNavigation)
                .Include(pp => pp.ProjectApprovalSteps)
                    .ThenInclude(pas => pas.ApproverUser)
                .FirstOrDefaultAsync(pp => pp.Id == id);
        }

        public async Task<int> CrearAsync(ProjectProposal proposal)
        {
            _context.projectProposals.Add(proposal);
            await _context.SaveChangesAsync();
            return proposal.Id;
        }

        public async Task ActualizarAsync(ProjectProposal proposal)
        {
            _context.Entry(proposal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
