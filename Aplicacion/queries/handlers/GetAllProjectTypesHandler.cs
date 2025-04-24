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
    class GetAllProjectTypesHandler : IRequestHandler<GetAllProjectTypesQuery, List<ProjectTypeDto>>
    {
        private readonly IProjectTypeRepository _projectTypeRepo;

        public GetAllProjectTypesHandler(IProjectTypeRepository projectTypeRepo)
        {
            _projectTypeRepo = projectTypeRepo;
        }

        public async Task<List<ProjectTypeDto>> Handle(GetAllProjectTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _projectTypeRepo.ObtenerTodosAsync();
            return types.Select(t => new ProjectTypeDto
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
        }
    }
}
