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
    class ProjectTypeRepository : IProjectTypeRepository
    {
        private readonly AppDbContext _context;

        public ProjectTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectType>> ObtenerTodosAsync()
        {
            return await _context.projectTypes.ToListAsync();
        }

        public async Task<ProjectType> ObtenerPorIdAsync(int id)
        {
            return await _context.projectTypes.FindAsync(id);
        }
    }
}
