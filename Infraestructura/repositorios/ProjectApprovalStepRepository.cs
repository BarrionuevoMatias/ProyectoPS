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
    class ProjectApprovalStepRepository : IProjectApprovalStepRepository
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectApprovalStep>> ObtenerTodosAsync()
        {
            return await _context.projectApprovalSteps
                .Include(pas => pas.ProjectProposal)
                .Include(pas => pas.ApproverRole)
                .Include(pas => pas.StatusNavigation)
                .Include(pas => pas.ApproverUser)
                .ToListAsync();
        }

        public async Task<ProjectApprovalStep> ObtenerPorIdAsync(long id)
        {
            return await _context.projectApprovalSteps
                .Include(pas => pas.ProjectProposal)
                .Include(pas => pas.ApproverRole)
                .Include(pas => pas.StatusNavigation)
                .Include(pas => pas.ApproverUser)
                .FirstOrDefaultAsync(pas => pas.Id == id);
        }

        public async Task<long> CrearAsync(ProjectApprovalStep step)
        {
            _context.projectApprovalSteps.Add(step);
            await _context.SaveChangesAsync();
            return step.Id;
        }

        public async Task ActualizarAsync(ProjectApprovalStep step)
        {
            _context.Entry(step).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectApprovalStep>> ObtenerPorProyectoIdAsync(int projectId)
        {
            return await _context.projectApprovalSteps
                .Where(pas => pas.ProjectProposalId == projectId)
                .Include(pas => pas.ApproverRole)
                .Include(pas => pas.StatusNavigation)
                .Include(pas => pas.ApproverUser)
                .OrderBy(pas => pas.StepOrder)
                .ToListAsync();
        }
    }
}
