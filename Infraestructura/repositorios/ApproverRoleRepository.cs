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
    class ApproverRoleRepository : IApproverRoleRepository
    {
        private readonly AppDbContext _context;

        public ApproverRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApproverRole>> ObtenerTodosAsync()
        {
            return await _context.approverRoles.ToListAsync();
        }

        public async Task<ApproverRole> ObtenerPorIdAsync(int id)
        {
            return await _context.approverRoles.FindAsync(id);
        }

    }
}
