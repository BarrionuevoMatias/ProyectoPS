using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Dominio.repositorios;
using MediatR;

namespace Aplicacion.queries.handlers
{
    class GetAllAreasHandler : IRequestHandler<GetAllAreasQuery, List<AreaDto>>
    {
        private readonly IAreaRepository _areaRepo;

        public GetAllAreasHandler(IAreaRepository areaRepo)
        {
            _areaRepo = areaRepo;
        }

        public async Task<List<AreaDto>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
        {
            var areas = await _areaRepo.ObtenerTodosAsync();
            return areas.Select(a => new AreaDto
            {
                Id = a.Id,
                Nombre = a.Nombre
            }).ToList();
        }
    }
}
