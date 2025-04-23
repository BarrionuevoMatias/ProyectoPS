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
    class AreaRepository : IAreaRepository 
    {
        private readonly AppDbContext _context;

        public AreaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Area>> ObtenerTodosAsync()
        {
            return await _context.areas.ToListAsync();
        }

        public async Task<Area> ObtenerPorIdAsync(int id)
        {
            return await _context.areas.FindAsync(id);
        }
    }
}
