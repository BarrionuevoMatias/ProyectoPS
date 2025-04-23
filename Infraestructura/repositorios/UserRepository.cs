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
    class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> ObtenerTodosAsync()
        {
            return await _context.users.Include(u => u.ApproverRole).ToListAsync();
        }

        public async Task<User> ObtenerPorIdAsync(int id)
        {
            return await _context.users.Include(u => u.ApproverRole).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> ObtenerPorRolAsync(int roleId)
        {
            return await _context.users.Where(u => u.Role == roleId).ToListAsync();
        }
    }
}
