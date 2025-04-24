using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Aplicacion.DTOs;

namespace Aplicacion.queries
{
   public class GetAllAreasQuery : IRequest<List<AreaDto>>
    {
    }
}
