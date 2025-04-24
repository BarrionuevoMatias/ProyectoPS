using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;
using MediatR;

namespace Aplicacion.commands.Proposals
{
   public class CreateProposalCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Area { get; set; }
        public int Type { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int CreatedBy { get; set; }
    }
}
