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
    class ApprovalStatusRepository : IApprovalStatusRepository
    {
        private readonly AppDbContext _context;

        public ApprovalStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalStatus>> ObtenerTodosAsync()
        {
            return await _context.approvalStatuses.ToListAsync();
        }

        public async Task<ApprovalStatus> ObtenerPorIdAsync(int id)
        {
            return await _context.approvalStatuses.FindAsync(id);
        }
    }
}
