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
    class ApprovalRuleRepository : IApprovalRuleRepository
    {
        private readonly AppDbContext _context;

        public ApprovalRuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalRule>> ObtenerTodosAsync()
        {
            return await _context.approvalRules
                .Include(ar => ar.AreaNavigation)
                .Include(ar => ar.TypeNavigation)
                .Include(ar => ar.ApproverRole)
                .ToListAsync();
        }

        public async Task<ApprovalRule> ObtenerPorIdAsync(int id)
        {
            return await _context.approvalRules
                .Include(ar => ar.AreaNavigation)
                .Include(ar => ar.TypeNavigation)
                .Include(ar => ar.ApproverRole)
                .FirstOrDefaultAsync(ar => ar.Id == id);
        }
    }
}
