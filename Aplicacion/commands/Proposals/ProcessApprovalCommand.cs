using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Aplicacion.commands.Proposals
{
    class ProcessApprovalCommand : IRequest<bool>
    {
        public long StepId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public string? Observations { get; set; }
    }
}
